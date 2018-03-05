using NUnit.Framework;
using Rhino.Mocks;
using System;
using TemperatureReader;

namespace TemperatureReaderTests
{
    [TestFixture()]
    public class ThermometerTests
    {
        IThermometer thermometer;
        ISensor sensor;

        [SetUp]
        public void Setup()
        {
            thermometer = new Thermometer();
            sensor = new Sensor();
        }

        [TearDown]
        public void Teardown()
        {
            thermometer = null;
            sensor = null;
        }

        [Test]
        public void TestThermometerTemperatureChangedCalledXTimes()
        {
            // arrange
            int counter = 0;

            // act
            thermometer.TemperatureChanged += (s, e) =>
            {
                counter++;
            };
            thermometer.On(sensor);

            // assert
            Assert.IsTrue(counter == 20);
        }

        [Test]
        public void TestFinalCelsiusReading()
        {
            // arrange
            double expected = 100.0;
            double delta = 0.001;

            var stubThermometer = MockRepository.GenerateMock<IThermometer>();

            // act
            stubThermometer.Stub(t => t.TemperatureCelsius).Return(expected);

            // assert
            Assert.IsTrue(expected - 100.0 < delta);
        }

        [Test]
        public void TestFinalFahrenheitReading()
        {
            // arrange
            double expected = 100.0;
            double delta = 0.001;

            var stubThermometer = MockRepository.GenerateMock<IThermometer>();

            // act
            stubThermometer.Stub(t => t.TemperatureFahrenheit).Return(expected);

            // assert
            Assert.IsTrue(expected - 100.0 < delta);
        }
    }
}
