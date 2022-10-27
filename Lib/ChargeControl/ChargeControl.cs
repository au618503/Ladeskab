using Cabinet_Library.ChargeControl.States;
using Cabinet_Library.Display;
using Cabinet_Library.ObserverPattern;

namespace Cabinet_Library.ChargeControl
{
    public class ChargeControl : IChargeControl
    {
        
        IUsbCharger _charger;
        public IDisplay _display;

        private StateBase _state;
        
        // Initial idea was to notify StationControl when state changes
        // This is unnecessary
        // Notification on ERROR sounds like a good idea though. Use delegate set in constructor?

        public event EventHandler<ChargingEventArgs> ErrorEvent;

        public ChargeControl(IUsbCharger charger, IDisplay display)
        {
            _charger = charger;
            _charger.CurrentValueEvent += OnCurrentEvent;
            _display = display;
            var defaultState = new StateReady(_charger, this);
            ChangeState(defaultState);
        }

        public bool DeviceConnected()
        {
            return _charger.Connected;
        }

        // Monitoring logic delegated to states via GoF State pattern
        public void OnCurrentEvent(object? sender, CurrentEventArgs args)
        {
            _state.MonitorCurrentLevel(args.Current);
        }

        public void StartCharge()
        {
            ChangeState(new StateCharging(_charger, this));
        }

        public void StopCharge()
        {
            _state.StopCharge();
        }

        // Display needs to be notified when state is changed, but this can be done via a single method
        public void ChangeState(StateBase state)
        {
            _state = state;
            _display.SetChargingText(_state.DisplayMessage);
        }

        public void OnError(double current)
        {
            ErrorEvent.Invoke(this, new ChargingEventArgs(){Current = current});
        }

        public StateID GetState()
        {
            return _state.StateId;
        }

        // Reset the charger after an error has occured
        public void Reset()
        {
            ChangeState(new StateReady(_charger, this));
        }

        public IUsbCharger GetCharger()
        {
            return _charger;
        }
    }
}
