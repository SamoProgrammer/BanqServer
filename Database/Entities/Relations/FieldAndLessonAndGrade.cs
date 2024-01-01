using System.ComponentModel.DataAnnotations;
using Banq.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Banq.Database.Entities.Relations;

[PrimaryKey(nameof(FieldCode), nameof(LessonCode), nameof(Grade))]
public class FieldAndLessonAndGrade {
	[Required]
	[MinLength(Validation.Field.CodeLength)]
	[MaxLength(Validation.Field.CodeLength)]
	[RegularExpression(Validation.Text.NumberRegex)]
	public string FieldCode { get; set; } = default!;

	[Required]
	[MinLength(Validation.Lesson.CodeLength)]
	[MaxLength(Validation.Lesson.CodeLength)]
	[RegularExpression(Validation.Text.NumberRegex)]
	public string LessonCode { get; set; } = default!;

	[Required]
	[EnumDataType(typeof(Grade))]
	public Grade Grade { get; set; } = default!;

	[Required]
	public Guid ConcurrencyStamp { get; set; } = default!;
}
