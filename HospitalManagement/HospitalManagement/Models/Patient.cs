using System.ComponentModel.DataAnnotations;
using HospitalManagement.Validation; 

namespace HospitalManagement.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } 

        [Required]
        [EmailAddress]
        public string Email { get; set; } 

        [Required]
        public string Disease { get; set; } 

        [Required]
        [Display(Name = "Admission Date")]
        [DataType(DataType.Date)]
        [NoFutureDate] 
        public DateTime AdmissionDate { get; set; }

        [Required]
        [Range(1, 90, ErrorMessage = "Age must be between 1 and 90.")]
        public int Age { get; set; }
    }
}