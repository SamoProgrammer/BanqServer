using Banq.Authentication;
using Banq.Database.Entities;
using Banq.Database.Entities.Relations;
using Banq.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Banq.Database;

/// <summary>
///     Application's Database Context
/// </summary>
public class DatabaseContext : IdentityDbContext<ApplicationUser> {
	/// <summary>
	///     Constructor
	/// </summary>
	public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

	#region Entities
	public DbSet<Admin>        Admins        { get; set; } = default!;
	public DbSet<City>         Cities        { get; set; } = default!;
	public DbSet<Class>        Classes       { get; set; } = default!;
	public DbSet<Field>        Fields        { get; set; } = default!;
	public DbSet<FieldOfTeach> FieldsOfTeach { get; set; } = default!;
	public DbSet<Lesson>       Lessons       { get; set; } = default!;
	public DbSet<Manager>      Managers      { get; set; } = default!;
	public DbSet<Office>       Offices       { get; set; } = default!;
	public DbSet<Province>     Provinces     { get; set; } = default!;
	public DbSet<Question>     Questions     { get; set; } = default!;
	public DbSet<School>       Schools       { get; set; } = default!;
	public DbSet<Teacher>      Teachers      { get; set; } = default!;
	#endregion

	#region Relations
	public DbSet<CityAndOffice>              CityAndOffices                { get; set; } = default!;
	public DbSet<FieldAndLessonAndGrade>     FieldAndLessonsAndGrades      { get; set; } = default!;
	public DbSet<ManagerAndSchool>           ManagerAndSchools             { get; set; } = default!;
	public DbSet<ProvinceAndCity>            ProvinceAndCities             { get; set; } = default!;
	public DbSet<SchoolAndClass>             SchoolAndClasses              { get; set; } = default!;
	public DbSet<SchoolAndField>             SchoolsAndFields              { get; set; } = default!;
	public DbSet<OfficeAndSchool>            OfficeAndSchools              { get; set; } = default!;
	public DbSet<TeacherAndClass>            TeachersAndClasses            { get; set; } = default!;
	public DbSet<TeacherAndClassAndQuestion> TeacherAndClassesAndQuestions { get; set; } = default!;
	public DbSet<TeacherAndFieldOfTeach>     TeachersAndFieldOfTeaches     { get; set; } = default!;
	public DbSet<TeacherAndSchool>           TeachersAndSchools            { get; set; } = default!;
	#endregion

	/// <inheritdoc />
	protected override void OnModelCreating(ModelBuilder builder) {
		base.OnModelCreating(builder);

		SeedAdmin(builder);
	}

	private static void SeedAdmin(ModelBuilder builder) {
		builder.Entity<Admin>().HasData(new Admin {
			Id = 1,
			Username = "site-admin",
			PasswordHash = Util.PasswordHashOf("Admin#123"),
			Name = "Admin",
			Family = "Admin",
			PriviledgeLevel = -1,
			PromotedBy = null,
			PictureGuid = null,
			ConcurrencyStamp = Guid.NewGuid()
		});
	}
}
