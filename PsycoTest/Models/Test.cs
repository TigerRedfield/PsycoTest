using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PsycoTest.Models
{
    public partial class Test
    {
        public int TestId { get; set; }

        public string TestName { get; set; }

        public string TestDescription { get; set; }

        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

        public virtual ICollection<Result> Results { get; set; } = new List<Result>();
    }
}