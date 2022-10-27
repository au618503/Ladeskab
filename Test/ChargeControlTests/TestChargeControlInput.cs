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
        private IDisplay _display;
        private IStationControl _stationControl = Substitute.For<IStationControl>();
        private IUsbCharger _usbCharger;
        private ChargeControl _uut;
        [SetUp]
        public void Setup()
        {
            _display = Substitute.For<IDisplay>();
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
            Assert.That(_uut.GetState(), Is.EqualTo(StateID.CHARGING));
        }
        [Test]
        public void Test_EventRaised_DisplayCalledSetChargingText_StateCharging()
        {
            _usbCharger.Connected.Returns(true);
            _uut.StartCharge();
            CurrentEventArgs testargs = new CurrentEventArgs() { Current = 6 };
            _usbCharger.CurrentValueEvent += Raise.EventWith(testargs);
            _display.Received().SetChargingText("Charging...");
        }

        [Test]
        public void TestCharging_CurrentTooHigh_StateIsError()
        {
            _usbCharger.Connected.Returns(true);
            _uut.StartCharge();
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = 530 });
            Assert.That(_uut.GetState(), Is.EqualTo(StateID.ERROR));
        }

        [Test]
        public void Test_EventRaised_DisplayCalledSetChargingText_StateError()
        {
            _usbCharger.Connected.Returns(true);
            _uut.StartCharge();
            CurrentEventArgs testargs = new CurrentEventArgs() { Current = 530 };
            _usbCharger.CurrentValueEvent += Raise.EventWith(testargs);
            _display.Received().SetChargingText("Charging error. Contact support.");
        }
        
        [Test]
        public void TestCharging_StateIsFullyCharged()
        {
            _usbCharger.Connected.Returns(true);
            _uut.StartCharge();
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = 0 });
            Assert.That(_uut.GetState(), Is.EqualTo(StateID.FULLY_CHARGED));
        }

        [Test]
        public void Test_EventRaised_DisplayCalledSetChargingText_StateFullyCharged()
        {
            _usbCharger.Connected.Returns(true);
            _uut.StartCharge();
            CurrentEventArgs testargs = new CurrentEventArgs() { Current = 0 };
            _usbCharger.CurrentValueEvent += Raise.EventWith(testargs);
            _display.Received().SetChargingText("Device fully charged.");
        }

        [Test]
        public void TestCharging_CurrentTooLow_StateIsReady()
        {
            _usbCharger.Connected.Returns(false);
            _uut.StartCharge();
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = 0 });
            Assert.That(_uut.GetState(), Is.EqualTo(StateID.READY));
        }

        [Test]
        public void Test_EventRaised_DisplayCalledSetChargingText_StateReady()
        {
            _usbCharger.Connected.Returns(false);
            _uut.StartCharge();
            CurrentEventArgs testargs = new CurrentEventArgs() { Current = 0 };
            _usbCharger.CurrentValueEvent += Raise.EventWith(testargs);
            _display.Received().SetChargingText("");
        }

        // Test of StopCharge()
        [Test]
        public void TestCharging_NotDoneCharging_UsbChargerNotStopped()
        {
            _usbCharger.Connected.Returns(true);
            _uut.StartCharge();
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = 6 });
            _usbCharger.DidNotReceive().StopCharge();
        }

        [Test]
        public void TestCharging_CurrentTooHigh_UsbChargerStopped()
        {
            _usbCharger.Connected.Returns(true);
            _uut.StartCharge();
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = 530 });
            _usbCharger.Received().StopCharge();
        }

        [Test]
        public void TestCharging_NotConnected_UsbChargerStopped()
        {
            _usbCharger.Connected.Returns(false);
            _uut.StartCharge();
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = 0 });
            _usbCharger.Received().StopCharge();
        }

        // Test af StartCharge()
        [Test]
        public void TestCharging_UsbChargerStarted()
        {
            _usbCharger.Connected.Returns(true);
            _uut.StartCharge();
            _usbCharger.Received().StartCharge();
        }
    }
}
