using System.ComponentModel.DataAnnotations;
using Banq.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Banq.Database.Entities.Relations;

[PrimaryKey(nameof(SchoolCode), nameof(TeacherPersonnelCode))]
public class TeacherAndSchool {
	[Required]
	[MinLength(Validation.School.CodeLength)]
	[MaxLength(Validation.School.CodeLength)]
	[RegularExpression(Validation.Text.NumberRegex)]
	public string SchoolCode { get; set; } = default!;

	[Required]
	[MinLength(Validation.User.PersonnelCodeLength)]
	[MaxLength(Validation.User.PersonnelCodeLength)]
	[RegularExpression(Validation.Text.NumberRegex)]
	public string TeacherPersonnelCode { get; set; } = default!;

	[Required]
	public Guid ConcurrencyStamp { get; set; } = default!;
}
