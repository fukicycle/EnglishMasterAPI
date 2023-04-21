using EnglishMasterAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnglishMasterAPI.Controllers
{
    [Route("api/[action]/[controller]")]
    [ApiController]
    public class LevelController : ControllerBase
    {
        private DB _db;
        public LevelController(DB db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(new ResultContent<List<Level>>
                {
                    Message = "Success",
                    Content = _db.Levels.ToList(),
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
        public IActionResult Post([FromHeader] string token, Level level)
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
                if (!_db.Levels.Any(a => a.Name.ToUpper().Equals(level.Name.ToUpper())))
                {
                    _db.Levels.Add(level);
                    _db.SaveChanges();
                    return Ok(new ResultContent<string>
                    {
                        Message = "Success",
                        Content = "Added",
                        StatusCode = System.Net.HttpStatusCode.Created
                    });
                }
                return Ok(new ResultContent<string>
                {
                    Message = "Already added.",
                    Content = "",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
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
