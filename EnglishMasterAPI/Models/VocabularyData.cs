namespace EnglishMasterAPI.Models
{
    public class VocabularyData
    {
        public long PartOfSpeechID { get; set; }
        public string Word { get; set; } = null!;
        public string Meaning { get; set; } = null!;
        public long LevelID { get; set; }
    }
}
