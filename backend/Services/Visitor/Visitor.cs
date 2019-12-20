using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Visitor
{
    public interface Visitor
    {
        int visit(Tree item);
        int visit(Rock item);
        int visit(Water item);
    }
}
