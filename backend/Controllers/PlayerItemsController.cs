using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Helpers;
using backend.Services.PlayerItems;
using backend.Services.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/playersItems")]
    [ApiController]
    public class PlayerItemsController : ResponseController
    {
        private readonly IPlayerItemsService _playerItemsService;

        public PlayerItemsController(IPlayerItemsService playerItemsService, IResponseService service) : base(service)
        {
            _playerItemsService = playerItemsService;
        }
        [HttpGet("{userId}")]
        public IActionResult Get(string username)
        {
            Message msg = _playerItemsService.GetItemsByUser(username);
            if (msg.IsNotFound)
            {
                return NotFound();
            }
            return Ok(MapResponse);
        }
        [HttpPost("{username}/item/{itemId}")]
        public IActionResult Create(string username, int itemId)
        {
            Message msg = _playerItemsService.AddPlayerItem(username, itemId);
            if (!msg.IsValid)
            {
                return BadRequest(msg);
            }
            return Ok(MapResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, int eventId)
        {
            var item = _playerItemsService.DeletePlayerItem(id);
            if (item == false)
            {
                return BadRequest(new { message = "Deletion failed" });
            }

            return Ok(item);
        }
    }
}