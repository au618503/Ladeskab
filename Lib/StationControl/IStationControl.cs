namespace Cabinet_Library.StationControl;

// What should StationControl do on ChargingEvent ????
public interface IStationControl
{
    void OnDoorOpened();
    void OnDoorClosed();
    void OnChargerError();
    void OnRfidDetected(int id);
}