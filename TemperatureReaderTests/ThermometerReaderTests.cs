using NUnit.Framework;
using Rhino.Mocks;
using System;
using TemperatureReader;

namespace TemperatureReaderTests
{
    [TestFixture]
    public class ThermometerReaderTests
    {
        //IThermometerReader reader;
        MockRepository mock;

        [SetUp]
        public void Setup()
        {
            //reader = MockRepository.GenerateStrictMock<IThermometerReader>();
            mock = new MockRepository();
        }

        [TearDown]
        public void Teardown()
        {
            //reader = null;
            mock = null;
        }

        [Test]
        public void TestOnTemperatureChangedCalled()
        {
            // arrange
            IThermometerReader r = mock.DynamicMock<IThermometerReader>();
            Expect.Call(() => r.OnTemperatureChange(null, null));
            mock.ReplayAll();

            // act
            r.OnTemperatureChange(null, null);

            // assert
            mock.VerifyAll();
        }

        [Test]
        public void TestOnTemperatureThresholdChange()
        {
            IThermometerReader r = mock.DynamicMock<IThermometerReader>();
            Expect.Call(() => r.OnTemperatureThresholdChange(null, null));
            mock.ReplayAll();

            // act
            r.OnTemperatureThresholdChange(null, null);

            // assert
            mock.VerifyAll();
        }
    }
}
