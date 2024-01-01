using System.ComponentModel.DataAnnotations;
using Banq.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Banq.Database.Entities.Relations;

[PrimaryKey(nameof(TeacherPersonnelCode), nameof(ClassId), nameof(QuestionId))]
public class TeacherAndClassAndQuestion {
	[Required]
	[MinLength(Validation.User.PersonnelCodeLength)]
	[MaxLength(Validation.User.PersonnelCodeLength)]
	[RegularExpression(Validation.Text.NumberRegex)]
	public string TeacherPersonnelCode { get; set; } = default!;

	[Required]
	public ulong ClassId { get; set; } = default!;

	[Required]
	public ulong QuestionId { get; set; }

	[Required]
	public Guid ConcurrencyStamp { get; set; } = default!;
}
