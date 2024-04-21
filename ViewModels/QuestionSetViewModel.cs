using System.ComponentModel.DataAnnotations;
using Banq.Authentication;
using Banq.Database.Entities;
using Banq.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Banq.ViewModels;

public class QuestionSetViewModel
{
	public ulong Id { get; set; }
	public DateTime Time { get; set; } = default!;
	public Database.Entities.Type Type { get; set; } = default!;
	public Level Level { get; set; } = default!;
	public Lesson Lesson { get; set; }
	public Field Field { get; set; }
	public Grade Grade { get; set; }
	public ApplicationUser Author { get; set; }
	public Status Status { get; set; }
	public string Name { get; set; }


}
