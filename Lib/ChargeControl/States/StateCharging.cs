using Cabinet_Library.ChargeControl;

namespace Cabinet_Library.ChargeControl.States
{
    public class StateCharging : StateBase
    {
        private const StateID Id = StateID.CHARGING;
        private const string Message = "Charging...";
        public StateCharging(IUsbCharger charger, ChargeControl context) : base(charger, context, Id)
        {
            Charger.StartCharge();
            DisplayMessage = Message;
        }
        public sealed override void MonitorCurrentLevel(double current)
        {
            if (current > ThresholdError)
            {
                StopCharge();
                Context.ChangeState(new StateError(Charger, Context));
                return;
            }
            else if (current < ThresholdCharging)
            {
                if (Charger.Connected)
                {
                    // Switch state to StateFullyCharged 
                    Context.ChangeState(new StateFullyCharged(Charger, Context));
                    // Exits this loop, but doesn't stop the charger
                    Charging = false;
                }
                else
                {
                    // Device got disconnected, switch state to ready and stop charging
                    Context.ChangeState(new StateReady(Charger, Context));
                    StopCharge();
                }
            }
        }
    }
}
