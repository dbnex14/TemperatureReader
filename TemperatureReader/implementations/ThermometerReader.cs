using System;
namespace TemperatureReader
{
    public class ThermometerReader : IDisposable, IThermometerReader
    {
        private bool disposed = false;

        public ThermometerReader()
        {
            // TODO pass thermometer in and subscribe to events

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void OnTemperatureChange(object sender, TemperatureChangeEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnTemperatureThresholdChange(object sender, TemperatureChangeEventArgs args)
        {
            throw new NotImplementedException();
        }

        protected void Dispose(bool disposing)
        {
            if (!disposing)
            {
                if (disposed)
                {
                    // clear managed
                    //TODO clean
                }

                // clear unmanaged
                disposed = true;
            }
        }
    }
}
