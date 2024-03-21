using System.ComponentModel.DataAnnotations;
using Banq.Database.Entities;
using Banq.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Banq.DTOs;

[PrimaryKey(nameof(Code))]
public class SchoolDTO {
	public string Code { get; set; } = default!;

	public string Name { get; set; } = default!;

	public Gender Gender { get; set; }

	public CourseLevel CourseLevel { get; set; }

	public Guid? PictureGuid { get; set; }
}
