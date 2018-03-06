using System;
using System.Collections.Generic;

namespace TemperatureReader
{
    public enum Direction { Increasing, Decreasing, Either };

    public struct Settings
    {
        public List<double> threshold;
        public Direction direction;
        public double fluctuation;

        public Settings(List<double> threshold, Direction direction, double fluctuation)
        {
            this.threshold = threshold;
            this.direction = direction;
            this.fluctuation = fluctuation;
        }
    }

    public class ThermometerReader : IDisposable, IThermometerReader
    {
        private const double _DELTA = 0.1;
        private const string _READING_MESSAGE = "{0}> temperature {1}C";
        private const string _READING_DIRECTIONAL_THRESHOLD_MESSAGE = "{0}> [{1}] threshold of {2}C";
        private const string _DECREASING = "DECREASING";
        private const string _INCREASING = "INCREASING";

        private bool disposed = false;

        private Guid mId;
        private readonly IThermometer mThermometer;
        private Settings mSettings;
        private bool mTresholdReached = true;
        private PrinterCallback mPrinterCallback;

        public delegate void PrinterCallback(string message);

        public ThermometerReader(IThermometer thermometer, Settings? settings, PrinterCallback callback)
        {
            mId = Guid.NewGuid();  //reader id
            mThermometer = thermometer;
            mSettings = settings.GetValueOrDefault();
            mPrinterCallback = callback;

            if (settings.HasValue && mSettings.threshold != null && mSettings.threshold.Count > 0)
            {
                mThermometer.TemperatureThresholdChanged += OnTemperatureThresholdChange;
            }
            else
            {
                mThermometer.TemperatureChanged += OnTemperatureChange;
            }
        }

        public void OnTemperatureChange(object sender, TemperatureChangeEventArgs args)
        {
            mPrinterCallback(string.Format(_READING_MESSAGE, mId, args.NewTemperature));
        }

        public void OnTemperatureThresholdChange(object sender, TemperatureChangeEventArgs args)
        {
            if (mSettings.threshold.Count > 0)
            {
                foreach (var threshold in mSettings.threshold)
                {
                    if (Math.Abs(args.NewTemperature - threshold) <= _DELTA) // threashold
                    {
                        if (mTresholdReached)
                        {
                            string bearing = (args.OldTemperature - args.NewTemperature > 0) ? _DECREASING : _INCREASING;
                            Print(bearing, args.NewTemperature);
                            mTresholdReached = false;
                        }
                        else
                        {
                            ShowDirectionalThreshold(mSettings.direction, args);
                        }
                    }
                }
            }
        }

        private void ShowDirectionalThreshold(Direction direction, TemperatureChangeEventArgs args)
        {
            switch (direction)
            {
                case Direction.Increasing:
                    if ((Math.Abs(args.OldTemperature - args.NewTemperature) > mSettings.fluctuation)
                        && (args.OldTemperature - args.NewTemperature < 0))
                    {
                        Print(direction.ToString().ToUpper(), args.NewTemperature);
                    }
                    break;
                case Direction.Decreasing:
                    if ((Math.Abs(args.OldTemperature - args.NewTemperature) > mSettings.fluctuation)
                        && (args.OldTemperature - args.NewTemperature > 0))
                    {
                        Print(direction.ToString().ToUpper(), args.NewTemperature);
                    }
                    break;
                default:
                    if (Math.Abs(args.OldTemperature - args.NewTemperature) > mSettings.fluctuation)
                    {
                        string bearing = (args.OldTemperature - args.NewTemperature > 0) ? _DECREASING : _INCREASING;
                        Print(bearing, args.NewTemperature);
                    }
                    break;
            }
        }

        private void Print(string bearing, double reading)
        {
            mPrinterCallback(string.Format(_READING_DIRECTIONAL_THRESHOLD_MESSAGE
                              , mId
                              , bearing
                              , reading));
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
                    // clean managed
                    if (mThermometer != null)
                    {
                        mThermometer.TemperatureChanged -= OnTemperatureChange;
                        mThermometer.TemperatureThresholdChanged -= OnTemperatureThresholdChange;
                    }
                    if (mSettings.threshold != null)
                    {
                        mSettings.threshold.Clear();
                        mSettings.threshold = null;
                    }
                    if (mPrinterCallback != null)
                    {
                        mPrinterCallback = null;
                    }
                }

                // clean unmanaged
                disposed = true;
            }
        }
    }
}
