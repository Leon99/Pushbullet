using System;
using System.Diagnostics.Contracts;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pushbullet.Api.Common;
using Pushbullet.Api.Model;

namespace Pushbullet.Api
{
	public partial class PushbulletClient : IDisposable
	{
		#region Helper methods

		public static PushbulletPushType DetectType(string body)
		{
			if (string.IsNullOrEmpty(body))
			{
				return PushbulletPushType.Note;
			}
			if (body.Contains(@":\") || body.StartsWith("file://"))
			{
				return PushbulletPushType.File;
			}
			if (body.StartsWithAny("http://", "https://", "www."))
			{
				return PushbulletPushType.Link;
			}
			if (body.Contains(";"))
			{
				return PushbulletPushType.List;
			}
			return PushbulletPushType.Note;
		}

		#endregion

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
			var response = _client.GetAsync(PushbulletApiConstants.DevicesUrl).Result;
			var responseString = HandleResponse(response);
			return JsonConvert.DeserializeObject<PushbulletDevices>(responseString);
		}


		public dynamic Push(string deviceId, PushbulletPushType type, string title, string body)
		{
			var response = type != PushbulletPushType.File
					? SendText(deviceId, type, title, body)
					: SendFile(deviceId, body);
			var responseString = HandleResponse(response);
			return JObject.Parse(responseString);
		}


		private static string HandleResponse(HttpResponseMessage response)
		{
			string responseString;
			if (response.IsSuccessStatusCode)
			{
				responseString = response.Content.ReadAsStringAsync().Result;
			}
			else
			{
				throw new PushbulletException(response.StatusCode, response.ReasonPhrase);
			}
			return responseString;
		}


		private HttpResponseMessage SendText(string deviceId, PushbulletPushType type, string title, string content)
		{
			var postData = new KeyValuePairList<string, string>
			{
				{"device_iden", deviceId},
				{"type", type.ToString().ToLowerInvariant()},
				{
					type != PushbulletPushType.Address
						? "title"
						: "name",
					title
				}
			};
			switch (type)
			{
				case PushbulletPushType.Note:
					postData.Add("body", content);
					break;
				case PushbulletPushType.Link:
					postData.Add("url", content);
					break;
				case PushbulletPushType.Address:
					postData.Add("address", content);
					break;
				case PushbulletPushType.List:
					foreach (var item in content.Split(';'))
					{
						postData.Add("items", item);
					}
					break;
			}
			using (var postContent = new FormUrlEncodedContent(postData))
			{
				return _client.PostAsync(PushbulletApiConstants.PushesUrl, postContent).Result;
			}
		}


		public PushbulletUser GetCurrentUser()
		{
			var response = _client.GetAsync(PushbulletApiConstants.CurrentUserUrl).Result;
			var responseString = HandleResponse(response);
			return JsonConvert.DeserializeObject<PushbulletUser>(responseString);
		}

		public PushbulletPushes GetPushes()
		{
			var response = _client.GetAsync(PushbulletApiConstants.PushesUrl).Result;
			var responseString = HandleResponse(response);
			return JsonConvert.DeserializeObject<PushbulletPushes>(responseString, new PushJsonConverter());
		}
	}
}