using Microsoft.AspNetCore.Mvc;

namespace RFIDlock.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LockController : ControllerBase
    {
        // POST: api/lock/lock
        [HttpPost("lock")]
        public IActionResult LockDoor()
        {
            // Simulate locking logic
            // In real case, you'd interface with the Arduino or hardware API
            return Ok(new { status = "locked", message = "Door has been locked." });
        }

        // POST: api/lock/unlock
        [HttpPost("unlock")]
        public IActionResult UnlockDoor()
        {
            // Simulate unlocking logic
            return Ok(new { status = "unlocked", message = "Door has been unlocked." });
        }

        // GET: api/lock/status
        [HttpGet("status")]
        public IActionResult GetDoorStatus()
        {
            // Placeholder: you could connect this to actual RFID door status
            return Ok(new { status = "locked" }); // or "unlocked"
        }
    }
}
