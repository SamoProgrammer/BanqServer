using System.ComponentModel.DataAnnotations;
using Banq.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Banq.Database.Entities.Relations;

[PrimaryKey(nameof(OfficeCode), nameof(SchoolCode))]
public class OfficeAndSchool {
	[Required]
	[MinLength(Validation.Office.CodeLength)]
	[MaxLength(Validation.Office.CodeLength)]
	[RegularExpression(Validation.Text.NumberRegex)]
	public string OfficeCode { get; set; } = default!;

	[Required]
	[MinLength(Validation.School.CodeLength)]
	[MaxLength(Validation.School.CodeLength)]
	[RegularExpression(Validation.Text.NumberRegex)]
	public string SchoolCode { get; set; } = default!;

	[Required]
	public Guid ConcurrencyStamp { get; set; } = default!;
}
