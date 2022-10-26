using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab_biblio.ChargeControl;
using Ladeskab_biblio.Display;
using Ladeskab_biblio.Door;

//using Ladeskab.Interfaces;

namespace Ladeskab
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
        private Display _display;
        private IRfid _rfid;
        private ChargeControl _chargeControl;

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        // Her mangler constructor
        public StationControl(IDoor door, IUsbCharger charger, IRfid rfid)
        {
            _display = new Display();

            _door = door;
            _door.DoorEvent += HandleDoor;

            _charger = charger;
            //_charger.CurrentValueEvent += HandleChargeCurrent;

            _rfid = rfid;
            //_rfid.RfidEvent += RfidDetected;
            /* TEST CHARGE CONTROL
             *_chargeControl = new ChargeControl(_charger, OnChargingStateChanged);
            _chargeControl.StartCharge();*/
        }

        public void OnChargingStateChanged(object? sender, ChargeControl.ChargingEventArgs args)
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
                        _display.show("Tilslut telefon");
                        _state = LadeskabState.DoorOpen;
                    }
                    break;
                case LadeskabState.DoorOpen:
                    if (!e.IsOpen && !e.IsLocked)
                    {
                        _display.show("Indlæs RFID");
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
                            writer.WriteLine(DateTime.Now + ": Cabinet locked with RFID: {0}", id);
                        }

                        Console.WriteLine("Cabinet is locked. Your phone is now charging. Use RfID to unlock cabinet.");
                        _display.Show("Cabinet occupied");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        Console.WriteLine("You phone is not connected succesfully. Try again.");
                        _display.Show("Connection error");
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
                            writer.WriteLine(DateTime.Now + ": Cabinet unlocked with RFID: {0}", id);
                        }

                        Console.WriteLine("Take your phone out of the cabinet and close the door");
                        _display.Show("Remove phone");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        Console.WriteLine("Wrong RFID tag");
                        _display.Show("RFID error");
                    }

                    break;
            }
        }

        // Her mangler de andre trigger handlere
        
    }
}
