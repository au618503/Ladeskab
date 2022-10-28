namespace Cabinet_Library.Display;

public interface IDisplay
{
    public void Show();

    public void SetMainText(string text);
    public void SetChargingText(string chargingText);
}