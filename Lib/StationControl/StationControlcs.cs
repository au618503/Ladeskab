
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

namespace Cabinet_Library_StationControl
    {
        public class StationControl : IPublisher<EventArgs>
        {
              #region Publisher

           public void AddListener(IObserver observer, EventHandler<StationEventArgs> callback)
        {
            StationStateChanged += callback;
        }

          public void RemoveListener(EventHandler<StationEventArgs> callback)
        {
            StationStateChanged -= callback;
        }
              #endregion

              #region Events 
              //events for door and rdif

           public void OnRfidEvent(RfidEventArgs e, object RfidDetectedEventDetected)
          {
              RfidEvent = e.RfidDetected;
              RfidEvent?.Invoke(this, new);
          }


           public void HandleDoorEvent(object sender, DoorEventArgs e)
    {
        Doorevent = e.DoorEvent;
        OnNewDoorStatús.Inwoke(this, new);
        return Doorevent;
    }

             #endregion

        public StationControl(IDoor door, IDisplay display, IChargeControl chargeControl, IRFIDReader rfidReader, ILogFile logFile)
        {
              private IDoor _door;
        private IDisplay _display;
        private IChargeControl _chargeControl;
        private IRFIDReader _rfidReader;
        private ILogFile _logFile;
        private StateBase _state;
        public event EventHandler<StationEventArgs> StationStateChanged;
        public event EventHandler<DoorHandlerArgs> DoorStateChanged;
        }

        public void OnDoorEvent(object? sender, DoorEventArgs args)
        {
            _state.MonitorDoorState(args.IsOpen, args.IsLocked);
        }

        public void OnChargeEvent(object? sender, ChargingEventArgs args)
        {
            _state.MonitorChargeState(args.Id, args.Message);
        }

        public void OnRfidEvent(object? sender, RfidEventArgs args)
        {
            object value = _state.MonitorRfidState(args.Rfid);
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

        public void OnChangeState(StateBase state)
        {
            _state = state;
            _display.SetStationText(_state.DisplayMessage);
            StationStateChanged?.Invoke(this, new StationEventArgs()
            { Id = _state.StateId, Message = _state.DisplayMessage });
        }
    }


