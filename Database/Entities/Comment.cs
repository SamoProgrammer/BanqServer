using System.ComponentModel.DataAnnotations;
using Banq.Authentication;
using Banq.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Banq.Database.Entities;

[PrimaryKey(nameof(Id))]
public class Comment
{
	[Required]
	public ulong Id { get; set; } = default!;
	public ApplicationUser User { get; set; }
	public string Content { get; set; }
	public int Likes { get; set; } = 0;
}
