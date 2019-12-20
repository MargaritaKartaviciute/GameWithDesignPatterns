using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using backend.Helpers;
using backend.Models;
using backend.Services.Players;
using backend.Services.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/players")]
    [ApiController]
    public class PlayersController : ResponseController
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IResponseService service) : base(service)
        {
            Debug.WriteLine("player controller");

            _playerService = PlayerService.getInstance();

            Debug.WriteLine(_playerService.ToString() + "gautas instance player controller");

        }

        // GET: Places
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var player = _playerService.GetById(id);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _playerService.DeleteById(id);
            if (item == false)
            {
                return BadRequest(new { message = "Deletion failed" });
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody]  Player item)
        {
            Message msg = _playerService.Add(item);
            if (!msg.IsValid)
            {
                return BadRequest(msg);
            }
            return Ok(MapResponse);
        }

        [HttpPut("{username}")]
        public IActionResult Move([FromBody] Player player, string username)
        {
            Message msg = _playerService.Move(username, player);
            if (!msg.IsValid)
            {
                return BadRequest(msg);
            }
            return Ok(MapResponse);
        }
    }
}