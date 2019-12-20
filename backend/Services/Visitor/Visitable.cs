using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Visitor
{
    public interface Visitable
    {
        int accept(Visitor visitor);
    }
}
