using System.ComponentModel.DataAnnotations;
using Banq.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Banq.Database.Entities;

[PrimaryKey(nameof(Id))]
public class QuestionSet
{
	[Required]
	public ulong Id { get; set; }
	[Required]
	public string Name { get; set; }

	[Required]
	public DateTime ServerTime { get; set; } = default!;

	[Required]
	public DateTime Time { get; set; } = default!;

	[Required]
	public virtual ApplicationUser Author { get; set; }

	[Required]
	[EnumDataType(typeof(Type))]
	public Type Type { get; set; } = default!;

	[Required]
	[EnumDataType(typeof(Level))]
	public Level Level { get; set; } = default!;

	[Required]
	[EnumDataType(typeof(Status))]
	public Status Status { get; set; } = default!;

	[Required]
	public virtual Lesson Lesson { get; set; }

	[Required]
	public virtual Field Field { get; set; }

	[Required]
	[EnumDataType(typeof(Grade))]
	public Grade Grade { get; set; }

	[Required]
	public Guid ConcurrencyStamp { get; set; } = default!;

	public string FileLink { get; set; } = "";
}

public enum Type
{
	Pdf = 1,
	Docx,
	Text
}

public enum Level
{
	Easy = 1,
	Medium,
	Hard
}

public enum Status
{
	Pending = 1,
	Edited
}
