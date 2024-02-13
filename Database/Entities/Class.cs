using System.ComponentModel.DataAnnotations;
using Banq.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Banq.Database.Entities;

[PrimaryKey(nameof(Id))]
public class Class {
	[Required]
	public ulong Id { get; set; } = default!;

	[Required]
	[MinLength(Validation.Class.CodeMinLength)]
	[MaxLength(Validation.Class.CodeMaxLength)]
	[RegularExpression(Validation.Text.NumberRegex)]
	public string Code { get; set; } = default!;

	[Required]
	[MinLength(Validation.Lesson.CodeLength)]
	[MaxLength(Validation.Lesson.CodeLength)]
	[RegularExpression(Validation.Text.NumberRegex)]
	public Lesson Lesson { get; set; } = default!;

	[Required]
	public Guid ConcurrencyStamp { get; set; } = default!;

	[MaxLength(Validation.Class.NameMaxLength)]
	[RegularExpression(Validation.Text.PersianRegex)]
	public string? Name { get; set; } = default!;
}
