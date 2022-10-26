using Cabinet_Library.ChargeControl.States;
using Cabinet_Library.Display;
using Cabinet_Library.ObserverPattern;

namespace Cabinet_Library.ChargeControl
{
    public class ChargeControl : IChargeControl
    {
        #region Publisher
        /*
         * IPublisher<ChargingEventArgs> implementation
         *
         * Deprecated: ChargeControl is not supposed to send events
         * UNLESS we decide to add an error ocurred event
         *
        public void AddListener(IObserver observer, EventHandler<ChargingEventArgs> callback)
        {
            ChargingStateChanged += callback;
        }

        public void RemoveListener(EventHandler<ChargingEventArgs> callback)
        {
            ChargingStateChanged -= callback;
        }*/
        #endregion

        /* Controls charging via State pattern
         * States check currentlevel via MonitorCurrentLevel and make sure if current is above
         * error level, charging is stopped, as well as set display message
        */

        IUsbCharger _charger;
        private IDisplay _display;
        private StateBase _state;
        // Initial idea was to notify StationControl when state changes
        // This is unnecessary
        // Notification on ERROR sounds like a good idea though. Use delegate set in constructor?

        // public event EventHandler<ChargingEventArgs> ChargingStateChanged;

        public ChargeControl(IUsbCharger charger, IDisplay display)
        {
            _charger = charger;
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
            /*ChargingStateChanged?.Invoke(this, new ChargingEventArgs()
            { Id = _state.StateId, Message = _state.DisplayMessage, Current = _charger.CurrentValue });*/
        }

        public StateID GetState()
        {
            return _state.StateId;
        }
    }
}
