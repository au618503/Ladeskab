using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab_biblio.ChargeControl;
using Ladeskab_biblio.ChargeControl.States;
using Ladeskab_biblio.Display;
using Ladeskab_biblio.StationControl;
using NSubstitute;

namespace Ladeskab_test.ChargeControlTests
{
    public class TestChargeControlInput
    {
        private IDisplay _display = Substitute.For<IDisplay>();
        private IStationControl _stationControl = Substitute.For<IStationControl>();
        private IUsbCharger _usbCharger;
        private ChargeControl _uut;
        [SetUp]
        public void Setup()
        {
            _usbCharger = Substitute.For<IUsbCharger>();
            _uut = new ChargeControl(_usbCharger);
            _usbCharger.CurrentValueEvent += _uut.OnCurrentEvent;
        }

        [Test]
        public void TestCharging_StateIsCharging()
        {
            _usbCharger.Connected.Returns(true);
            _usbCharger.CurrentValue.Returns(6);
            _uut.StartCharge();
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = 6 });
            Assert.AreEqual( StateID.CHARGING, _uut.GetState());
        }

        [Test]
        public void TestCharging_CurrentTooHigh_StateIsError()
        {
            _usbCharger.Connected.Returns(true);
            _uut.StartCharge();
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = 530 });
            Assert.AreEqual(StateID.ERROR, _uut.GetState());
        }
    }
}
