using System;
using System.Collections.Generic;

namespace TemperatureReader
{
    public interface ISensor
    {
        IEnumerable<double> Read(bool doSleep = true);
    }
}
