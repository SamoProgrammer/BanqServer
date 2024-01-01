namespace Banq.Utilities;

public static class Constants {
	public const string UploadsDir = "uploads";
	public const string PdfsDir    = $"{UploadsDir}/pdf";
	public const string DocsDir    = $"{UploadsDir}/doc";
	public const string ImagesDir  = $"{UploadsDir}/img";

	public static class ContentType {
		public const string ApplicationJson = "application/json";
		public const string ApplicationPdf  = "application/pdf";
		public const string ApplicationXml  = "application/xml";
		public const string ApplicationDocx = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
		public const string ImageGif        = "image/gif";
		public const string ImageJpeg       = "image/jpeg";
		public const string ImagePng        = "image/png";
		public const string TextHtml        = "text/html";
		public const string TextPlain       = "text/plain";
		public const string TextXml         = "text/xml";
	}

	public static class RoleIds {
		public const string Admin   = "A71D7C2C-B34D-44CE-93FD-F8CEB16FAEB8";
		public const string Manager = "7C56DB22-346C-436A-8A44-8B7C68C316FC";
		public const string Teacher = "55081BB4-81C9-463C-9B60-A0A9D7F6A821";
		public const string Student = "10C85858-A0E9-433E-9A38-1077E9FDCC89";
	}
}
