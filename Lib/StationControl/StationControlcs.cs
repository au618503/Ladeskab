using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cabinet_Library.ChargeControl;

namespace Cabinet_Library.StationControl
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        private LadeskabState _state;
        //private IChargeControl _charger;
        private IUsbCharger _charger;
        private int _oldId;
        private IDoor _door;
        private Display.Display _display;
        private IRfid _rfid;
        private ChargeControl.ChargeControl _chargeControl;

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        // Her mangler constructor
        public StationControl(IDoor door, IUsbCharger charger, IRfid rfid)
        {
            _display = new Display.Display();

            _door = door;
            _door.DoorEvent += HandleDoor;

            _charger = charger;
            //_charger.CurrentValueEvent += HandleChargeCurrent;

            _rfid = rfid;
            _rfid.RfidEvent += HandleRfidDetected;
        }

        public void OnChargingStateChanged(object? sender, ChargingEventArgs args)
        {
            Console.WriteLine("Charging state changed: " + args.Id + "\nDisplay message: " + args.Message);
        }
        private void HandleDoor(object sender, DoorEventArgs e)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    if (e.IsOpen && !e.IsLocked)
                    {
                        _display.Vis("Tilslut telefon");
                        _state = LadeskabState.DoorOpen;
                    }
                    break;
                case LadeskabState.DoorOpen:
                    if (!e.IsOpen && !e.IsLocked)
                    {
                        _display.Vis("Indlæs RFID");
                        _state = LadeskabState.Available;
                    }
                    break;
                default:
                    // do nothing
                    break;
            }
        }

        private void HandleChargeCurrent(object sender, CurrentEventArgs e)
        {
        }

        private void HandleRfidDetected(object sender, RfidEventArgs e)
        {
            RfidDetected(e.Rfid);
        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen

        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected)
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = id;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

                        Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _display.Vis("Ladeskab optaget");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                        _display.Vis("Tilslutningsfejl");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }

                        Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                        _display.Vis("Fjern telefon");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        Console.WriteLine("Forkert RFID tag");
                        _display.Vis("RFID fejl");
                    }

                    break;
            }
        }

        // Her mangler de andre trigger handlere
    }
}
