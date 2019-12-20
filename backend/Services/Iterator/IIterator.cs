using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Iterator
{
    interface IIterator<T>
    {
        T Next();
        bool HasNext();
        void Reset();
    }
}
