using System.ComponentModel.DataAnnotations;
using Banq.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Banq.Database.Entities.Relations;

[PrimaryKey(nameof(ProvinceCode), nameof(CityCode))]
public class ProvinceAndCity {
	[Required]
	[MinLength(Validation.Province.CodeLength)]
	[MaxLength(Validation.Province.CodeLength)]
	[RegularExpression(Validation.Text.NumberRegex)]
	public string ProvinceCode { get; set; } = default!;

	[Required]
	[MinLength(Validation.City.CodeLength)]
	[MaxLength(Validation.City.CodeLength)]
	[RegularExpression(Validation.Text.NumberRegex)]
	public string CityCode { get; set; } = default!;

	[Required]
	public Guid ConcurrencyStamp { get; set; } = default!;
}
