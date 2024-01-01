using System.ComponentModel.DataAnnotations;
using Banq.Database.Entities;
using Banq.Utilities;

namespace Banq.DTOs;

/// <summary>
///     Model used to update <see cref="Teacher.Name" />, <see cref="Teacher.Family" /> and <see cref="Teacher.Biography" />
/// </summary>
public class UpdateNameFamilyBioModel {
	/// <summary>
	///     The new name<br />
	///     Must be in persian
	/// </summary>
	[MinLength(Validation.User.NameMinLength)]
	[MaxLength(Validation.User.NameMaxLength)]
	[RegularExpression(Validation.Text.PersianRegex)]
	public string? Name { get; set; }

	/// <summary>
	///     The new family<br />
	///     Must be in persian
	/// </summary>
	[MinLength(Validation.User.FamilyMinLength)]
	[MaxLength(Validation.User.FamilyMaxLength)]
	[RegularExpression(Validation.Text.PersianRegex)]
	public string? Family { get; set; }

	/// <summary>
	///     The new bio
	/// </summary>
	[MaxLength(Validation.User.BioMaxLength)]
	public string? Biography { get; set; }
}
