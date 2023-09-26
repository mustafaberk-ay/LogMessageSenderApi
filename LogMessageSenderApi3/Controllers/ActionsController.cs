using LogMessageSenderApi3.Models;
using LogMessageSenderApi3.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LogMessageSenderApi3.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ActionsController : Controller
    {
        private readonly ILogService _logService;
        public ActionsController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet]
        public IActionResult RespondOk()
        {
            _logService.SaveLog("OK");
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetRandomQuote()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string randomQuoteUrl = "https://api.quotable.io/quotes/random";
                    var response = await httpClient.GetAsync(randomQuoteUrl);
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    var quoteList = JsonConvert.DeserializeObject<List<Quote>>(content);
                    var quote = quoteList.FirstOrDefault().Content;
                    _logService.SaveLog($"Random Quote: {quote}");
                    return Ok(quote);
                }
            }
            catch (Exception e)
            {
                _logService.SaveLog(e.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Divide(DivisionRequest request)
        {
            if(request.Divisor == 0)
            {
                _logService.SaveLog("Divide: Division by zero is not allowed.");
                return BadRequest();
            }

            var result = request.Dividend / request.Divisor;
            _logService.SaveLog($"Divide: result is {result}");
            return Ok(result);
        }
    }
}
