using System;
using System.Collections.Generic;

namespace EnglishMasterAPI.Models
{
    public partial class Vocabulary
    {
        public Vocabulary()
        {
            ExamResultIncorrects = new HashSet<ExamResultIncorrect>();
            PracticeResults = new HashSet<PracticeResult>();
        }

        public long Id { get; set; }
        public long PartOfSpeechId { get; set; }
        public long WordId { get; set; }
        public string Meaning { get; set; } = null!;
        public long LevelId { get; set; }

        public virtual Level Level { get; set; } = null!;
        public virtual PartOfSpeech PartOfSpeech { get; set; } = null!;
        public virtual Word Word { get; set; } = null!;
        public virtual ICollection<ExamResultIncorrect> ExamResultIncorrects { get; set; }
        public virtual ICollection<PracticeResult> PracticeResults { get; set; }
    }
}
