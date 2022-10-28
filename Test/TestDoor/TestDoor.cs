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

        [SetUp]
        public void Setup()
        {
            _recievedEvent = Substitute.For<DoorEventArgs>();
            _uut = new Door();
            _uut.DoorEvent += (o, args) => { _recievedEvent = args; };
        }

        
        
        [Test]
        public void DoorIsLocked() 
            
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
        public void DoorIsUnlockedAndClosed()
        {
            _uut.UnlockDoor();
            _uut.SimulateDoorClosed();
            Assert.That(_uut.UnlockDoor, Is.True);
            Assert.That(_uut.DoorIsLocked, Is.True);
        }

        [Test]
        public void DoorIsLockedAndClosedAgain()
        {
            _uut.LockDoor();
            _uut.SimulateDoorClosed();
            Assert.That(_uut.LockDoor, Is.True);
            Assert.That(_uut.SimulateDoorClosed, Is.True);
        }
    }
}
