using EnglishMasterAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnglishMasterAPI.Controllers
{
    [Route("api/[action]/[controller]")]
    [ApiController]
    public class PartOfSpeechController : ControllerBase
    {
        private DB _db;
        public PartOfSpeechController(DB db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(new ResultContent<List<PartOfSpeech>>
                {
                    Message = "Success",
                    Content = _db.PartOfSpeeches.ToList(),
                    StatusCode = System.Net.HttpStatusCode.OK
                });
            }
            catch (Exception ex)
            {
                return Ok(new ResultContent<string>
                {
                    Message = "Internal Server Error",
                    Content = ex.Message,
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                });
            }
        }
    }
}
