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
    public class TestStationControl
    {

        private  StationControl _uut;
        private IDoor _door;
        private IChargeControl _chargeControl;
        private IDisplay _display;
        private ILogger _logFile;
        private IRfIdReader _rfidReader;
        private IRfIdReader _rfid;
        private StationStateBase _state;


        [SetUp]
        //Arrange
        public void Setup()
        {
            _door = Substitute.For<IDoor>();
           // _chargeControl = Substitute.For<IChargeControl>();
            _display = Substitute.For<IDisplay>();
            _rfidReader = Substitute.For<IRfIdReader>();
            _state = Substitute.For<StationStateBase>();
            _logFile = Substitute.For<ILogger>();
            _uut = new StationControl(_door, _display, _chargeControl, _rfidReader, _logFile);

        }
       


        void TestLogDoor(ILogger _logFile)
        {
            _uut.LogDoorLocked(1);
            _logFile.Received().LogDoorLocked(1);
           
        }

        [Test]
        public void TestLogDoor_Unlocked()
        {
            _uut.LogDoorUnlocked(1);
            _logFile.Received().LogDoorUnlocked(1);
            
        }



        //#region TestDoor
        //[Test]
        ////ACT and ASSERT
        //public void TestStationControl_DoorOpen_true()
        //{

        //    //Setup the stub with resired response
        //    _door.DoorEvent += Raise.EventWith(new DoorEventArgs() { IsOpen = true });

        //    //_door.DoorIsOpen = true;
        //    //_door.DoorEvent.Invoke(this, new DoorEventArgs() { IsOpen = DoorIsOpen });

        //    //_door.DoorIsOpen = true;

        //    ////assert that doorOpen is called correct
        //    Assert.That(_uut., Is.EqualTo(true));
        //}
        #endregion
        //[Test]
        //PASSED
        //public void TestStationControl_LogDoorLocked()
        //{
        //   // _door.DoorEvent += Raise.EventWith(new DoorEventArgs());
        //    DoorEventArgs testargs = new LogEventArgs() { IsOpen = true };

        //    _door.DoorEvent += Raise.EventWith(testargs);
        //    testargs.IsOpen = true;

        //    Assert.That(actual: _uut., Is.EqualTo(,IsOpen));
        //}

        


        //[Test]
        //public void TestStationControl_RFIDDetected()
        //{
        //    _rfid.RfidEvent += Raise.EventWith(new RfidEventArgs { Rfid = 1 });

        //    Assert.That(_rfidReader, Is.EqualTo(_rfid));
        //}




        //    _uut.ChangeState(new AvailableState(_uut, _door, _chargeControl, _display, _log, _rfidReader));
        //    Assert.That(_uut._state, Is.EqualTo(new AvailableState(_uut, _door, _chargeControl, _display, _log, _rfidReader)));
        //}


    }

}

    
