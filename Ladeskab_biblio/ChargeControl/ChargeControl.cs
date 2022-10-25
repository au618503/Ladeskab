using Ladeskab_biblio.ChargeControl.States;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Ladeskab_biblio.ChargeControl
{
    public class ChargeControl
    {
        public class StateChangedEventArgs : EventArgs
        {
            public ChargeState State;
        }

        IUsbCharger _charger;
        ChargeState _chargeState;
        StateBase _state;
        public event EventHandler<StateChangedEventArgs> StateChanged;
        // Use these values to monitor the charging
        const double ThresholdMin = 5;
        const double ThresholdMax = 500;
        private double _lastLoggedCurrent;
        public ChargeControl(IUsbCharger charger)
        {
            _charger = charger;
        }

        // Check if device is connected to the charger and return status
        public bool DeviceConnected()
        {
            return _charger.Connected;
        }

        double GetCurrentLevel()
        {
            return _charger.CurrentValue;
        }

        void MonitorCurrentLevel()
        {
            double level = GetCurrentLevel();
            if (level > ThresholdMax)
            {
                // ERROR! STOP CHARGING IMMEDIATILY!
                _charger.StopCharge();
                _chargeState = ChargeState.ERROR;
                OnChargeStateChange();
            }
            else if (level == 0)
            {
                _chargeState = 
            }
        }

        void OnChargeStateChange()
        {
            StateChanged.Invoke(this, new StateChangedEventArgs(){State = _chargeState});
        }
    }
}
