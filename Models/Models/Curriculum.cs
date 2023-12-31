﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Curriculum
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CurriculumId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string CurriculumName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string CurriculumCode { get; set; }
        [JsonIgnore]
        public virtual ICollection<Subject> CurriculumSubjects { get; set; } = new HashSet<Subject>();
        [JsonIgnore]
        public virtual ICollection<Student> CurriculumStudents { get; set; } = new HashSet<Student>();

        public override string ToString()
        {
            return $"{CurriculumCode} - {CurriculumName}";
        }
    }
}
