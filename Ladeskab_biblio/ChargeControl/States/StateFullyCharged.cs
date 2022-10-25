namespace Ladeskab_biblio.ChargeControl.States;

public class StateFullyCharged : StateBase
{
    private const double ThresholdError = 500;
    public StateFullyCharged(IUsbCharger charger) : base(charger) { }
    public override void MonitorCurrentLevel()
    {

    }
}