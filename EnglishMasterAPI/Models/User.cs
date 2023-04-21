using System;
using System.Collections.Generic;

namespace EnglishMasterAPI.Models
{
    public partial class User
    {
        public User()
        {
            ExamResults = new HashSet<ExamResult>();
            PracticeResults = new HashSet<PracticeResult>();
        }

        public long Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Icon { get; set; }

        public virtual ICollection<ExamResult> ExamResults { get; set; }
        public virtual ICollection<PracticeResult> PracticeResults { get; set; }
    }
}
