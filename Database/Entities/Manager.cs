using System.ComponentModel.DataAnnotations;
using Banq.Authentication;
using Banq.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Banq.Database.Entities;

// [PrimaryKey(nameof(PersonnelCode))]
public class Manager
{
	public ulong Id { get; set; }

	[Required]
	[MinLength(Validation.User.PersonnelCodeLength)]
	[MaxLength(Validation.User.PersonnelCodeLength)]
	[RegularExpression(Validation.Text.NumberRegex)]
	public string PersonnelCode { get; set; } = default!;

	[Required]
	public virtual ApplicationUser User { get; set; }

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
}
