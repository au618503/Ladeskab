using Ladeskab_biblio.ChargeControl.States;

namespace Ladeskab_biblio.ChargeControl;

public class ChargingEventArgs
{
    // Can add error message if more errors are introduced
    public StateID Id { get; set; }
    public string? Message { get; set; }
    public double? Current { get; set; }
}