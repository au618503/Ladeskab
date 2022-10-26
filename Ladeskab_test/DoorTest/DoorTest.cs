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
            _uut = new DoorSimulator();
        }

        [Test]
        public void DoorOpen()
        {
            _uut.DoorOpen();
            Assert.That(_uut.DoorOpen, Is.True);
        }

        [Test]
        public void DoorClose()
        {
            _uut.DoorClose();
            Assert.That(_uut.DoorOpen, Is.False);
        }
        [Test]
        public void DoorOpenEvent()
        {
            bool eventRaised = false;
            _uut.DoorOpenEvent += (o, args) => eventRaised = true;
            _uut.DoorOpen();
            Assert.That(eventRaised, Is.True);
        }

        [Test]
        public void DoorCloseEvent()
        {
            bool eventRaised = false;
            _uut.DoorCloseEvent += (o, args) => eventRaised = true;
            _uut.DoorClose();
            Assert.That(eventRaised, Is.True);
        }


    }
}

