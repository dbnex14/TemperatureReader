using System;
namespace TemperatureReader
{
    public class TemperatureChangeEventArgs : EventArgs
    {
        public double OldTemperature { get; }
        public double NewTemperature { get; }

        public TemperatureChangeEventArgs(double oldTemperature, double newTemperature)
        {
            OldTemperature = oldTemperature;
            NewTemperature = newTemperature;
        }
    }
}
