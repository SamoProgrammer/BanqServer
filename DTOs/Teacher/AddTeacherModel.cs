using System.ComponentModel.DataAnnotations;
using Banq.Utilities;

namespace Banq.DTOs.Teacher;

/// <summary>
///     This model is used to create a new <see cref="Banq.Database.Entities.Teacher" />
/// </summary>
public class AddTeacherModel {
	/// <summary>
	///     The unique teacher personnel code
	/// </summary>
	[Required]
	[MinLength(Validation.User.PersonnelCodeLength)]
	[MaxLength(Validation.User.PersonnelCodeLength)]
	[RegularExpression(Validation.Text.NumberRegex)]
	public required string PersonnelCode { get; set; }

	/// <summary>
	///     the password
	/// </summary>
	[Required]
	[MinLength(Validation.User.PasswordMinLength)]
	[MaxLength(Validation.User.PasswordMaxLength)]
	[RegularExpression(Validation.Text.PasswordRegex)]
	public required string Password { get; set; }

	/// <summary>
	///     confirmation of the <see cref="Password" />
	/// </summary>
	[Required]
	[MinLength(Validation.User.PasswordMinLength)]
	[MaxLength(Validation.User.PasswordMaxLength)]
	[RegularExpression(Validation.Text.PasswordRegex)]
	public required string ConfirmPassword { get; set; }

	/// <summary>
	///     the name of the teacher
	/// </summary>
	[Required]
	[MinLength(Validation.User.NameMinLength)]
	[MaxLength(Validation.User.NameMaxLength)]
	[RegularExpression(Validation.Text.PersianRegex)]
	public required string Name { get; set; }

	/// <summary>
	///     the family of the teacher
	/// </summary>
	[Required]
	[MinLength(Validation.User.FamilyMinLength)]
	[MaxLength(Validation.User.FamilyMaxLength)]
	[RegularExpression(Validation.Text.PersianRegex)]
	public required string Family { get; set; }
}
