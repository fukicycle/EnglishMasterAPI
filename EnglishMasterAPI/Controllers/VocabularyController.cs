using EnglishMasterAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnglishMasterAPI.Controllers
{
    [Route("api/[action]/[controller]")]
    [ApiController]
    public class VocabularyController : ControllerBase
    {
        private DB _db;

        public VocabularyController(DB db)
        {
            _db = db;
        }

    }
}
