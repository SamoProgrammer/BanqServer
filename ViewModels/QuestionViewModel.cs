using System.ComponentModel.DataAnnotations;
using Banq.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Banq.Database.Entities;

public class QuestionViewModel
{
	public ulong Id { get; set; }
	public DateTime Time { get; set; } = default!;

	[EnumDataType(typeof(Type))]
	public Type Type { get; set; } = default!;

	[EnumDataType(typeof(Level))]
	public Level Level { get; set; } = default!;

}
