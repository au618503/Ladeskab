namespace Ladeskab_biblio.ChargeControl.States;

public class StateBase
{
    protected IUsbCharger _charger;
    public StateBase(IUsbCharger charger)
    {
        _charger = charger;
    }

    public virtual void MonitorCurrentLevel()
    {

    }
}