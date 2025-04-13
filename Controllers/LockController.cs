using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace RFIDlock.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LockController : ControllerBase
    {
        private static readonly ConcurrentDictionary<string, string> _commands = new();

        // POST: api/lock/lock
        [HttpPost("lock")]
        public IActionResult LockDoor()
        {
            return Ok(new { status = "locked", message = "Door has been locked." });
        }

        // POST: api/lock/unlock
        [HttpPost("unlock")]
        public IActionResult UnlockDoor()
        {
            return Ok(new { status = "unlocked", message = "Door has been unlocked." });
        }

        // GET: api/lock/status
        [HttpGet("status")]
        public IActionResult GetDoorStatus()
        {
            return Ok(new { status = "locked" }); // Placeholder
        }

        // GET: api/lock/command?mac=ESP123
        [HttpGet("command")]
        public IActionResult GetCommand([FromQuery] string mac)
        {
            if (string.IsNullOrWhiteSpace(mac))
                return BadRequest("MAC address is required.");

            if (_commands.TryGetValue(mac, out string command))
                return Ok(command);

            return Ok("NONE");
        }

        // POST: api/lock/command (JSON body: { "mac": "ESP123", "command": "LOCK" })
        [HttpPost("command")]
        public IActionResult SetCommand([FromBody] CommandRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Mac) || string.IsNullOrWhiteSpace(request.Command))
                return BadRequest("Missing MAC or command.");

            _commands[request.Mac] = request.Command.ToUpper();
            return Ok(new { success = true, message = $"Command '{request.Command}' set for {request.Mac}" });
        }

        public class CommandRequest
        {
            public string Mac { get; set; }
            public string Command { get; set; }
        }
    }
}