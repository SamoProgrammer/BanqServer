using System.ComponentModel.DataAnnotations;
using Banq.Authentication;
using Banq.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Banq.Database.Entities;

public class QuestionViewModel
{
	public ulong Id { get; set; }
	public DateTime Time { get; set; } = default!;
	public Type Type { get; set; } = default!;
	public Level Level { get; set; } = default!;
	public Lesson Lesson { get; set; }
	public Field Field { get; set; }
	public Grade Grade { get; set; }
	public ApplicationUser Author { get; set; }

}
