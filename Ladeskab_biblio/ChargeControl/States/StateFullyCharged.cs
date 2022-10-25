namespace Ladeskab_biblio.ChargeControl.States;

public class StateFullyCharged : StateBase
{
    private const StateID Id = StateID.FULLY_CHARGED;
    private const string Message = "Device fully charged.";
    public StateFullyCharged(IUsbCharger charger, ChargeControl context) : base(charger, context, Id)
    {
        DisplayMessage = Message;
        Task.Run(() => MonitorCurrentLevel());
    }

    public sealed override void MonitorCurrentLevel()
    {
        while (Charging)
        {
            double currentLevel = Charger.CurrentValue;
            if (currentLevel > ThresholdError)
            {
                StopCharge();
                Context.ChangeState(new StateError(Context));
            }
        }
    }
}