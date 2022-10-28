using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cabinet_Library.Door;
using NSubstitute;

namespace UnitTests.TestDoor
{
    internal class TestDoor
    {
        private Door _uut;
        private DoorEventArgs _recievedEvent;

        /// <summary>
        /// Test conditions: default door settings
        /// DoorOpened = false
        /// DoorLocked = false
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _recievedEvent = Substitute.For<DoorEventArgs>();
            _uut = new Door();
            _uut.DoorEvent += (o, args) => { _recievedEvent = args; };
        }

        
        
        [Test]
        public void DoorClosed_DoorIsLocked() 
            
        {
            _uut.LockDoor();
            Assert.That(_uut.DoorIsLocked, Is.True);
        }

        [Test]
        public void DoorIsClosed()
        {
          
            _uut.SimulateDoorClosed();
            Assert.That(_uut.DoorIsLocked, Is.False);
        }

        [Test]
        public void DoorIsUnlocked()
        {
            _uut.UnlockDoor();
            Assert.That(_uut.DoorIsLocked, Is.False);
        }

        [Test]
        public void DoorIsOpen()
        {
            _uut.SimulateDoorOpened();
            Assert.That(_uut.DoorIsLocked, Is.False);
        }

        [Test]
        public void DoorIsLockedAgain()
        {
            _uut.UnlockDoor();
            _uut.LockDoor();
            Assert.That(_uut.DoorIsLocked, Is.True);
        }

        [Test]
        public void DoorIsClosedAgain()
        {
            _uut.UnlockDoor();
            _uut.LockDoor();
            Assert.That(_uut.DoorIsLocked, Is.True);
        }

        [Test]
        public void DoorIsUnlockedAndOpen()
        {
            _uut.UnlockDoor();
            _uut.SimulateDoorOpened();
            Assert.That(_uut.DoorIsOpen, Is.True);
            Assert.That(_uut.DoorIsLocked, Is.False);
        }


        [Test]
        public void Door_UnlockDoor_IsUnlocked()
        {
            _uut.UnlockDoor();
            Assert.That(_uut.DoorIsLocked, Is.False);
        }

        [Test]
        public void DoorLocked_OpenDoorFails()
        {
            _uut.LockDoor();
            _uut.SimulateDoorOpened();
            Assert.IsFalse(_uut.DoorIsOpen);
        }

        [Test]
        public void DoorIsLockedAndClosedAgain()
        {
            _uut.SimulateDoorClosed();
            _uut.LockDoor();
            Assert.That(_uut.DoorIsLocked, Is.True);
        }
    }
}
