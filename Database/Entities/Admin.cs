using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Banq.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Banq.Database.Entities;

[PrimaryKey(nameof(Id))]
public class Admin {
	[Required]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public ulong Id { get; set; }

	[Required]
	[MinLength(Validation.User.UsernameMinLength)]
	[MaxLength(Validation.User.UsernameMaxLength)]
	[RegularExpression(Validation.Text.UsernameRegex)]
	public string Username { get; set; } = default!;

	[Required]
	public string PasswordHash { get; set; } = default!;

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

	public Guid? PictureGuid { get; set; }

	[MinLength(Validation.User.UsernameMinLength)]
	[MaxLength(Validation.User.UsernameMaxLength)]
	[RegularExpression(Validation.Text.UsernameRegex)]
	public string? PromotedBy { get; set; }

	[Required]
	public int PriviledgeLevel { get; set; }

	[Required]
	public Guid ConcurrencyStamp { get; set; }
}
