using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cabinet_Library.ChargeControl;
using Cabinet_Library;
using System.Runtime.InteropServices;
using Cabinet_Library.ChargeControl.States;
using Cabinet_Library.Display;
using Cabinet_Library.Door;
using Cabinet_Library.StationControl;
using Cabinet_Library.StationControl.States;
using Cabinet_Library.Logger;
using Cabinet_Library.RfIdReader;

namespace Cabinet_Library_StationControl
{
    public class StationControl : IStationControl
    {
        private IDoor _door;
        private IDisplay _display;
        private IChargeControl _chargeControl;
       // private IRfIdReader _rfid;
        private ILogger _logFile;
        private StationStateBase _state;

        #region EventHandlers

        //events for door and rdif

        public void OnRfidEvent(object? sender, RfidEventArgs args)
        {
            int id = args.Rfid;
            _state.OnRfidDetected(id);
        }

        public void OnDoorEvent(object? sender, DoorEventArgs args)
        {
            if (args.IsOpen)
            {
                _state.OnDoorOpened();
            }
            else
            {
                _state.OnDoorClosed();
            }
        }

        public void OnChargerError(object? sender, ChargingEventArgs args)
        {
            _display.SetMainText($"Error, current too high ({args.Current})\n Reset system.");
            _logFile.LogChargingError(args.Current);
        }

        #endregion

        
        public StationControl(IDoor door, IDisplay display, IChargeControl chargeControl, IRfIdReader rfidReader,
            ILogger logFile)
        {
            rfidReader.RfidEvent += OnRfidEvent;
            _door = door;
            _door.DoorEvent += OnDoorEvent;
            _display = display;
            _chargeControl = chargeControl;
            _chargeControl.ErrorEvent += OnChargerError;
            _logFile = logFile;
            _state = new AvailableState(this, _chargeControl, _display, _door, null);

        }

        public StationControl(IChargeControl chargeControl, IDisplay display, IDoor door, IStationControl stationControl)
        {
            _chargeControl = chargeControl;
            _display = display;
            _door = door;
            _state = new AvailableState(this, _chargeControl, _display, _door, null);
        }

        public StationStateBase GetState()
        {
            return _state;
        }
        public void ChangeState(StationStateBase state)
        {
            _state = state;
        }

        public void LogDoorLocked(int id)
        {
            _logFile.LogDoorLocked(id);
            
        }
        public void LogDoorUnlocked(int id)
        {
            _logFile.LogDoorUnlocked(id);
        }

        public void Reset()
        {
            _chargeControl.StopCharge();
            _chargeControl.Reset();
            _display.SetMainText("Welcome to Chargebinet 1.027. Open the door and follow the instructions!");
            _door.UnlockDoor();
            ChangeState(new AvailableState(this, _chargeControl, _display, _door, null));
        }

        public void ChargingFinished()
        {
            _chargeControl.StopCharge();
            ChangeState(new AvailableState(this, _chargeControl, _display, _door, null));
            _chargeControl.ChangeState(new StateReady(_chargeControl.GetCharger(), _chargeControl));
        }
    }
}


