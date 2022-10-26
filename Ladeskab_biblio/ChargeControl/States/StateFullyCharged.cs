namespace Ladeskab_biblio.ChargeControl.States;

public class StateFullyCharged : StateBase
{
    private const StateID Id = StateID.FULLY_CHARGED;
    private const string Message = "Device fully charged.";
    public StateFullyCharged(IUsbCharger charger, ChargeControl context) : base(charger, context, Id)
    {
        DisplayMessage = Message;;
    }

    public sealed override void MonitorCurrentLevel(double current)
    {
        if (current > ThresholdError)
        {
            StopCharge();
            Context.ChangeState(new StateError(Context));
        }
    }
}