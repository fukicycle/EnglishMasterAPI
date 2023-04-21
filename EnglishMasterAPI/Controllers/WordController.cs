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

        [HttpPost]
        public IActionResult Post([FromHeader] string token, [FromBody] List<string> words)
        {
            try
            {
                var auth = _db.Users.FirstOrDefault(a => a.Token == token);
                if (auth is null)
                {
                    return Ok(new ResultContent<string>
                    {
                        Message = "Authentication failed.",
                        Content = "Invalid credential",
                        StatusCode = System.Net.HttpStatusCode.Unauthorized
                    });
                }
                int count = 0;
                int depCount = 0;
                foreach (var word in words)
                {
                    if (_db.Words.Any(a => a.Word1.ToUpper().Equals(word.ToUpper())))
                    {
                        depCount++;
                        continue;
                    }
                    else
                    {
                        _db.Words.Add(new Word
                        {
                            Word1 = word,
                        });
                        count++;
                    }
                }
                _db.SaveChanges();
                return Ok(new ResultContent<string>
                {
                    Message = "Success",
                    Content = $"Adding {count} words. Depulicate {depCount}",
                    StatusCode = System.Net.HttpStatusCode.Created
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
