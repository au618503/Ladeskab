namespace Cabinet_Library.StationControl;

// What should StationControl do on ChargingEvent ????
// StationControl is the transisition between states, so it should be the one to call the state change.
public interface IStationControl
{
    void OnDoorOpened();
    void OnDoorClosed();
    void OnChargerError();
    void OnRfidDetected(int id);
}