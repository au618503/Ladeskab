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

        private readonly StationControl _uut;
        private IDoor _door;
        private IChargeControl _chargeControl;
        private IDisplay _display;
        private ILogger _log;
        private IRfIdReader _rfidReader;


        [SetUp]
        public void Setup()
        {
            _door = Substitute.For<IDoor>();
            _chargeControl = Substitute.For<IChargeControl>();
            _display = Substitute.For<IDisplay>();
            _rfidReader = Substitute.For<IRfIdReader>();
            //_uut = new StationControl(IChargeControl chargeControl, IDisplay display, IDoor door, IStationControl stationControl);


        }

        [Test]
        public void TestStationControl_DoorOpen_true()
        {
            _door.DoorIsOpen.Returns(true);

            Assert.That(_door.DoorIsOpen, Is.EqualTo(true));
        }
        [Test]
        public void TestStationControl_DoorOpen_false()
        {
            _door.DoorIsOpen.Returns(false);

            Assert.That(_door.DoorIsOpen, Is.EqualTo(false));
        }



    }

}

    
