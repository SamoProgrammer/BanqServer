using System.ComponentModel.DataAnnotations;
using Banq.Utilities;

namespace Banq.DTOs;

/// <summary>
///     This model is used to login with a personnel code
/// </summary>
public class PersonnelCodeLoginModel {
	/// <summary>
	///     Personnel code<br />
	///     Must be a number
	/// </summary>
	[Required]
	[MinLength(Validation.User.PersonnelCodeLength)]
	[MaxLength(Validation.User.PersonnelCodeLength)]
	[RegularExpression(Validation.Text.NumberRegex)]
	public required string PersonnelCode { get; set; }

	/// <summary>
	///     Password
	/// </summary>
	[Required]
	public required string Password { get; set; }
}
