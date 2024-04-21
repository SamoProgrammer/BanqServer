using System.ComponentModel.DataAnnotations;
using Banq.Database.Entities;
using Banq.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Banq.ViewModels;

public class SchoolViewModel {
	public string Code { get; set; } = default!;

	public string Name { get; set; } = default!;

	public Gender Gender { get; set; }

	public CourseLevel CourseLevel { get; set; }

	public string? PictureURL { get; set; }
}
