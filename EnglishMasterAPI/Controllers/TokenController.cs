using EnglishMasterAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace EnglishMasterAPI.Controllers
{
    [Route("api/[action]/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private DB _db;

        public TokenController(DB db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get(User user)
        {
            try
            {
                var auth = _db.Users.FirstOrDefault(a => a.Username == user.Username && a.Password == user.Password);
                if (auth is null)
                    return Ok(new ResultContent<string>
                    {
                        Message = "Authentication failed",
                        Content = "Invalid credential",
                        StatusCode = System.Net.HttpStatusCode.Unauthorized
                    });
                return Ok(new ResultContent<string>
                {
                    Message = "Success",
                    Content = GenerateToken(auth.Id),
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

        public string GenerateToken(long id)
        {
            StringBuilder sb = new StringBuilder();
            for (int len = 0; len < 64; len++)
            {
                if (Random.Shared.Next() % 2 == 0)
                {
                    sb.Append(Convert.ToChar(Random.Shared.Next(65, 90)));
                }
                else
                {
                    sb.Append(Convert.ToChar(Random.Shared.Next(97, 122)));
                }
            }
            _db.Users.Find(id)!.Token = sb.ToString();
            _db.SaveChanges();
            return sb.ToString();
        }
    }
}
