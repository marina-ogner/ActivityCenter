using System;
using System.ComponentModel.DataAnnotations;
namespace EntityProject.Models
{

    public class RegistrationViewModel : BaseEntity
    {
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^\D+$")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [RegularExpression(@"^\D+$")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password min length is 8 characters")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*.#?&])[A-Za-z\d$@$!%*.#?&]{8,}$", ErrorMessage = "Password must contain at least 1 number, 1 letter, and a special character")]

        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password Confirmation")]
        [Compare("Password", ErrorMessage = "Password and confirmation must match.")]
        public string PasswordConfirmation { get; set; }
    }

    public class LoginViewModel : BaseEntity
    {

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        // [Compare("_context.users.Where( u => u.Email == model.Email).ToList()[0].Email", ErrorMessage = "Email doesn't exist")]
        public string EmailLog { get; set; }

        [Required]
        [MinLength(3)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string PasswordLog { get; set; }
    }

    public class ActivityViewModel : BaseEntity
    {
        [Required]
        public int ActivityId { get; set; }

        [Required]
        [MinLength(2)]
        public string Title { get; set; }
        
        [Required]
        [DataType(DataType.Time)]
        public TimeSpan? Time { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [CheckFuture]
        public DateTime? Date { get; set; }

        [Required]
        public int? Duration { get; set; }

        [Required]
        public int Units { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        [Required]
        public string Address { get; set; }
    }

    public class CheckFuture : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime? Today = DateTime.Now;

            if ((DateTime?)value < Today)
            {
                return new ValidationResult("Date must be in the Future");
            }
            return ValidationResult.Success;
        }
    }
}