﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PsycoTest.Models
{
    public partial class Group
    {
        public int GroupId { get; set; }

        public string GroupNumber { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}