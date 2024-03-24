using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PsycoTest.Models
{
    public partial class Answer
    {
        public int AnswerId { get; set; }

        public int AnswerQuestionId { get; set; }

        public string Answer1 { get; set; }

        public string Answer2 { get; set; }

        public string Answer3 { get; set; }

        public string Answer4 { get; set; }

        public string Answer5 { get; set; }

        public string Answer6 { get; set; }

        public string Answer7 { get; set; }

        public string Answer8 { get; set; }

        public string Answer9 { get; set; }

        public string Answer10 { get; set; }

        public virtual Question AnswerQuestion { get; set; }

        public virtual ICollection<AnswerResult> AnswerResults { get; set; } = new List<AnswerResult>();
    }
}