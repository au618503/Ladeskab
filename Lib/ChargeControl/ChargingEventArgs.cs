using Cabinet_Library.ChargeControl.States;

namespace Cabinet_Library.ChargeControl;

public class ChargingEventArgs : EventArgs
{
    // Can add error message if more errors are introduced
    public double Current { get; set; }
}