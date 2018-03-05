﻿using System;
using System.Collections.Generic;

namespace TemperatureReader
{
    /*
     * Dino Buljubasic
     * EncoreFX
     */
    class MainClass
    {
        private const string _FINAL_READING = "\nFinal Temperature: {0}C / {1}F";

        public static void Main(string[] args)
        {
            //
            // Create 2 readers:
            //  - one without settings observing all temperature changes,
            //  - one with settings interested only in threshold values and direction.
            //
            using (Sensor sensor = new Sensor())
            using (Thermometer thermometer = new Thermometer())
            using (ThermometerReader reader = new ThermometerReader(
                "R1"
                , thermometer
                , null
                , Print))
            using (ThermometerReader readerWithSettings = new ThermometerReader(
                "RS1"
                , thermometer
                , new Settings(new List<double>() { 0.0, 100.0 }, Direction.Either, 0.5)
                , Print))
            {
                thermometer.On(sensor);
                Print(string.Format(_FINAL_READING, thermometer.TemperatureCelsius, thermometer.TemperatureFahrenheit));
            }

            Console.ReadLine();
        }

        static void Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}
