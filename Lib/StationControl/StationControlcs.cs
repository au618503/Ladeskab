using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cabinet_Library.ChargeControl;
using Cabinet_Library;
using static Cabinet_Library.Door;


namespace Cabinet_Library.StationControl
{
    public class StationControl
    {


        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum CabinetState
        {
            Available,
            Locked,
            DoorOpen
        };
        // Her mangler flere member variable
        public CabinetState _state { get; private set; }


        //private IChargeControl _charger;
        private IUsbCharger _charger;
        private int _oldId;
        private IDoor _door;
        private Display.Display _display;
        private IRfid _rfid;
        private ChargeControl.ChargeControl _chargeControl;

        private string logFile = "LogFile.txt"; // Navnet på systemets log-fil

        // event variables = tjek lige om det er rigtigt ift ILogFile navngivning
        Door.Doorevent -= HandleDoorOpenCloseEvent;
            public _rfidEvent {get; private set; }


    public StationControl(IDoor door, IUsbCharger charger, IRfid rfid, LogFile logfile, IDisplay display)
    {
        var door;
        door.DoorOpenedEvent += HandleDoorOpenedEvent;

        _charger = charger;
        _chargeControl = new ChargeControl.ChargeControl(charger, display);


        _rfid = rfid;
        _rfid.RfidDetectedEvent += HandleRfidDetectedEvent;

        _display = display;
        _display = new Display.Display();


        _logfile = logfile;
        _state = CabinetState.Available;
    }

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
    }

    #endregion

    private void Acces(int id)
    {
        //Checks for powerconnection
        if (!IsNotConected())
        {
            _display.DisplayMessage("Phone is not connected");

        }
        else
        {
            _charger.StartCharge();
            _door.LockDoor();
            _display.DisplayMessage("Door is locked");
            _state = CabinetState.Locked;
            _oldId = id;
            _logfile.LogDoorLocked(id.ToString());
            _display.Rfid();

        }
    }
    
    private void ChangeDoorState(Door door)
    {
        if (_doorEvent == DoorState.Unlocked)
        {
            DoorOpened();
            
        }
        else
        {
            DoorClosed();
           
        }
    }

  

}


//  _door.DoorClosedEvent += HandleDoorClosedEvent;

//  _charger.CurrentValueEvent += HandleCurrentValueEvent;

