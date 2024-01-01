using System.ComponentModel.DataAnnotations;
using Banq.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Banq.Database.Entities.Relations;

[PrimaryKey(nameof(ManagerPersonnelCode), nameof(SchoolCode))]
public class ManagerAndSchool {
	[Required]
	[MinLength(Validation.User.PersonnelCodeLength)]
	[MaxLength(Validation.User.PersonnelCodeLength)]
	[RegularExpression(Validation.Text.NumberRegex)]
	public string ManagerPersonnelCode { get; set; } = default!;

	[Required]
	[MinLength(Validation.School.CodeLength)]
	[MaxLength(Validation.School.CodeLength)]
	[RegularExpression(Validation.Text.NumberRegex)]
	public string SchoolCode { get; set; } = default!;

	[Required]
	public Guid ConcurrencyStamp { get; set; } = default!;
}
