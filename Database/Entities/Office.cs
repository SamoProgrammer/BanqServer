using System.ComponentModel.DataAnnotations;
using Banq.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Banq.Database.Entities;

[PrimaryKey(nameof(Code))]
public class Office {
	[Required]
	[MinLength(Validation.Office.CodeLength)]
	[MaxLength(Validation.Office.CodeLength)]
	[RegularExpression(Validation.Text.NumberRegex)]
	public string Code { get; set; } = default!;

	[Required]
	[MaxLength(Validation.Office.NameMaxLength)]
	[RegularExpression(Validation.Text.PersianRegex)]
	public string Name { get; set; } = default!;

	[Required]
	public Guid ConcurrencyStamp { get; set; } = default!;
}
