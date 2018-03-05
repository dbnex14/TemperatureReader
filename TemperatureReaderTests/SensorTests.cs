using NUnit.Framework;
using TemperatureReader;
using Rhino.Mocks;
using System;
using System.Collections.Generic;

namespace TemperatureReaderTests
{
    [TestFixture()]
    public class SensorTests
    {
        ISensor sensor;

        [Test]
        [SetUp]
        public void Setup()
        {
            sensor = new Sensor();
            Assert.IsNotNull(sensor);
        }

        [TearDown]
        public void Teardown()
        {
            sensor = null;
        }

        [Test()]
        public void SensorInitTest()
        {
            ISensor s = new Sensor();
            Assert.IsNotNull(s);
        }

        [Test]
        public void SensorReadTest()
        {
            int count = 0;
            foreach(var s in sensor.Read(false))
            {
                count++;
            }
            Assert.IsTrue(count == 20);
        }

    }
}
