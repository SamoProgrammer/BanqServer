using System.ComponentModel.DataAnnotations;
using Banq.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Banq.Database.Entities.Relations;

[PrimaryKey(nameof(TeacherPersonnelCode), nameof(FieldOfTeachCode))]
public class TeacherAndFieldOfTeach {
	[Required]
	[MinLength(Validation.User.PersonnelCodeLength)]
	[MaxLength(Validation.User.PersonnelCodeLength)]
	[RegularExpression(Validation.Text.NumberRegex)]
	public string TeacherPersonnelCode { get; set; } = default!;

	[Required]
	[MinLength(Validation.FieldOfTeach.CodeLength)]
	[MaxLength(Validation.FieldOfTeach.CodeLength)]
	[RegularExpression(Validation.Text.NumberRegex)]
	public string FieldOfTeachCode { get; set; } = default!;

	[Required]
	public Guid ConcurrencyStamp { get; set; } = default!;
}
