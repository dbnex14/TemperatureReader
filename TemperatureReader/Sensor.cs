using System;
using System.Collections.Generic;
using System.Threading;

namespace TemperatureReader
{
    public class Sensor : IDisposable, ISensor
    {
        private bool disposed = false;

        // Celsius readings
        private List<double> sensorReader = new List<double>()
        {
            1.5, 1.0, 0.5, 0.0, -0.5
            , 0.0, 0.5, -1.5, 0.0, 2.0
            , 0.0, -1.5, 101.5, 100.0, 99.5
            , 100.0, 102.0, 100.0, 98.5, 100.0
        };

        public Sensor() {}

        public IEnumerable<double> Read(bool doSleep = true)
        {
            foreach (var read in sensorReader)
            {
                if (doSleep)
                {
                    Thread.Sleep(500); 
                }
                yield return read;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // clean managed
                    sensorReader.Clear();
                    sensorReader = null;
                }

                // clean unmanaged
                disposed = true;
            }
        }
    }
}
