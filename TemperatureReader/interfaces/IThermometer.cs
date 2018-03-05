using System;
namespace TemperatureReader
{
    public delegate void TemperatureChangedEventHandler(object sender, TemperatureChangeEventArgs args);

    public interface IThermometer
    {
        event TemperatureChangedEventHandler TemperatureChanged;
        event TemperatureChangedEventHandler TemperatureThresholdChanged;

        double TemperatureCelsius { get; }
        double TemperatureFahrenheit { get; }

        void On(ISensor sensor);
    }
}
