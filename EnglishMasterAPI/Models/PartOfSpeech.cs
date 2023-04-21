using System;
using System.Collections.Generic;

namespace EnglishMasterAPI.Models
{
    public partial class PartOfSpeech
    {
        public PartOfSpeech()
        {
            Vocabularies = new HashSet<Vocabulary>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string InJapanese { get; set; } = null!;

        public virtual ICollection<Vocabulary> Vocabularies { get; set; }
    }
}
