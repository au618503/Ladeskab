using Ladeskab_biblio.ChargeControl.States;
using Ladeskab_biblio.ObserverPattern;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Ladeskab_biblio.ChargeControl
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
        private StateBase _state;
        public event EventHandler<ChargingEventArgs> ChargingStateChanged;
        
        public ChargeControl(IUsbCharger charger)
        {
            _charger = charger;
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
            ChargingStateChanged?.Invoke(this, new ChargingEventArgs()
                { Id = _state.StateId , Message = _state.DisplayMessage});
        }

        public StateID GetState()
        {
            return _state.StateId;
        }
    }
}
