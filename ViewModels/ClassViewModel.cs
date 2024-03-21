using System.ComponentModel.DataAnnotations;
using Banq.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Banq.ViewModels;

public class ClassViewModel {

	public string Code { get; set; } = default!;

	public string LessonName { get; set; } = default!;

	public string? Name { get; set; } = default!;
}
