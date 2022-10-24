using System;

namespace Ladeskab
{
    public class RfidEventArgs : EventArgs
    {
        public int IsRead { set; get; }
    }

    public interface IRfid
    {
        // Event triggered on door opened or closed
        event EventHandler<RfidEventArgs> RfidEvent;

        // Direct access to the door status
        int RfidIsRead { get; }

        void OnRfidRead(int id);
    }

    public class RfidReader : IRfid
    {
        public event EventHandler<RfidEventArgs> RfidEvent;
        public int RfidIsRead { get; private set; }
        public void OnRfidRead(int id)
        {
            OnNewRfidStatus();
        }
        private void OnNewRfidStatus()
        {
            RfidEvent?.Invoke(this, new RfidEventArgs() { IsRead = this.RfidIsRead});
        }
    }

}
