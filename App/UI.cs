using System.Reflection.PortableExecutable;
using Cabinet_Library.Display;

namespace App
{

    public class UI
    {
        public string Connected { get; set; }
        public string DoorOpen { get; set; }
        public string DoorLocked { get; set; }
        private ConsoleColor _defaultColor = ConsoleColor.White;
        private ConsoleColor _batFull = ConsoleColor.Blue;
        private ConsoleColor _batLow = ConsoleColor.Red;
        private ConsoleColor _batMed = ConsoleColor.Yellow;
        private ConsoleColor _batHigh = ConsoleColor.Green;
        private readonly int _noLines = 8;

        public void DisplayUI(Display display, double BatteryLevel)
        {
            Console.SetCursorPosition(0, 4);
            Console.WriteLine("Status: ");
            Console.Write("Door: " + DoorOpen + ", " + DoorLocked + "    " + "Connected: " + Connected + "\n");
            Console.Write("Phone battery level: ");
            Console.ForegroundColor = ChooseColor(BatteryLevel);
            Console.Write(BatteryLevel.ToString("N2") + "%\n");
            Console.ForegroundColor = _defaultColor;
            Console.Write("Display: \n");
            Console.WriteLine(display.MainText + "\n");
            if (display.ChargingText.Contains("error"))
            {
                Console.ForegroundColor = _batLow;
                Console.WriteLine(display.ChargingText);
                Console.ForegroundColor = _defaultColor;
            }
            else
            {
                Console.WriteLine(display.ChargingText);
            }
        }

        private ConsoleColor ChooseColor(double level)
        {
            ConsoleColor color = ConsoleColor.White;
            if (level < 33)
            {
                color = _batLow;
            }
            else if (level >= 33 && level <= 66)
            {
                color = _batMed;
            }
            else if (level < 99)
            {
                color = _batHigh;
            }
            else if (level >= 99)
            {
                color = _batFull;
            }

            return color;
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