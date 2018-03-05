using System;
namespace TemperatureReader
{
    public interface IThermometerReader
    {
        void OnTemperatureChange(object sender, TemperatureChangeEventArgs args);
        void OnTemperatureThresholdChange(object sender, TemperatureChangeEventArgs args);
    }
}
