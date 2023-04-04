using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using Gaming1.Api.Contracts.Ping;

namespace Gaming1.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PingController : ControllerBase
    {

        private readonly ILogger<PingController> _logger;

        public PingController(ILogger<PingController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PingResult), (int)HttpStatusCode.OK)]
        public IActionResult Ping()
        {
            _logger.LogDebug("Ping request received.");
            return Ok(new PingResult { ServerTime = DateTime.UtcNow });
        }
    }
}