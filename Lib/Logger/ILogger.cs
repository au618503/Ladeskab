namespace Cabinet_Library.Logger;

public interface ILogger
{
    public void LogDoorLocked(int id);
    public void LogDoorUnlocked(int id);
    public void LogChargingError(double currentValue);
}