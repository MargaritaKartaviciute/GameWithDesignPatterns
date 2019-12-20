using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Players
{
    public interface ICutObjects
    {
        void Cut(string username, int objectId);
    }
}
