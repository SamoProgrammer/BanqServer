using System.ComponentModel.DataAnnotations;
using Banq.Authentication;
using Banq.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Banq.Database.Entities;

public class CommentViewModel
{
	public int id { get; set; }
	public string Username { get; set; }
	public string Content { get; set; }
	public int Likes { get; set; } = 0;
}
