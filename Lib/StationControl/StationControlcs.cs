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
using Cabinet_Library.ObserverPattern;
using Cabinet_Library.Display;
using Cabinet_Library.StationControl;
using Cabinet_Library.StationControl.States;
using Cabinet_Library.Logger;
using Cabinet_Library.RfIdReader;

namespace Cabinet_Library_StationControl
{
    public class StationControl : IStationControl 
        {
              #region Events 
              //events for door and rdif

           public void OnRfidEvent(RfidEventArgs e, object RfidDetectedEventDetected)
          {
            int id = e.Rfid;
            _state.OnRfidDetected(id);
           }


           public void HandleDoorEvent(object sender, DoorEventArgs e)
    {
        Doorevent = e.DoorEvent;
        OnNewDoorStatús.Inwoke(this, new);
        return Doorevent;
    }

             #endregion
             
        private IDoor _door;
        private IDisplay _display;
        private IChargeControl _chargeControl;
        private IRfIdReader _rfid;
        private ILogger _logFile;
        private StationStateBase _state;

        public StationControl(IDoor door, IDisplay display, IChargeControl chargeControl, IRFIDReader rfidReader, ILogFile logFile)
        {
        
        }

        public void OnDoorEvent(object? sender, DoorEventArgs args)
        {
        if (args.IsOpen) 
        {
            _state.OnDoorOpen();
        }
        else{
            _state.OnDoorClose();
        }
      }

        public void OnChargeEvent(object? sender, ChargingEventArgs args)
        {
            _state.MonitorChargeState(args.Id, args.Message);
        }

        public void OnRfidEvent(object? sender, RfidEventArgs args)
        {
            object value = _state.CheckRfid(args.Rfid);
        }
        public void OnStartCharge(object? sender, ChargingEventArgs args)
        {
            _state.StartCharge();   

        }
        public void OnStopCharge(object? sender, ChargingEventArgs args)
        {
            _state.StopCharge(args.);

        }

        public void OnLockDoor(object? sender, DoorEventArgs args)
        {
            IDoor.LockDoor(args.IsLocked);
        }

        public void OnUnlockDoor(object? sender, DoorEventArgs args)
        {
            IDoor.UnlockDoor();
        }

        public void ChangeState(StationStateBase state)
        {
            _state = state;
            _display.SetStationText(_state.DisplayMessage);
            StationStateChanged?.Invoke(this, new StationEventArgs()
            { Id = _state.StateId, Message = _state.DisplayMessage });
        }
    }


