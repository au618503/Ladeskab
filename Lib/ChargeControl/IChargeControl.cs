using Cabinet_Library.ChargeControl.States;

namespace Cabinet_Library.ChargeControl;

public interface IChargeControl
{
    public event EventHandler<ChargingEventArgs> ErrorEvent;
    public void StartCharge();
    public void StopCharge();
    public bool DeviceConnected();
    public void OnCurrentEvent(object? sender, CurrentEventArgs args);
    public void ChangeState(StateBase state);
}