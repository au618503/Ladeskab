using Cabinet_Library.ChargeControl;
using Cabinet_Library.Door;
using Cabinet_Library.RfIdReader;
using Cabinet_Library.StationControl;
using System;
using Cabinet_Library_StationControl;

class Program
{
    static void Main(string[] args)
    {
        // Assemble your system here from all the classes
        Door door = new Door();
        RfidReader rfidReader = new RfidReader();
        UsbChargerSimulator usbcharger = new UsbChargerSimulator();
        StationControl _stationControl = new StationControl();

        bool finish = false;
        do
        {
            string input;
            System.Console.WriteLine("Indtast E, O, C, R, T, A: ");
            input = Console.ReadLine();
            if (string.IsNullOrEmpty(input)) continue;

            switch (input[0])
            {
                case 'E':
                    finish = true;
                    break;

                case 'O':
                    door.SimulateDoorOpened();
                    break;

                case 'C':
                    door.SimulateDoorClosed();
                    break;
                case 'T':
                    usbcharger.SimulateConnected(true);
                    break;
                case 'A':
                    usbcharger.SimulateConnected(false);
                    break;
                case 'R':
                    System.Console.WriteLine("Indtast RFID id: ");
                    string idString = System.Console.ReadLine();

                    int id = Convert.ToInt32(idString);
                    rfidReader.OnRfidRead(id);
                    break;

                default:
                    break;
            }

        } while (!finish);
    }
}