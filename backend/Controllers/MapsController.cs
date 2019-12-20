using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.DataContracts;
using backend.Helpers;
using backend.Models;
using backend.Services.Maps;
using backend.Services.Players;
using backend.Services.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/maps")]
    [ApiController]
    public class MapsController : ResponseController
    {
        private readonly IMapService _mapService;
        private readonly IPlayerService _playerService;

        public MapsController(IMapService mapService, IPlayerService playerService, IResponseService service) : base(service)
        {
            _mapService = mapService;
            _playerService = playerService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] MapObjectsDataContract mapObject)
        {
            Message msg = _mapService.Create(mapObject);
            if (!msg.IsValid)
            {
                return BadRequest(msg);
            }
            msg = _playerService.Add(new Player { UserName = mapObject.Username, MapId = MapResponse.Id});
            if (!msg.IsValid) return BadRequest(msg);
            return Ok(MapResponse);
        }

        [HttpGet]
        public IActionResult Get()
        {
            Message msg = _mapService.Get();
            if (msg.IsNotFound)
            {
                return NotFound();
            }
            return Ok(MapResponse);
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            try
            {
                Message msg = _mapService.DeleteGame();
                if (!msg.IsValid)
                {
                    return BadRequest(msg);
                }
                return StatusCode(202);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}