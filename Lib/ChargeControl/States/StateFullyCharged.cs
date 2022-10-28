using Cabinet_Library.ChargeControl;

namespace Cabinet_Library.ChargeControl.States;

public class StateFullyCharged : StateBase
{
    private const StateID Id = StateID.FULLY_CHARGED;
    private const string Message = "Device fully charged.";
    public StateFullyCharged(IUsbCharger charger, IChargeControl context) : base(charger, context, Id)
    {
        DisplayMessage = Message; ;
    }

    public sealed override void MonitorCurrentLevel(double current)
    {
        if (current > ThresholdError)
        {
            StopCharge();
            Context.ChangeState(new StateError(Charger, Context, current));
        }
    }
}