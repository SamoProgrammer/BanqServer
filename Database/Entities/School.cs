using System.ComponentModel.DataAnnotations;
using Banq.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Banq.Database.Entities;

[PrimaryKey(nameof(Code))]
public class School {
	[Required]
	[MinLength(Validation.School.CodeLength)]
	[MaxLength(Validation.School.CodeLength)]
	[RegularExpression(Validation.Text.NumberRegex)]
	public string Code { get; set; } = default!;

	[Required]
	[MaxLength(Validation.School.NameMaxLength)]
	[RegularExpression(Validation.Text.PersianRegex)]
	public string Name { get; set; } = default!;

	[Required]
	[EnumDataType(typeof(Gender))]
	public Gender Gender { get; set; }

	[Required]
	[EnumDataType(typeof(CourseLevel))]
	public CourseLevel CourseLevel { get; set; }

	[Required]
	public Guid ConcurrencyStamp { get; set; }

	public string? PictureURL { get; set; }
}

public enum Gender {
	Male = 1,
	Female
}

public enum CourseLevel {
	MiddleSchool = 1,
	HighSchool
}

public enum Grade {
	Seven = 7,
	Eight,
	Nine,
	Ten,
	Eleven,
	Tweleve
}
