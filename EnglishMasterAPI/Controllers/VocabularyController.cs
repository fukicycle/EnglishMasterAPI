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

        [HttpPost]
        public IActionResult Post([FromHeader] string token, List<VocabularyData> vocabularyDatas)
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
                var partOfSpeeches = _db.PartOfSpeeches.ToList();
                if (vocabularyDatas.Any(a => !partOfSpeeches.Select(b => b.Id).Contains(a.PartOfSpeechID)))
                {
                    string invalidIds = string.Join(",", vocabularyDatas.Where(a => !partOfSpeeches.Select(b => b.Id).Contains(a.PartOfSpeechID)).Select(a => a.PartOfSpeechID).ToList());
                    return Ok(new ResultContent<string>
                    {
                        Message = "Validation failed.",
                        Content = $"Invalid PartOfSpeechID is found. Invalid id:{invalidIds}",
                        StatusCode = System.Net.HttpStatusCode.BadRequest
                    });
                }
                var levels = _db.Levels.ToList();
                if (vocabularyDatas.Any(a => !levels.Select(b => b.Id).Contains(a.LevelID)))
                {
                    string invalidIds = string.Join(",", vocabularyDatas.Where(a => !levels.Select(b => b.Id).Contains(a.LevelID)).Select(a => a.LevelID).ToList());
                    return Ok(new ResultContent<string>
                    {
                        Message = "Validation failed.",
                        Content = $"Invalid LevelID is found. Invalid id:{invalidIds}",
                        StatusCode = System.Net.HttpStatusCode.BadRequest
                    });
                }
                int count = 0;
                int wordCount = 0;
                foreach (var vocaburalyData in vocabularyDatas)
                {
                    Word? word = _db.Words.FirstOrDefault(a => a.Word1.ToUpper().Equals(vocaburalyData.Word.ToUpper()));
                    if (word is null)
                    {
                        word = new Word
                        {
                            Word1 = vocaburalyData.Word.ToLower()
                        };
                        _db.Words.Add(word);
                        _db.SaveChanges();
                        wordCount++;
                    }
                    Vocabulary vocabulary = new Vocabulary
                    {
                        PartOfSpeechId = vocaburalyData.PartOfSpeechID,
                        LevelId = vocaburalyData.LevelID,
                        WordId = word.Id,
                        Meaning = vocaburalyData.Meaning
                    };
                    _db.Vocabularies.Add(vocabulary);
                    count++;
                }
                _db.SaveChanges();
                return Ok(new ResultContent<string>
                {
                    Message = "Success",
                    Content = $"Adding {count} vocabularies and {wordCount} words.",
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
