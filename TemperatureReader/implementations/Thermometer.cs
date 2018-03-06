using System;
namespace TemperatureReader
{
    public class Thermometer : IDisposable, IThermometer
    {
        private bool disposed = false;

        private const double _FACTOR = 1.8;
        private const int _SCALE = 32;
        private const double _DELTA = 0.4;
        private double mTemperatureC;
        private Guid mId;

        public event TemperatureChangedEventHandler TemperatureChanged;
        public event TemperatureChangedEventHandler TemperatureThresholdChanged;

        public Thermometer() 
        {
            mId = Guid.NewGuid();
        }

        public double TemperatureCelsius
        {
            get
            {
                return mTemperatureC;
            }
        }

        public double TemperatureFahrenheit
        {
            get
            {
                return (TemperatureCelsius * _FACTOR) + _SCALE;
            }
        }

        private void SetTemperatureCelsius(double reading)
        {
            if (Math.Abs(mTemperatureC - reading) > _DELTA)
            {
                TemperatureChangeEventArgs args = new TemperatureChangeEventArgs(mTemperatureC, reading);
                mTemperatureC = reading;
                OnTemperatureChanged(args);
            }
        }

        public void On(ISensor sensor)
        {
            foreach (double reading in sensor.Read())
            {
                SetTemperatureCelsius(reading);
            }
        }

        virtual protected void OnTemperatureChanged(TemperatureChangeEventArgs args)
        {
            if (TemperatureChanged != null)
            {
                TemperatureChanged(this, args);
            }

            if (TemperatureThresholdChanged != null)
            {
                TemperatureThresholdChanged(this, args);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // clear managed
                    TemperatureChanged = null;
                    TemperatureThresholdChanged = null;
                }

                // clear unmanaged
                disposed = true;
            }
        }
    }
}
