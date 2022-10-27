using System;

namespace Cabinet_Library.RfIdReader
{

    public class RfidReader : IRfIdReader
    {
        public event EventHandler<RfidEventArgs> RfidEvent;
        public int RfidRead { get; private set; }
        public void OnRfidRead(int id)
        {
            OnNewRfidStatus();
        }
        private void OnNewRfidStatus()
        {
            RfidEvent?.Invoke(this, new RfidEventArgs() { Rfid = RfidRead });
        }
    }

}
