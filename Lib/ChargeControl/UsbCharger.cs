using System;

namespace Cabinet_Library.ChargeControl
{
    public class CurrentEventArgs : EventArgs
    {
        // Value in mA (milliAmpere)
        public double Current { set; get; }
    }

    public interface IUsbCharger
    {
        // Event triggered on new current value
        event EventHandler<CurrentEventArgs> CurrentValueEvent;

        // Direct access to the current current value
        double CurrentValue { get; }

        // Require connection status of the phone
        bool Connected { get; }

        void OnPhoneFullyCharged();

        // Start charging
        void StartCharge();
        // Stop charging
        void StopCharge();
    }
}