using Microsoft.AspNetCore.Identity;

namespace Banq.Utilities;

public static class Util {
	public static void EnsureDirectoryExists(params string[] paths) {
		foreach (var path in paths) {
			Directory.CreateDirectory(path);
		}
	}

	public static string PasswordHashOf(string password) {
		var hash = new PasswordHasher<object>().HashPassword(null!, password);

		#if TEST_ENV
		File.AppendAllText(".dump/passwords", $"{hash}\t{password}\n");
		#endif

		return hash;
	}

	public static bool VerifyHashedPassword(string hashedPassword, string password) {
		return new PasswordHasher<object>().VerifyHashedPassword(null!, hashedPassword, password) == PasswordVerificationResult.Success;
	}
}
