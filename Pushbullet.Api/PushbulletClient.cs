using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PCLStorage;
using Pushbullet.Api.Model;
using Pushbullet.Api.Shared;

namespace Pushbullet.Api
{
	public class PushbulletClient : IDisposable
	{
		#region Helper methods

		public static string GetDeviceName(PushbulletDeviceExtras extras)
		{
			Contract.Requires(extras != null);

			return extras.Nickname ?? extras.Model;
		}

		public static PushbulletMessageType DetectType(string body)
		{
			if (string.IsNullOrEmpty(body))
			{
				return PushbulletMessageType.Note;
			}
			if (body.Contains(@":\") || body.StartsWith("file://"))
			{
				return PushbulletMessageType.File;
			}
			if (body.StartsWithAny("http://", "https://", "www."))
			{
				return PushbulletMessageType.Link;
			}
			if (body.Contains(";"))
			{
				return PushbulletMessageType.List;
			}
			return PushbulletMessageType.Note;
		}

		#endregion

		private const string BaseApiUrl = "https://api.pushbullet.com/api/";
		private const string PushUrl = BaseApiUrl + "pushes";
		
		private readonly HttpClient _client;

		public PushbulletClient(string apiKey)
		{
			Contract.Requires(!string.IsNullOrEmpty(apiKey));

			_client = new HttpClient();
			string authEncoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(apiKey + ":"));
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authEncoded);
		}

		public void Dispose()
		{
			if (_client != null)
			{
				_client.Dispose();
			}
		}

		public PushbulletDevices GetDevices()
		{
			const string url = BaseApiUrl + "devices";

			string devices = _client.GetStringAsync(url).Result;
			return JsonConvert.DeserializeObject<PushbulletDevices>(devices);
		}

		public dynamic Push(string deviceId, PushbulletMessageType type, string title, string body)
		{
			HttpResponseMessage response =
				type != PushbulletMessageType.File
					? SendText(deviceId, type, title, body)
					: SendFile(deviceId, body);
			string responseString;
			if (response.IsSuccessStatusCode)
			{
				responseString = response.Content.ReadAsStringAsync().Result;
			}
			else
			{
				throw new PushbulletException(response.StatusCode, response.ReasonPhrase);
			}

			return JObject.Parse(responseString);
		}

		private HttpResponseMessage SendText(string deviceId, PushbulletMessageType type, string title, string body)
		{
			var data = new KeyValuePairList<string, string>
			{
				{"device_iden", deviceId},
				{"type", type.ToString().ToLowerInvariant()},
				{
					type != PushbulletMessageType.Address
						? "title"
						: "name",
					title
				}
			};
			switch (type)
			{
				case PushbulletMessageType.Note:
					data.Add("body", body);
					break;
				case PushbulletMessageType.Link:
					data.Add("url", body);
					break;
				case PushbulletMessageType.Address:
					data.Add("address", body);
					break;
				case PushbulletMessageType.List:
					foreach (var item in body.Split(';'))
					{
						data.Add("items", item);
					}
					break;
			}
			using (var content = new FormUrlEncodedContent(data))
			{
				return _client.PostAsync(PushUrl, content).Result;
			}
		}

		#region File push routines

		private HttpResponseMessage SendFile(string deviceId, string filePath)
		{
			using (var content = new MultipartFormDataContent())
			{
				content.Add(CreateContent("device_iden", deviceId));
				content.Add(CreateContent("type", "file"));
				content.Add(CreateContent(filePath));

				return _client.PostAsync(PushUrl, content).Result;
			}
		}

		private static StreamContent CreateContent(string filePath)
		{
			IFile file = FileSystem.Current.GetFileFromPathAsync(filePath).Result;
			Stream stream = file.OpenAsync(FileAccess.Read).Result;

			var fileContent = new StreamContent(stream);
			HttpContentHeaders contentHeaders = fileContent.Headers;
			contentHeaders.ContentDisposition = CreateFormDataHeader(PushbulletMessageType.File.ToString().ToLowerInvariant());
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

		#endregion
	}
}