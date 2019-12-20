using backend.DataContracts;
using backend.Helpers;
using backend.Helpers.ChainOfResponsibility;
using backend.Models;
using backend.Services.Items;
using backend.Services.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace backend.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemsController : ResponseController
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService, IResponseService service) : base(service)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Item> items = _itemService.GetAll();
            if (items == null && items.Count == 0)
            {
                return NotFound();
            }
            return Ok(items);
        }

        [HttpPost("itemCreation")]
        public IActionResult CreateItem([FromBody] List<Item> addItem)
        {
            Message msgError = new Message();
            addItem.ForEach(a =>
            {
                if (msgError.IsValid)
                {
                    a.service = _itemService;
                    Message msg = ChainsCreation.AddItem(a);
                    if (msg.IsValid) msgError = msg;
                }
            });
            
            if (msgError.IsValid) return Ok(msgError.MessageText);
            return BadRequest(msgError.MessageText);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ItemBuyDataContract itemBuyObject)
        {
            Message msg = _itemService.Buy(itemBuyObject);
            if (!msg.IsValid)
            {
                return BadRequest(msg);
            }
            return Ok(MapResponse);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Message msg = _itemService.GetById(id);
            if (msg.IsNotFound)
            {
                return NotFound();
            }

            return Ok(MapResponse);
        }
    }
}