namespace Cabinet_Library.Display;

public interface IDisplay
{
    public string _chargingText { get;}
    public string _stateText { get;}
    public void SetChargingText(string chargingText);
}