using System.ComponentModel.DataAnnotations;
using Banq.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Banq.Database.Entities;

public class ClassDTO {

	public string Code { get; set; } = default!;

	public string LessonName { get; set; } = default!;

	public string? Name { get; set; } = default!;
}