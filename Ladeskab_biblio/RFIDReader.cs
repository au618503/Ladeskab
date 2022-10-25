using System;

namespace Ladeskab
{
    public class RfidEventArgs : EventArgs
    {
        public int Rfid { set; get; }
    }

    public interface IRfid
    {
        event EventHandler<RfidEventArgs> RfidEvent;

        int RfidRead { get; }

        void OnRfidRead(int id);
    }

    public class RfidReader : IRfid
    {
        public event EventHandler<RfidEventArgs> RfidEvent;
        public int RfidRead { get; private set; }
        public void OnRfidRead(int id)
        {
            OnNewRfidStatus();
        }
        private void OnNewRfidStatus()
        {
            RfidEvent?.Invoke(this, new RfidEventArgs() { Rfid = this.RfidRead});
        }
    }

}
