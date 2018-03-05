using System;

namespace TemperatureReader
{
    class MainClass
    {
        private const string _FINAL_READING = "\nFinal Temperature: {0}C / {1}F";

        public static void Main(string[] args)
        {
            using(Sensor sensor = new Sensor())
            using (Thermometer thermometer = new Thermometer())
            {
                thermometer.On(sensor);
                Console.WriteLine(_FINAL_READING, thermometer.TemperatureCelsius, thermometer.TemperatureFahrenheit);
            }

            Console.ReadLine();
        }
    }
}
