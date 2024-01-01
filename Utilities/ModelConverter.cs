using Banq.DTOs;

namespace Banq.Utilities;

/// <summary>
///     Utility class that provides extension methods to convert models to each other
/// </summary>
public static class ModelConverter {
	/// <summary>
	///     Converts an <see cref="UnrestrictedUpdatePasswordModel" /> to an <see cref="UpdatePasswordModel" />
	/// </summary>
	/// <param name="model">(receiver)</param>
	public static UpdatePasswordModel ToRestrictedModel(this UnrestrictedUpdatePasswordModel model) {
		return new UpdatePasswordModel {
			OldPassword = "",
			NewPassword = model.NewPassword,
			ConfirmPassword = model.ConfirmPassword
		};
	}
}
