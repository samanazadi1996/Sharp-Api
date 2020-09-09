using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Dto
{
    public class RegisterUserDto /*: IValidatableObject*/
    {
        [Required]
        [StringLength(500)]
        public string Password { get; set; }

        [Required]
        [StringLength(500)]
        [Compare("Password", ErrorMessage = "تکرار گذرواژه نامعتبر است")]
        public string ConfirmPassword { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(11)]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public GenderType Gender { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    var db = new Data.ApplicationDbContext();
        //    if (db.Users.Any(p => p.UserName.ToLower()==UserName.ToLower()))
        //        yield return new ValidationResult("نام کاربری تکراری است");
        //}
    }
}
