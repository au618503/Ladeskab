using System;

namespace Cabinet_Library.RfIdReader
{

    public class RfidReader : IRfIdReader
    {
        public event EventHandler<RfidEventArgs>? RfidEvent;
        public void SimulateRfidDetected(int id)
        {
            RfidEvent?.Invoke(this, new RfidEventArgs() { Rfid = id });
        }
    }

}
