using System.ComponentModel.DataAnnotations;
using Banq.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Banq.Database.Entities;

public class SchoolViewModel {
	public string Code { get; set; } = default!;

	public string Name { get; set; } = default!;

	public Gender Gender { get; set; }

	public CourseLevel CourseLevel { get; set; }

	public Guid? PictureGuid { get; set; }
}
