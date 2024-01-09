using System.ComponentModel.DataAnnotations;
using Banq.Utilities;

namespace Banq.Authentication.Models
{
    public class TeacherRegisterModel : RegisterModel
    {
        [Required]
        [MinLength(Validation.User.PersonnelCodeLength)]
        [MaxLength(Validation.User.PersonnelCodeLength)]
        [RegularExpression(Validation.Text.NumberRegex)]
        public string PersonnelCode { get; set; } = default!;

        // [Required]
        // public string PasswordHash { get; set; } = default!;

        [Required]
        [MinLength(Validation.User.NameMinLength)]
        [MaxLength(Validation.User.NameMaxLength)]
        [RegularExpression(Validation.Text.PersianRegex)]
        public string Name { get; set; } = default!;

        [Required]
        [MinLength(Validation.User.FamilyMinLength)]
        [MaxLength(Validation.User.FamilyMaxLength)]
        [RegularExpression(Validation.Text.PersianRegex)]
        public string Family { get; set; } = default!;


        [MaxLength(Validation.User.BioMaxLength)]
        public string? Biography { get; set; }

        [Required]
        public bool WantsToCheckOtherQuestions { get; set; }
    }
}
