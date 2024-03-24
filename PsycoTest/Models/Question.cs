using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PsycoTest.Models
{
    public partial class Question
    {
        public int QuestionId { get; set; }

        public int QuestionTestId { get; set; }

        public string QuestionName { get; set; }

        public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

        public virtual Test QuestionTest { get; set; }
    }
}