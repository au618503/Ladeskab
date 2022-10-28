using Cabinet_Library.ChargeControl;
using Cabinet_Library.Display;
using Cabinet_Library.Door;
using Cabinet_Library.Logger;
using Cabinet_Library.RfIdReader;
using Cabinet_Library.StationControl.States;
using Cabinet_Library_StationControl;
using NSubstitute;
using NUnit.Framework;

namespace UnitTests.StationControlTests;

[TestFixture]
public class TestStationControl_ErrorEvent
{
    private StationControl _uut;
    private IDoor _door;
    private IChargeControl _chargeControl;
    private IDisplay _display;
    private ILogger _logFile;
    private IRfIdReader _rfidReader;
    private StationStateBase _state;
    private const int Id = 123;

    [SetUp]
    // Initial test conditions:
    // + Door closed
    // + Device Connected
    // + Start in default state: AVAILABLE
    public void Setup()
    {
        _door = Substitute.For<IDoor>();
        _chargeControl = Substitute.For<IChargeControl>();
        _door.DoorIsOpen.Returns(false);
        _chargeControl.DeviceConnected().Returns(true);
        _display = Substitute.For<IDisplay>();
        _rfidReader = Substitute.For<IRfIdReader>();
        _logFile = Substitute.For<ILogger>();
        _uut = new StationControl(_door, _display, _chargeControl, _rfidReader, _logFile);
    }

    [Test]
    [TestCase(1000)]
    [TestCase(99999)]
    public void ErrorEventReceived_ErrorLogged(double c)
    {
        _chargeControl.ErrorEvent += Raise.EventWith(_chargeControl, new ChargingEventArgs() { Current = c });
        _logFile.Received().LogChargingError(c);
    }
    [Test]
    [TestCase(1000)]
    [TestCase(99999)]
    public void ErrorEventReceived_TextChanged(double c)
    {
        string errorText = $"Error, current too high ({c})\n Reset system.";
        _chargeControl.ErrorEvent += Raise.EventWith(_chargeControl, new ChargingEventArgs() { Current = c });
        _display.Received().SetMainText(errorText);
    }
}