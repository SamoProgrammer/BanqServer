using System.ComponentModel.DataAnnotations;
using Banq.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Banq.Database.Entities.Relations;

[PrimaryKey(nameof(SchoolCode), nameof(FieldCode))]
public class SchoolAndField {
	[Required]
	[MinLength(Validation.School.CodeLength)]
	[MaxLength(Validation.School.CodeLength)]
	[RegularExpression(Validation.Text.NumberRegex)]
	public string SchoolCode { get; set; } = default!;

	[Required]
	[MinLength(Validation.Field.CodeLength)]
	[MaxLength(Validation.Field.CodeLength)]
	[RegularExpression(Validation.Text.NumberRegex)]
	public string FieldCode { get; set; } = default!;

	[Required]
	public Guid ConcurrencyStamp { get; set; } = default!;
}
