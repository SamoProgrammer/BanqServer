namespace Banq.Utilities;

public static class Validation {
	public static class Admin { }

	public static class Size {
		public const int PictureSizeLimit = 512 * 1024; // 512KiB
	}

	public static class Text {
		/// <summary>
		///     Regex for Persian characters excluding numbers [۰−۹]<br />
		///     This regex contains the following:
		///     <ul>
		///         <li>Persian characters</li>
		///         <li>Vowels</li>
		///         <li>Spaces (including half-space)</li>
		///     </ul>
		/// </summary>
		public const string PersianRegex =
			@"[\u0622\u0627\u0628\u067E\u062A-\u062C\u0686\u062D-\u0632\u0698\u0633-\u063A\u0641\u0642\u06A9\u06AF\u0644-\u0648\u06CC\u202C\u064B\u064C\u064E-\u0652\u200C\u200F ]*";

		/// <summary>
		///     Only numbers
		/// </summary>
		public const string NumberRegex = @"[0-9]*";

		/// <summary>
		///     Validates a username<br />
		///     Username must be all lowercase and at least 5 characters; it must start with a letter
		/// </summary>
		public const string UsernameRegex = @"[a-z][a-z0-9_\-.]{4,}";

		/// <summary>
		///     Regex for strong password<br />
		///     The password should contain at least:
		///     <ul>
		///         <li>one lower-case letter</li>
		///         <li>one upper-case letter</li>
		///         <li>one digit</li>
		///         <li>one special character of !@#$%^&*()+-~</li>
		///     </ul>
		///     and be at least 8 characters long.
		/// </summary>
		public const string PasswordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[\!@#\$%\^&\*\(\)\+\-~]).{8,}$";
	}

	public static class User {
		public const int NameMinLength       = 2;
		public const int NameMaxLength       = 50;
		public const int FamilyMinLength     = 2;
		public const int FamilyMaxLength     = 50;
		public const int BioMaxLength        = 255;
		public const int UsernameMinLength   = 5;
		public const int UsernameMaxLength   = 30;
		public const int PasswordMinLength   = 8;
		public const int PasswordMaxLength   = 50;
		public const int PersonnelCodeLength = 8;
	}

	public static class Tag {
		public const int NameMaxLength        = 50;
		public const int DescriptionMaxLength = 500;
	}

	public static class Question {
		public const int TitleMaxLength = 50;
	}

	public static class Request {
		public const long PdfUploadSizeLimit   = 10 * 1024 * 1024; // 10MiB
		public const long ImageUploadSizeLimit = 1 * 1024 * 1024; // 1 MiB
	}

	public static class School {
		public const int NameMaxLength = 100;
		public const int CodeLength    = 8;
	}

	public static class Province {
		public const int CodeLength    = 4;
		public const int NameMaxLength = 30;
	}

	public static class City {
		public const int CodeLength    = 4;
		public const int NameMaxLength = 30;
	}

	public static class Office {
		public const int CodeLength    = 4;
		public const int NameMaxLength = 30;
	}

	public static class Field {
		public const int CodeLength    = 2;
		public const int NameMaxLength = 30;
	}

	public static class Lesson {
		public const int CodeLength     = 5;
		public const int BookCodeLength = 6;
		public const int NameMaxLength  = 50;
	}

	public static class FieldOfTeach {
		public const int CodeLength    = 3;
		public const int NameMaxLength = 50;
	}

	public static class Class {
		public const int CodeMinLength = 3;
		public const int CodeMaxLength = 10;
		public const int NameMaxLength = 30;
	}
}
