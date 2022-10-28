

namespace Cabinet_Library.Display
{
    public class Display : IDisplay
    {
        public string ChargingText = "";
        public string MainText = "";

        public void Show()
        {
            Console.WriteLine(MainText);
            Console.Write("\n");
            Console.WriteLine(ChargingText);
        }

        public void SetMainText(string text)
        {
            MainText = text;
        }
        // Used by ChargeControl. Display needs to display both charge info (if any) and 
        // StationControl info
        public void SetChargingText(string text)
        {
            ChargingText = text;
        }
    }
}
