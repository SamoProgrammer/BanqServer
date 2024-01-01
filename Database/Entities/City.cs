using System.ComponentModel.DataAnnotations;
using Banq.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Banq.Database.Entities;

[PrimaryKey(nameof(Code))]
public class City {
	[Required]
	[MinLength(Validation.City.CodeLength)]
	[MaxLength(Validation.City.CodeLength)]
	[RegularExpression(Validation.Text.NumberRegex)]
	public string Code { get; set; } = default!;

	[Required]
	[MaxLength(Validation.City.NameMaxLength)]
	[RegularExpression(Validation.Text.PersianRegex)]
	public string Name { get; set; } = default!;

	[Required]
	public Guid ConcurrencyStamp { get; set; } = default!;
}
