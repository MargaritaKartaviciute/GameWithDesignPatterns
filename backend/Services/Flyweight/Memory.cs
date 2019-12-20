using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Flyweight
{
    public class Memory : ProgramMeasurement
    {
        private long _kbAtExecution;
        private long _kbAfter1;
        public void Measure()
        {
            Console.WriteLine(_kbAtExecution + " Started with this kb.");
            Console.WriteLine(_kbAfter1 + " After the test.");
            Console.WriteLine(_kbAfter1 - _kbAtExecution + " Amt. Added.");
        }

        public void Start()
        {
            _kbAtExecution = GC.GetTotalMemory(false) / 1024;
        }

        public void Stop()
        {
            _kbAfter1 = GC.GetTotalMemory(false) / 1024;
        }
  
    }
}
