using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Flyweight
{
    public interface ProgramMeasurement
    {
        void Start();
        void Stop();
        void Measure();
    }
}
