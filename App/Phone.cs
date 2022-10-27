using Cabinet_Library.ChargeControl;

namespace App;

public class Phone
{
    public double BatteryLevel = 0;
    public bool Connected = false;
    private IUsbCharger _charger;

    public Phone(IUsbCharger charger, double batteryLevel = 0)
    {
        _charger = charger;
        _charger.CurrentValueEvent += OnCurrentEvent;
        BatteryLevel = batteryLevel;
    }


    public void OnCurrentEvent(object? sender, CurrentEventArgs args)
    {
        BatteryLevel += args.Current / 200;
        if (BatteryLevel > 100)
        {
            BatteryLevel = 100;
            _charger.OnPhoneFullyCharged();
        }
    }

    public void UnloadBattery()
    {
        BatteryLevel = 0;
    }
}