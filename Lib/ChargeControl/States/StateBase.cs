using Cabinet_Library.ChargeControl;

namespace Cabinet_Library.ChargeControl.States;

public class StateBase
{
    public readonly StateID StateId;
    public string DisplayMessage { get; set; }
    protected IUsbCharger Charger;
    protected IChargeControl Context;
    protected const double ThresholdError = 500;
    protected const double ThresholdCharging = 5;
    protected bool Charging = true;
    public StateBase(IUsbCharger charger, IChargeControl context, StateID stateId)
    {
        Charger = charger;
        Context = context;
        StateId = stateId;
    }

    public virtual void MonitorCurrentLevel(double current)
    {

    }

    public void StopCharge()
    {
        Charger.StopCharge();
        // Makes sure if a task is running, it is stopped
        Charging = false;
    }
    public virtual void OnEnter() { }
}