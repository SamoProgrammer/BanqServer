using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Banq.Database.Entities;

[PrimaryKey(nameof(Id))]
public class Question {
	[Required]
	public ulong Id { get; set; }

	[Required]
	public DateTime ServerTime { get; set; } = default!;

	[Required]
	public DateTime Time { get; set; } = default!;

	[Required]
	[EnumDataType(typeof(Type))]
	public Type Type { get; set; } = default!;

	[Required]
	[EnumDataType(typeof(Level))]
	public Level Level { get; set; } = default!;

	[Required]
	public Guid ConcurrencyStamp { get; set; } = default!;
}

public enum Type {
	Pdf = 1,
	Docx,
	Text
}

public enum Level {
	Easy = 1,
	Medium,
	Hard
}
