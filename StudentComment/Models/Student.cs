using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentComment.Models
{
    public class Student
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Picture { get; set; }
        public string Department { get; set; }
        public string Class { get; set; }
        public int Number { get; set; }
    }
}