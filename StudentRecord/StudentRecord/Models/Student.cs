using System.ComponentModel.DataAnnotations;

namespace StudentRecord.Models
{
    public class Student
    {
        [Required]
        public string RollNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0, 100)]
        public int Marks { get; set; }
    }
}