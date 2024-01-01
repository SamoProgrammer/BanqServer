using System.ComponentModel.DataAnnotations;
using Banq.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Banq.Database.Entities.Relations;

[PrimaryKey(nameof(CityCode), nameof(OfficeCode))]
public class CityAndOffice {
	[Required]
	[MinLength(Validation.City.CodeLength)]
	[MaxLength(Validation.City.CodeLength)]
	[RegularExpression(Validation.Text.NumberRegex)]
	public string CityCode { get; set; } = default!;

	[Required]
	[MinLength(Validation.Office.CodeLength)]
	[MaxLength(Validation.Office.CodeLength)]
	[RegularExpression(Validation.Text.NumberRegex)]
	public string OfficeCode { get; set; } = default!;

	[Required]
	public Guid ConcurrencyStamp { get; set; } = default!;
}
