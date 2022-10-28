namespace Cabinet_Library.ChargeControl.States;

// Indicates that an error has occurred
public class StateError : StateBase
{
    private const StateID Id = StateID.ERROR;
    private const string Message = "Charging error. Contact support.";
    public double LoggedCurrentValue { get;}
    public StateError(IUsbCharger charger, IChargeControl context, double loggedCurrentValue) : base(charger, context, Id)
    {
        LoggedCurrentValue = loggedCurrentValue;
        Context.OnError(LoggedCurrentValue);
        Charging = false;
        DisplayMessage = Message;
    }
}