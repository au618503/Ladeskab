namespace Ladeskab_biblio.ChargeControl.States
{
    public class StateCharging : StateBase
    {
        public StateCharging(IUsbCharger charger) : base(charger) {}
        public override void MonitorCurrentLevel()
        {

        }
    }
}
