using Cabinet_Library.ChargeControl.States;
using Cabinet_Library.Display;
using Cabinet_Library.ObserverPattern;

namespace Cabinet_Library.ChargeControl
{
    public class ChargeControl : IPublisher<ChargingEventArgs>
    {
        #region Publisher

        public void AddListener(IObserver observer, EventHandler<ChargingEventArgs> callback)
        {
            ChargingStateChanged += callback;
        }

        public void RemoveListener(EventHandler<ChargingEventArgs> callback)
        {
            ChargingStateChanged -= callback;
        }
        #endregion

        /* Controls charging via State pattern
         * StateCharging and StateFullyCharged run a non blocking method, which poll charger
         * interface, and act according to the specifications
        */

        IUsbCharger _charger;
        public IDisplay _display;
        private StateBase _state;
        public event EventHandler<ChargingEventArgs> ChargingStateChanged;

        public ChargeControl(IUsbCharger charger, IDisplay display)
        {
            _charger = charger;
            _display = display;
            var defaultState = new StateReady(_charger, this);
            ChangeState(defaultState);
        }

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
        // Monitoring logic delegated to states via GoF State pattern
        public void ChangeState(StateBase state)
        {
            _state = state;
            _display.SetChargingText(_state.DisplayMessage);
            ChargingStateChanged?.Invoke(this, new ChargingEventArgs()
            { Id = _state.StateId, Message = _state.DisplayMessage });
        }

        public StateID GetState()
        {
            return _state.StateId;
        }

     
    }
    }
}
