using System.ComponentModel.DataAnnotations;
using Banq.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Banq.Database.Entities;

[PrimaryKey(nameof(Code))]
public class Field {
	[Required]
	[MinLength(Validation.Field.CodeLength)]
	[MaxLength(Validation.Field.CodeLength)]
	[RegularExpression(Validation.Text.NumberRegex)]
	public string Code { get; set; } = default!;

	[Required]
	[MaxLength(Validation.Field.NameMaxLength)]
	[RegularExpression(Validation.Text.PersianRegex)]
	public string Name { get; set; } = default!;

	[Required]
	public Guid ConcurrencyStamp { get; set; } = default!;
}
