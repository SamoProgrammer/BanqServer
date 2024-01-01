using Banq.Database;
using Banq.Database.Entities;

namespace Banq.Utilities;

/// <summary>
///     Provides some utility extension methods
/// </summary>
public static class Extensions {
	/// <summary>
	///     Checks that whether the current running environment is <c>Test</c>
	/// </summary>
	/// <param name="environment">receiver</param>
	public static bool IsTest(this IWebHostEnvironment environment) {
		return environment.IsEnvironment("Test");
	}

	public static void SeedTestData(this DatabaseContext context) {
		context.Database.EnsureCreated();

		context.SeedTeachers();
		context.SeedManagers();
	}

	private static void SeedTeachers(this DatabaseContext context) {
		context.Teachers.Add(new Teacher {
			PersonnelCode = "98864523",
			PasswordHash = Util.PasswordHashOf("Teacher1@pass"),
			Name = "Teacher",
			Family = "Teacher Zadeh",
			ConcurrencyStamp = Guid.NewGuid(),
			WantsToCheckOtherQuestions = false,
			Biography = null,
			PictureGuid = null
		});

		context.SaveChanges();
	}

	private static void SeedManagers(this DatabaseContext context) {
		context.Managers.Add(new Manager {
			PersonnelCode = "52344520",
			PasswordHash = Util.PasswordHashOf("M@n@g3r"),
			Name = "علیرضا",
			Family = "یوسفی",
			ConcurrencyStamp = Guid.NewGuid(),
			Biography = null,
			PictureGuid = null
		});

		context.SaveChanges();
	}
}
