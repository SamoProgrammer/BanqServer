using System.ComponentModel.DataAnnotations;
using Banq.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Banq.DTOs;

public class QuestionSetDTO
{
	public DateTime Time { get; set; } = default!;

	[EnumDataType(typeof(Database.Entities.Type))]
	public Database.Entities.Type Type { get; set; } = default!;

	[EnumDataType(typeof(Level))]
	public Level Level { get; set; } = default!;

	[Required]
	public string LessonName { get; set; }

	[Required]
	public string FieldName { get; set; }

	[Required]
	public Grade Grade { get; set; }

}