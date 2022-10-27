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
        private DoorEventArgs _dooreventargs;
        private IDoor _uut;

        [SetUp]
        public void Setup()
        {

            //SetUp for Door
            _uut = Substitute.For<IDoor>();
            _dooreventargs = new DoorEventArgs() { IsOpen = true };
            
            

        }

        [Test]
        public void TestDoor_StateIsChanging()
        {
            _uut.DoorEvent += Raise.EventWith(_dooreventargs);
            
            Assert.That(_uut.DoorIsOpen(), Is.EqualTo(DoorStateID.CHANGING));

        }


       

   
}
