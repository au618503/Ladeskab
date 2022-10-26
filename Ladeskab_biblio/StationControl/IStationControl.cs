using Ladeskab_biblio.ChargeControl;

namespace Ladeskab_biblio.StationControl;

public interface IStationControl : IObserver<ChargingEventArgs>
{
    void ChargingEventCallback();
}