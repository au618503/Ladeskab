using Cabinet_Library.ChargeControl;
using Cabinet_Library.Display;
using Cabinet_Library.Door;

namespace Cabinet_Library.StationControl.States;

public class DoorOpenedState : StationStateBase
{
    private readonly StationStateID stateId = StationStateID.DOOROPEN;
    public string TextEnter { get; } = "Connect Phone to the charger";
    public string TextLeave { get; } = "Scan RFID.";
    public DoorOpenedState(IStationControl context, IChargeControl chargeControl, IDisplay display, IDoor door, int? savedId) 
        : base(context, chargeControl, display, door, savedId)
    {
        Display.SetMainText(TextEnter);
    }

    public override void OnDoorClosed()
    {
        Display.SetMainText(TextLeave);
        Context.ChangeState(new AvailableState(Context, ChargeControl, Display, Door, SavedId));
    }
}