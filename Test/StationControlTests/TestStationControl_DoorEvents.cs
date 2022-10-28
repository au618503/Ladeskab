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
public class TestStationControl_DoorEvents
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
    public void DefaultStateIsAvailable()
    {
        Assert.IsTrue(_uut.GetState().StateID == StationStateID.AVAILABLE);
    }

    [Test]
    public void StateAvailable_DoorOpens_ChangedState()
    {
        _door.DoorEvent += Raise.EventWith(_door, new DoorEventArgs() { IsOpen = true});
        Assert.IsTrue(_uut.GetState().StateID == StationStateID.DOOROPEN); 
    }

    [Test]
    public void StateAvailable_DoorOpens_ThenCloses_StateIsAvailable()
    {
        _door.DoorEvent += Raise.EventWith(_door, new DoorEventArgs() { IsOpen = true });
        _door.DoorEvent += Raise.EventWith(_door, new DoorEventArgs() { IsOpen = false });
        Assert.IsTrue(_uut.GetState().StateID == StationStateID.AVAILABLE);
    }

}