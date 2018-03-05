using NUnit.Framework;
using System;
using TemperatureReader;

namespace TemperatureReaderTests
{
    [TestFixture]
    public class TemperatureReaderTests
    {
        [SetUp]
        public void Setup()
        {
            IThermometerReader reader = new ThermometerReader();
        }
    }
}
