using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentComment.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Explanation { get; set; }
        public string Status { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }
        
        [ForeignKey("Students")]
        public string StudentId { get; set; }
        public virtual Student Students { get; set; }
        public string TeacherName { get; set; }
        public string TeacherPicture { get; set; }
    }
}