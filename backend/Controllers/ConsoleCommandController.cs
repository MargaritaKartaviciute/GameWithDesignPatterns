using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Services.Interpreter;
using backend.Services.Players;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsoleCommandController : ControllerBase
    {
        private readonly IPlayerService playerService;

        public ConsoleCommandController(IPlayerService playerService)
        {
            this.playerService = playerService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] String interpret)
        {
            try
            {
                var commands = interpret.Split(",");
                InterpreterContext context = new InterpreterContext(playerService);
                String output = "";
                foreach (var stringCommand in commands)
                {
                    string stringCommandTrimmed = stringCommand.Trim();
                    var splitted = stringCommandTrimmed.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    Amount amount = null;
                    if (splitted.Length > 2)
                    {
                        amount = new Amount(int.Parse(splitted[2].Trim()));
                    }
                    Command command = new Command(splitted[0].Trim(), new Receiver(splitted[1].Trim(), amount));
                    output += "\n" + command.Interpret(context);
                }
                return Ok(output);
            }
            catch (Exception e)
            {
                return NotFound("Unable to interpret your syntax");
            }
        }
    }
}