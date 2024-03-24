using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PsycoTest.Models
{
    public partial class AnswerResult
    {
        public int ResultId { get; set; }

        public int AnswerId { get; set; }

        public virtual Answer Answer { get; set; }

        public virtual Result Result { get; set; }
    }
}