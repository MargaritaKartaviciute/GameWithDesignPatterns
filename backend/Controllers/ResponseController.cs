using backend.Models;
using backend.Services.Response;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    public class ResponseController : ControllerBase
    {
        private IResponseService _service;
        public ResponseController(IResponseService service)
        {
            _service = service;
        }

        public Map MapResponse
        {
            get
            {
                return _service.GetResponse();
            }
        }
    }
}
