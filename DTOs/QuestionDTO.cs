using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Banq.Database.Entities;

public class QuestionDTO
{
	public DateTime Time { get; set; } = default!;

	[EnumDataType(typeof(Type))]
	public Type Type { get; set; } = default!;

	[EnumDataType(typeof(Level))]
	public Level Level { get; set; } = default!;

	[Required]
	public string LessonName { get; set; }

	[Required]
	public string FieldName { get; set; }

	[Required]
	public Grade Grade { get; set; }

}