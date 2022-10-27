using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cabinet_Library.ChargeControl;
using Cabinet_Library;
using NSubstitute;
using Cabinet_Library.Display;
using Cabinet_Library.Door;
using Cabinet_Library_StationControl;
using Cabinet_Library.StationControl;
using Cabinet_Library.StationControl.States;

namespace UnitTests.StationControlTests
{
    public class TestStationControlInput
    {
        private IDisplay _display = Substitute.For<IDisplay>();
        private IChargeControl _chargeControl = Substitute.For<IChargeControl>();
        private IDoor _door = Substitute.For<IDoor>();
        private StationControl _uut;
        private IStationControl _stationControl = Substitute.For<IStationControl>();
        
        
        [SetUp]
        public void Setup()
        {
            _uut = new StationControl(_chargeControl, _display, _door, _stationControl);
        }

        [Test]
        public void TestDoorOpened_StateIsDoorOpened()
        {
            _door.DoorOpenedEvent += Raise.EventWith(new DoorEventArgs() { DoorOpen = true });
            Assert.That(_uut.GetState(), Is.EqualTo(StationStateID.DOOR_OPENED));
        }

        [Test]
        public void TestDoorOpened_DisplayCalledSetDoorOpenText()
        {
            _door.DoorOpenedEvent += Raise.EventWith(new DoorEventArgs() { DoorOpen = true });
            _display.Received().SetDoorOpenText("Door is open");
        }

        [Test]
        public void TestDoorClosed_StateIsAvailable()
        {
            _door.DoorOpenedEvent += Raise.EventWith(new DoorEventArgs() { DoorOpen = true });
            _door.DoorOpenedEvent += Raise.EventWith(new DoorEventArgs() { DoorOpen = false });
            Assert.That(_uut.GetState(), Is.EqualTo(StationStateID.AVAILABLE));
        }

        [Test]
        public void TestDoorClosed_DisplayCalledSetDoorClosedText()
        {
            _door.DoorOpenedEvent += Raise.EventWith(new DoorEventArgs() { DoorOpen = true });
            _door.DoorOpenedEvent += Raise.EventWith(new DoorEventArgs() { DoorOpen = false });
            _display.Received().SetDoorClosedText("Door is closed");
        }

        [Test]
        public void TestRfidDetected_StateIsOccupied()
        {
            _door.DoorOpenedEvent += Raise.EventWith(new DoorEventArgs() { DoorOpen = true });
            _door.DoorOpenedEvent += Raise.EventWith(new DoorEventArgs() { DoorOpen = false });
            _uut.RfidDetected(1);
            Assert.That(_uut.GetState(), Is.EqualTo(StationStateID.OCCUPIED));
        }

        [Test]
        public void TestRfidDetected_DisplayCalledSetOccupiedText()
        {
            _door.DoorOpenedEvent += Raise.Event

    }
}
