using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Flyweight
{
    public class ProgramMeasurementFactory
    {
        private static Dictionary<string, ProgramMeasurement> pm = new Dictionary<string, ProgramMeasurement>();

        public static ProgramMeasurement GetMeasurementType(string type)
        {
            ProgramMeasurement p;
            if (pm.ContainsKey(type))
            {
                p = pm.GetValueOrDefault(type);
            }
            else
            {
                switch (type)
                {
                    case "Memory":
                        p = new Memory();
                        break;
                    case "Speed":
                        p = new Speed();
                        break;
                    default:
                        p = null;
                        break;
                }
            }
            return p;
        }

    }
}
