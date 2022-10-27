using System;
using System.Linq.Expressions;
using App;
using Cabinet_Library.StationControl;
using Cabinet_Library.ChargeControl;
using Cabinet_Library.Display;
using Cabinet_Library.RfIdReader;
using Cabinet_Library.Door;
using Cabinet_Library.Logger;
using Cabinet_Library_StationControl;

class Program
{

    static void Main(string[] args)
    {
        Console.Title = "Charging Cabinet";
        // Assemble your system here from all the classes
        Door door = new Door();
        RfidReader rfidReader = new RfidReader();
        UsbChargerSimulator usbcharger = new UsbChargerSimulator();
        Phone phone = new Phone(usbcharger);
        Display display = new Display();
        LogFile logger = new LogFile();
        ChargeControl chargeControl = new ChargeControl(usbcharger, display);
        StationControl _stationControl = new StationControl(door, display, chargeControl, rfidReader, logger);
        UI ui = new UI();
        Console.CursorVisible = false;
        System.Console.WriteLine("Indtast:\n E_xit, O_pen door, C_lose door, R_fid scan," +
                                 " T: Connected, A: Disconnected\n Z: Simulate Overcharge, M : Reset system, U: Reset phone battery\n ");
        bool finish = false;
        do
        {
            if (door.DoorIsOpen)
            {
                ui.DoorOpen = "Open";
            }
            else
            {
                ui.DoorOpen = "Closed";
            }

            if (door.DoorIsLocked)
            {
                ui.DoorLocked = "Locked";
            }
            else
            {
                ui.DoorLocked = "Unlocked";
            }

            if (usbcharger.Connected)
            {
                ui.Connected = "Yes";
            }
            else
            {
                ui.Connected = "False";
            }

            ui.DisplayUI(display.Show, phone.BatteryLevel);
            //display.Show("Charger state: "+ chargeControl.GetState().ToString());
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.E:
                        finish = true;
                        break;

                    case ConsoleKey.O:
                        door.SimulateDoorOpened();
                        break;

                    case ConsoleKey.C:
                        door.SimulateDoorClosed();
                        break;
                    case ConsoleKey.T:
                        if (door.DoorIsOpen)
                        {
                            usbcharger.SimulateConnected(true);
                        }
                        
                        if (usbcharger.Connected)
                        {
                            ui.Connected = "Yes";
                            phone.Connected = true;
                        }
                        break;
                    case ConsoleKey.A:
                        if (door.DoorIsOpen)
                        {
                            usbcharger.SimulateConnected(false);
                        }
                        
                        if (usbcharger.Connected)
                        {
                            ui.Connected = "No";
                            phone.Connected = false;
                        }
                        break;
                    case ConsoleKey.R:
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();
                        try
                        {
                            int id = Convert.ToInt32(idString);
                            rfidReader.SimulateRfidDetected(id);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("RFID input must be integer!");
                        }

                        break;
                        
                    case ConsoleKey.Z:
                        System.Console.WriteLine("Simulating charger overload");
                        usbcharger.SimulateOverload(true);
                        break;
                    case ConsoleKey.M:
                        usbcharger.SimulateOverload(false);
                        _stationControl.Reset();
                        phone.UnloadBattery();
                        break;
                    case ConsoleKey.U:
                        phone.UnloadBattery();
                        break;
                    default:
                        break;
                }
            }
            System.Threading.Thread.Sleep(200);
            ui.Clear();
        } while (!finish);
    }
}