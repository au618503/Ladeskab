namespace Ladeskab_biblio.ChargeControl.States;

// Indicates that an error has occurred
public class StateError : StateBase
{
    private const StateID Id = StateID.ERROR;
    private const string Message = "Charging error. Contact support.";
    public StateError(ChargeControl context) : base(null, context, Id)
    {
        Charging = false;
        DisplayMessage = Message;
    }

}