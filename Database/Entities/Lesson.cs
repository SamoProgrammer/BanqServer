using System.ComponentModel.DataAnnotations;
using Banq.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Banq.Database.Entities;

[PrimaryKey(nameof(Code))]
public class Lesson {
	[Required]
	[MinLength(Validation.Lesson.CodeLength)]
	[MaxLength(Validation.Lesson.CodeLength)]
	[RegularExpression(Validation.Text.NumberRegex)]
	public string Code { get; set; } = default!;

	[Required]
	[MaxLength(Validation.Lesson.NameMaxLength)]
	[RegularExpression(Validation.Text.PersianRegex)]
	public string Name { get; set; } = default!;

	[Required]
	[MinLength(Validation.Lesson.BookCodeLength)]
	[MaxLength(Validation.Lesson.BookCodeLength)]
	[RegularExpression(Validation.Text.NumberRegex)]
	public string BookCode { get; set; } = default!;

	[Required]
	public Guid ConcurrencyStamp { get; set; } = default!;
}
