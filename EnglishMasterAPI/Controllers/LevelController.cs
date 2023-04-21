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
    }
}
