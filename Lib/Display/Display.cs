using System;

namespace Cabinet_Library.Display
{
    public class Display : IDisplay
    {
        public string _chargingText { get; set; }
        public string _stateText { get; set; }
        //private string _chargingText = "";
        public void Show(string Text)
        {
            _stateText = Text;
            Console.WriteLine(_stateText);
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
