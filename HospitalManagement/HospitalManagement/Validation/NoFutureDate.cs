using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Validation
{
    public class NoFutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue)
            {
                
                if (dateValue > DateTime.Today)
                {
                    return new ValidationResult("The admission date cannot be in the future.");
                }
            }
            return ValidationResult.Success;
        }
    }
}