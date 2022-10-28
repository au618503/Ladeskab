using Cabinet_Library.ChargeControl;
using Cabinet_Library.Door;
using Cabinet_Library.RfIdReader;
using Cabinet_Library.StationControl.States;

namespace Cabinet_Library.StationControl;

// What should StationControl do on ChargingEvent ????
// StationControl is the transisition between states, so it should be the one to call the state change.
public interface IStationControl
{
    public void OnRfidEvent(object? sender, RfidEventArgs args);

    public void OnDoorEvent(object? sender, DoorEventArgs args);

    public void OnChargerError(object? sender, ChargingEventArgs args);

    public void ChargingFinished();

    void LogDoorLocked(int id);
    void LogDoorUnlocked(int id);
    void ChangeState(StationStateBase state);
    public void Reset();
}