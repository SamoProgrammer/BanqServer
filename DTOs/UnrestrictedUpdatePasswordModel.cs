using System.ComponentModel.DataAnnotations;
using Banq.Utilities;

namespace Banq.DTOs;

/// <summary>
///     This model doesn't require the old password
/// </summary>
/// <seealso cref="UpdatePasswordModel" />
public class UnrestrictedUpdatePasswordModel {
	/// <summary>
	///     The new password<br />
	///     Must be validated against <see cref="Validation.Text.PasswordRegex" />
	/// </summary>
	[Required]
	[MinLength(Validation.User.PasswordMinLength)]
	[MaxLength(Validation.User.PasswordMaxLength)]
	[RegularExpression(Validation.Text.PasswordRegex)]
	public required string NewPassword { get; set; }

	/// <summary>
	///     Confirmation of the <see cref="NewPassword" /><br />
	///     Must be validated against <see cref="Validation.Text.PasswordRegex" />
	/// </summary>
	[Required]
	[MinLength(Validation.User.PasswordMinLength)]
	[MaxLength(Validation.User.PasswordMaxLength)]
	[RegularExpression(Validation.Text.PasswordRegex)]
	public required string ConfirmPassword { get; set; }
}
