using Cabinet_Library.ChargeControl;

namespace Cabinet_Library.ChargeControl.States;

// This state indicates that the system ready but doesn't do anything
public class StateReady : StateBase
{
    public const StateID Id = StateID.READY;
    private const string Message = "";
    public StateReady(IUsbCharger charger, ChargeControl context) : base(charger, context, Id)
    {
        Charging = false;
        DisplayMessage = Message;
    }
}