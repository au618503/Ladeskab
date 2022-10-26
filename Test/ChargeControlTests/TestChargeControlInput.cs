using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cabinet_Library.ChargeControl;
using Cabinet_Library.ChargeControl.States;
using Cabinet_Library.Display;
using Cabinet_Library.StationControl;
using NSubstitute;

namespace UnitTests.ChargeControlTests
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
            _uut = new ChargeControl(_usbCharger, _display);
        }

        [Test]
        public void TestCharging_StateIsCharging()
        {
            _usbCharger.Connected.Returns(true);
            _usbCharger.CurrentValue.Returns(6);
            _uut.StartCharge();
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = 6 });
            Assert.AreEqual(StateID.CHARGING, _uut.GetState());
        }

        [Test]
        public void Test_EventRaised_DisplayCalledSetCText()
        {
            _uut.StartCharge();
            CurrentEventArgs testargs = new CurrentEventArgs() { Current = 10 };
            _usbCharger.CurrentValueEvent += Raise.EventWith(testargs);
            _display.ReceivedWithAnyArgs().SetChargingText(default);
        }

        [Test]
        public void TestCharging_CurrentTooHigh_UsbChargerStopped()
        {
            _usbCharger.Connected.Returns(true);
            _uut.StartCharge();
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = 530 });
            _usbCharger.Received().StopCharge();
        }
    }
}
