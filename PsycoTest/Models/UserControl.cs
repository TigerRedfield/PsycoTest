using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PsycoTest.Models
{
    public partial class UserControl
    {
        public int UserId { get; set; }

        public int UserGroupId { get; set; }

        public string UserLogin { get; set; }

        public string UserPassword { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public string UserPatronymic { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy'/'MM'/'dd}", ApplyFormatInEditMode = true)]
        public DateTime UserDateBirth { get; set; }

        
        public virtual ICollection<Result> Results { get; set; } = new List<Result>();

        public virtual Group UserGroup { get; set; }
    }
}