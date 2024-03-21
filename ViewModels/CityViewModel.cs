using System.ComponentModel.DataAnnotations;
using Banq.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Banq.ViewModels;

public class CityViewModel {
	public string Code { get; set; } = default!;
	public string Name { get; set; } = default!;

}
