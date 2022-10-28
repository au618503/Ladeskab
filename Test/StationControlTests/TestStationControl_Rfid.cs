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
using Cabinet_Library.Logger;
using Cabinet_Library.RfIdReader;


namespace UnitTests.StationControlTests
{
    public class TestStationControl_Rfid
    {

        private  StationControl _uut;
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

        // RfidDetected, State == available, DoorIsClosed, DeviceConnected:
        [Test]
        [TestCase(Id)]
        public void RfidDetected_IdSaved(int id)
        {
            _rfidReader.RfidEvent += Raise.EventWith(_rfidReader, new RfidEventArgs() { Rfid = id });

            Assert.IsTrue(_uut.GetState().SavedId == id);
        }

        [Test]
        public void RfidDetected_DoorReceivedLock()
        {
            _rfidReader.RfidEvent += Raise.EventWith(_rfidReader, new RfidEventArgs() { Rfid=Id});
            
            _door.Received().LockDoor();
        }

        [Test]
        public void RfidDetected_ChargeReceivedStart()
        {
            _rfidReader.RfidEvent += Raise.EventWith(_rfidReader, new RfidEventArgs() { Rfid = Id });
            _chargeControl.Received().StartCharge();
        }

        [Test]
        public void RfidDetected_LogDoorLocked()
        {
            _rfidReader.RfidEvent += Raise.EventWith(_rfidReader, new RfidEventArgs() { Rfid = Id });
            _logFile.Received().LogDoorLocked(Id);
        }

        [Test]
        public void RfidDetected_StateChangedToOccupied()
        {
            _rfidReader.RfidEvent += Raise.EventWith(_rfidReader, new RfidEventArgs() { Rfid = Id });
            Assert.IsTrue(_uut.GetState().StateID == StationStateID.OCCUPIED);
        }

        // Test Rfid functionality in different States
        [Test]
        public void RfidDetected_DoorOpenedState_DoNothing()
        {
            _uut.ChangeState(new DoorOpenedState(_uut, _chargeControl, _display, _door, null));
            _rfidReader.RfidEvent += Raise.EventWith(_rfidReader, new RfidEventArgs() { Rfid = Id });
            // Hard to test that NOTHING is happening, but at least we can test we are in the same state
            Assert.IsTrue(_uut.GetState().StateID == StationStateID.DOOROPEN);
            _chargeControl.DidNotReceive().StartCharge();
        }

        // Test RfidDetected (if other tests passed, state will be OCCUPIED) in occupied state while charging
        [Test]
        public void RfidDetected_IdCorrect_Occupied_DoorUnlocked()
        {
            _rfidReader.RfidEvent += Raise.EventWith(_rfidReader, new RfidEventArgs() { Rfid = Id });
            _rfidReader.RfidEvent += Raise.EventWith(_rfidReader, new RfidEventArgs() { Rfid = Id });
            _door.Received().UnlockDoor();
        }
        [Test]
        public void RfidDetected_IdCorrect_Occupied_LogReceivedUnlock()
        {
            _rfidReader.RfidEvent += Raise.EventWith(_rfidReader, new RfidEventArgs() { Rfid = Id });
            _rfidReader.RfidEvent += Raise.EventWith(_rfidReader, new RfidEventArgs() { Rfid = Id });
            _logFile.Received().LogDoorUnlocked(Id);
        }

        [Test]
        public void RfidDetected_WrongId_DoorDidNotUnlock()
        {
            _rfidReader.RfidEvent += Raise.EventWith(_rfidReader, new RfidEventArgs() { Rfid = Id });
            _rfidReader.RfidEvent += Raise.EventWith(_rfidReader, new RfidEventArgs() { Rfid = 0 });
            _door.DidNotReceive().UnlockDoor();
        }
    }
}

    
