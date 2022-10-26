using Ladeskab_biblio.ChargeControl;

namespace Ladeskab_biblio.StationControl;

// What should StationControl do on ChargingEvent ????
public interface IStationControl
{
    void OnDoorOpened();
    void OnDoorClosed();
    void OnChargerError();
    void OnRfidDetected(int id);
}