using EnglishMasterAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnglishMasterAPI.Controllers
{
    [Route("api/{action}/{controller}")]
    [ApiController]
    public class WordController : ControllerBase
    {
        private DB _db;
        public WordController(DB db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(new ResultContent<List<Word>>
                {
                    Message = "Success",
                    Content = _db.Words.ToList(),
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
