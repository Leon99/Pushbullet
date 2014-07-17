using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PCLStorage;
using Pushbullet.Api;
using Pushbullet.Api.Model;

namespace Pushbullet.Api
{
	public partial class PushbulletClient
	{
		private HttpResponseMessage SendFile(string deviceId, string filePath)
		{
			using (var content = new MultipartFormDataContent())
			{
				content.Add(CreateContent("device_iden", deviceId));
				content.Add(CreateContent("type", "file"));
				content.Add(CreateContent(filePath));

				return _client.PostAsync(PushbulletApiConstants.PushesUrl, content).Result;
			}
		}

		private static StreamContent CreateContent(string filePath)
		{
			IFile file = FileSystem.Current.GetFileFromPathAsync(filePath).Result;
			Stream stream = file.OpenAsync(FileAccess.Read).Result;

			var fileContent = new StreamContent(stream);
			HttpContentHeaders contentHeaders = fileContent.Headers;
			contentHeaders.ContentDisposition = CreateFormDataHeader(PushbulletPushType.File.ToString().ToLowerInvariant());
			contentHeaders.ContentDisposition.FileName = "\"" + file.Name + "\"";
			contentHeaders.ContentType = new MediaTypeHeaderValue("application/octet-stream");
			return fileContent;
		}

		private static StringContent CreateContent(string name, string content)
		{
			var fileContent = new StringContent(content);
			fileContent.Headers.ContentDisposition = CreateFormDataHeader(name);
			return fileContent;
		}

		private static ContentDispositionHeaderValue CreateFormDataHeader(string name)
		{
			return new ContentDispositionHeaderValue("form-data")
			{
				Name = "\"" + name + "\""
			};
		}
	}
}