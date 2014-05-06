using System.IO;
using System.IO.IsolatedStorage;
using System.Security.Cryptography;
using System.Text;

namespace Pushbullet.UI.Console.Shared
{
	class SecureStorage
	{
		private const string FileName = "token";

		public static void SaveToken(string token)
		{
			byte[] plaintextBytes = Encoding.ASCII.GetBytes(token);
			byte[] encodedBytes = ProtectedData.Protect(plaintextBytes, null, DataProtectionScope.CurrentUser);
			using (IsolatedStorageFile isoFile = IsolatedStorageFile.GetStore(IsolatedStorageScope.Assembly | IsolatedStorageScope.User | IsolatedStorageScope.Roaming, null, null))
			{
				isoFile.CreateFile(FileName).Write(encodedBytes, 0, encodedBytes.Length);
			}
		}

		public static string LoadToken()
		{
			byte[] encodedBytes;
			using (IsolatedStorageFile isoFile = IsolatedStorageFile.GetStore(IsolatedStorageScope.Assembly | IsolatedStorageScope.User | IsolatedStorageScope.Roaming, null, null))
			{
				if (!isoFile.FileExists(FileName))
				{
					return null;
				}
				using (var stream = isoFile.OpenFile(FileName, FileMode.Open))
				{
					encodedBytes = new byte[stream.Length];
					stream.Read(encodedBytes, 0, encodedBytes.Length);
				}
			}
			byte[] decodedBytes = ProtectedData.Unprotect(encodedBytes, null, DataProtectionScope.CurrentUser);

			return Encoding.ASCII.GetString(decodedBytes);
		}
	}
}
