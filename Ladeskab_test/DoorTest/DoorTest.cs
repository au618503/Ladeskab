using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ladeskab_biblio;
using Ladeskab_biblio.Door;

namespace Ladeskab_test.DoorTest

{
    [TestFixture]
    public class DoorTest
    {
        private Door _uut;


        [SetUp]
        public void Setup()
        {
            _uut = new Door();
        }

        [Test]
        public void DoorIsOpen()
        {
             _uut.OnDoorOpen();
            Assert.That(_uut.OnDoorOpen, Is.True);
        }

        [Test]
        public void DoorIsLocked()
            
        {
            _uut.LockDoor();
            Assert.That(_uut.DoorIsLocked, Is.True);
        }

        [Test]
        public void DoorIsUnlocked()
        {
            _uut.UnlockDoor();
            Assert.That(_uut.DoorIsLocked, Is.False);
        }

        [Test]
        public void DoorIsClosed()
        {
            _uut.OnDoorClose();
            Assert.That(_uut.DoorIsOpen, Is.False);
        }

        [Test]
        public void DoorIsClosedAndLocked()
        {
            _uut.OnDoorClose();
            _uut.LockDoor();
            Assert.That(_uut.DoorIsOpen, Is.False);
            Assert.That(_uut.DoorIsLocked, Is.True);
        }
    }
}

