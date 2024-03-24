using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PsycoTest.Models
{
    public partial class Result
    {
        public int ResultId { get; set; }

        public int ResultTestId { get; set; }

        public int ResultUserId { get; set; }

        public DateTime ResultDateStart { get; set; }

        public DateTime ResultDateEnd { get; set; }

        public virtual AnswerResult AnswerResult { get; set; }

        public virtual Test ResultTest { get; set; } 

        public virtual User ResultUser { get; set; }
    }
}