using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentComment.Models
{
    public class TeacherViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name  { get; set; }
        public string SurName { get; set; }
        public string Picture { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

    }
}