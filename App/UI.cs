using System.Reflection.PortableExecutable;

namespace App
{

    public class UI
    {
        public string Connected { get; set; }
        public string DoorOpen { get; set; }
        public string DoorLocked { get; set; }
        private readonly int _noLines = 6;

        public void DisplayUI(Action displayShow, double BatteryLevel)
        {
            Console.SetCursorPosition(0, 4);
            Console.WriteLine("Status: ");
            Console.Write("Door: " + DoorOpen + ", " + DoorLocked + "    " + "Connected: " + Connected + "\n");
            Console.Write("Phone battery level: " + BatteryLevel + "%\n");
            Console.Write("Display: \n");
            displayShow();
        }

        public void Clear()
        {
            Console.SetCursorPosition(0, 4);
            for (int i = 0; i < _noLines; i++)
            {
                string cleared = "";
                for (int j = 0; j < 160; j++)
                {
                    cleared += "  ";
                }
                Console.Write(cleared + "\n");
            }
        }
    }
}