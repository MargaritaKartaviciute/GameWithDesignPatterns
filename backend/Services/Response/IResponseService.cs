using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Response
{
    public interface IResponseService
    {
        Map GetResponse();
    }
}
