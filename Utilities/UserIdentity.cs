namespace Banq.Utilities;

public static class UserIdentity {
	public static class Roles {
		public const string Admin   = "Roles.Admin";
		public const string Manager = "Roles.Manager";
		public const string Teacher = "Roles.Teacher";
		public const string Student = "Roles.Student";
	}

	public static class Claims {
		public const string AdminId        = "Claims.Admin.Id";
		public const string AdminUsername  = "Claims.Admin.Username";
		public const string AdminPrivilege = "Claims.Admin.PrivilegeLevel";
		public const string PersonnelCode  = "Claims.User.PersonnelCode";
	}
}
