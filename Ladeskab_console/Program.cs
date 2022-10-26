using Ladeskab;
<<<<<<< HEAD
using Ladeskab_biblio.Door;
using System;
using UsbChargerSimulator;
using NUnit.Framework;
using NSubstitute;
using Ladeskab_biblio.ChargeControl;


=======
using Ladeskab_biblio.StationControl;
using System;
using Ladeskab_biblio.ChargeControl;
>>>>>>> origin/LadeskabUnitTests

class Program
{
    static void Main(string[] args)
    {
        // Assemble your system here from all the classes
        Door door = new Door();
        RfidReader rfidReader = new RfidReader();
        UsbChargerSimulator usbcharger = new UsbChargerSimulator();
        StationControl _stationControl = new StationControl(door, usbcharger, rfidReader);

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
                    door.OnDoorOpen();
                    break;

                case 'C':
                    door.OnDoorClose();
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