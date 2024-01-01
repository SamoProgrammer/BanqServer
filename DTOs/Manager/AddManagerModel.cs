using System.ComponentModel.DataAnnotations;
using Banq.Utilities;

namespace Banq.DTOs.Manager;

/// <summary>
///     model to create a new manager in the database
/// </summary>
public class AddManagerModel {
	/// <summary>
	///     the personnel code of the manager
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
	///     confirmation of <see cref="Password" />
	/// </summary>
	[Required]
	[MinLength(Validation.User.PasswordMinLength)]
	[MaxLength(Validation.User.PasswordMaxLength)]
	[RegularExpression(Validation.Text.PasswordRegex)]
	public required string ConfirmPassword { get; set; }

	/// <summary>
	///     the name of the manager
	/// </summary>
	[Required]
	[MinLength(Validation.User.NameMinLength)]
	[MaxLength(Validation.User.NameMaxLength)]
	[RegularExpression(Validation.Text.PersianRegex)]
	public required string Name { get; set; }

	/// <summary>
	///     the family of the manager
	/// </summary>
	[Required]
	[MinLength(Validation.User.FamilyMinLength)]
	[MaxLength(Validation.User.FamilyMaxLength)]
	[RegularExpression(Validation.Text.PersianRegex)]
	public required string Family { get; set; }
}
