using System.ComponentModel.DataAnnotations;
using Banq.Utilities;

namespace Banq.DTOs;

/// <summary>
///     This model is used to update name and family fields
/// </summary>
public class UpdateNameFamilyModel {
	/// <summary>
	///     New name (can be null in order not to update)<br />
	///     Must be in persian
	/// </summary>
	[MinLength(Validation.User.NameMinLength)]
	[MaxLength(Validation.User.NameMaxLength)]
	[RegularExpression(Validation.Text.PersianRegex)]
	public string? Name { get; set; }

	/// <summary>
	///     New family (can be null in order not to update)<br />
	///     Must be in persian
	/// </summary>
	[MinLength(Validation.User.FamilyMinLength)]
	[MaxLength(Validation.User.FamilyMaxLength)]
	[RegularExpression(Validation.Text.PersianRegex)]
	public string? Family { get; set; }
}
