using System;

namespace Cabinet_Library.Display
{
    public class Display
    {
        private string _chargingText = "";
        public void Vis(string Tekst)
        {
            // skrives ud på display, printf, hvad den bliver bedt om.
            Console.WriteLine(Tekst);
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
