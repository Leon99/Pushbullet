using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Pushbullet.UI.Console.Shared
{
	class SecureStorage
	{
		private static readonly string DirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Program.ApplicationName);
		private const string FileName = "token";

		public static void SaveToken(string token)
		{
			byte[] plaintextBytes = Encoding.ASCII.GetBytes(token);
			byte[] encodedBytes = ProtectedData.Protect(plaintextBytes, null, DataProtectionScope.CurrentUser);
			using (var fileStream = CreateFile())
			{
				fileStream.Write(encodedBytes, 0, encodedBytes.Length);
			}
		}

		public static string LoadToken()
		{
			byte[] encodedBytes;
			using (var fileStream = OpenFile())
			{
				if (fileStream == null)
				{
					return null;
				}
				encodedBytes = new byte[fileStream.Length];
				fileStream.Read(encodedBytes, 0, encodedBytes.Length);
			}
			byte[] decodedBytes = ProtectedData.Unprotect(encodedBytes, null, DataProtectionScope.CurrentUser);

			return Encoding.ASCII.GetString(decodedBytes);
		}

		private static FileStream CreateFile()
		{
			var appDir = new DirectoryInfo(DirectoryPath);
			if (!appDir.Exists)
			{
				appDir.Create();
			}

			return File.Create(Path.Combine(DirectoryPath, FileName));
		}

		private static FileStream OpenFile()
		{
			var file = new FileInfo(Path.Combine(DirectoryPath, FileName));
			if (!file.Exists)
			{
				return null;
			}
			return file.OpenRead();
		}
	}
}
