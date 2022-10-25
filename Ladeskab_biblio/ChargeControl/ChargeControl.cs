using Ladeskab_biblio.ChargeControl.States;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Ladeskab_biblio.ChargeControl
{
    public class ChargeControl
    {
        /* Controls charging via State pattern
         * StateCharging and StateFullyCharged run a non blocking method, which poll charger
         * interface, and act according to the specifications
        */

        // Can add error message if more errors are introduced
        public class ChargingEventArgs : EventArgs
        {
            public StateID Id { get; set; }
            public string? Message { get; set; }
        }

        IUsbCharger _charger;
        private StateBase _state;
        public event EventHandler<ChargingEventArgs> ChargingStateChanged;
        
        // Subscribe to state change events in the constructor
        public ChargeControl(IUsbCharger charger, EventHandler<ChargingEventArgs> handler)
        {
            _charger = charger;
            var defaultState = new StateReady(_charger, this);
            ChargingStateChanged += handler;
            ChangeState(defaultState);
        }

        public void StartCharge()
        {
            ChangeState(new StateCharging(_charger, this));
        }

        public void StopCharge()
        {
            _state.StopCharge();
        }
        // All logic delegated to states via GoF State pattern
        public void ChangeState(StateBase state)
        {
            _state = state;
            ChargingStateChanged.Invoke(this, new ChargingEventArgs()
                { Id = _state.StateId , Message = _state.DisplayMessage});
        }
    }
}
