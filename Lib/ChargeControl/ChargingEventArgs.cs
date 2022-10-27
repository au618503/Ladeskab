using Cabinet_Library.ChargeControl.States;

namespace Cabinet_Library.ChargeControl;

public class ChargingEventArgs
{
    // Can add error message if more errors are introduced
    public StateID? Id { get; set; }
    public string? Message { get; set; }
    public double Current { get; set; }
}