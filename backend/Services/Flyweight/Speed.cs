using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Flyweight
{
    public class Speed : ProgramMeasurement
    {
        private System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();

        public void Measure()
        {
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
        }

        public void Start()
        {
            if (!watch.IsRunning)
                watch.Restart();
        }
        public void Stop()
        {
            watch.Stop();
        }
    }
}
