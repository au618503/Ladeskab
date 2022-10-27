using System;
using System.Linq.Expressions;
using Cabinet_Library.StationControl;
using Cabinet_Library.ChargeControl;
using Cabinet_Library.Display;
using Cabinet_Library.RfIdReader;
using Cabinet_Library.Door;
using Cabinet_Library.Logger;
using Cabinet_Library_StationControl;

class Program
{
    static string InputNoBlock()
    {
        while (true)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
            }
        }
    }
    static void Main(string[] args)
    {
        // Assemble your system here from all the classes
        Door door = new Door();
        RfidReader rfidReader = new RfidReader();
        UsbChargerSimulator usbcharger = new UsbChargerSimulator();
        Display display = new Display();
        LogFile logger = new LogFile();
        ChargeControl chargeControl = new ChargeControl(usbcharger, display);
        StationControl _stationControl = new StationControl(door, display, chargeControl, rfidReader, logger);

        System.Console.WriteLine("Indtast:\n E_xit, O_pen door, C_lose door, R_fid scan, T_ phone connected, A_ phone disconnected: ");
        bool finish = false;
        do
        {
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
                        usbcharger.SimulateConnected(true);
                        break;
                    case ConsoleKey.A:
                        usbcharger.SimulateConnected(false);
                        break;
                    case ConsoleKey.R:
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        rfidReader.SimulateRfidDetected(id);
                        break;
                    case ConsoleKey.Z:
                        System.Console.WriteLine("Simulating charger overload");
                        usbcharger.SimulateOverload(true);
                        break;
                    default:
                        break;
                }
            }



        } while (!finish);
    }
}