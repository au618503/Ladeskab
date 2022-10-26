using System;

namespace Cabinet_Library.Display
{
    public class Display
    {
        private _display;
        private string _chargingText = "";
        public void Show(string Text);
        {
            // skrives ud på display, printf, hvad den bliver bedt om.
            Console.WriteLine(Text);
            Console.Write("\n");
            Console.WriteLine(_chargingText);
        }

        // Used by ChargeControl. Display needs to display both charge info (if any) and 
        // StationControl info
        public void SetChargingText(string text)
        {
            _chargingText = text;
        }
    }
}
