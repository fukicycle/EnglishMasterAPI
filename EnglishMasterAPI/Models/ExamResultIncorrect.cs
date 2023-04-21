using System;
using System.Collections.Generic;

namespace EnglishMasterAPI.Models
{
    public partial class ExamResultIncorrect
    {
        public long Id { get; set; }
        public long ExamResultId { get; set; }
        public long VocabularyId { get; set; }

        public virtual ExamResult ExamResult { get; set; } = null!;
        public virtual Vocabulary Vocabulary { get; set; } = null!;
    }
}
