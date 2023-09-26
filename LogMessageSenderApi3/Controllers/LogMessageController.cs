using LogMessageSenderApi3.Data;
using LogMessageSenderApi3.Models;
using LogMessageSenderApi3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LogMessageSenderApi3.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LogMessageController : Controller
    {
        private readonly ILogService _logService;

        public LogMessageController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpPost]
        public IActionResult SaveLog(string message)
        {
            try
            {
                _logService.SaveLog($"Manual Log: {message}");
                return Ok();
            }
            catch (Exception e)
            {
                _logService.SaveLog(e.Message);
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult GetLogs() 
        {
            try
            {
                var logs = _logService.GetLogs();
                _logService.SaveLog("Retrieval of logs successful!");
                return Ok(logs);
            }
            catch (Exception e)
            {
                _logService.SaveLog(e.Message);
                return BadRequest();
            }
        }
    }
}
