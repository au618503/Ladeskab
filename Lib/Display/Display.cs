

namespace Cabinet_Library.Display
{
    public class Display : IDisplay
    {
        private string _chargingText = "";
        private string _mainText = "";

        public void Show()
        {
            Console.WriteLine(_mainText);
            Console.Write("\n");
            Console.WriteLine(_chargingText);
        }

        public void SetMainText(string text)
        {
            _mainText = text;
        }
        // Used by ChargeControl. Display needs to display both charge info (if any) and 
        // StationControl info
        public void SetChargingText(string text)
        {
            _chargingText = text;
        }
    }
}
