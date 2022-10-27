namespace Cabinet_Library.ChargeControl.States;

// Indicates that an error has occurred
public class StateError : StateBase
{
    private const StateID Id = StateID.ERROR;
    private const string Message = "Charging error. Contact support.";
    public StateError(IUsbCharger charger, ChargeControl context) : base(charger, context, Id)
    {
        Context.OnError(charger.CurrentValue);
        Charging = false;
        DisplayMessage = Message;
    }

}