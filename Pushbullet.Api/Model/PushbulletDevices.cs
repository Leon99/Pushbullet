using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Pushbullet.Api.Model
{
	public class PushbulletDevice : IIdentifiableObject
    {
		public string Name
		{
			get { return Nickname ?? Model; }
		}

        [JsonProperty("iden")]
        public string Id { get; set; }

        [JsonProperty("push_token")]
        public string PushToken { get; set; }

        [JsonProperty("app_version")]
        public int AppVersion { get; set; }

        [JsonProperty("android_sdk_version")]
        public string AndroidSdkVersion { get; set; }

        [JsonProperty("fingerprint")]
        public string Fingerprint { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty("type")]
        public PushbulletDeviceType Type { get; set; }

        [JsonProperty("created")]
        public double CreatedRaw { get; set; }

        [JsonProperty("modified")]
		public double ModifiedRaw { get; set; }

        [JsonProperty("android_version")]
        public string AndroidVersion { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("pushable")]
        public bool Pushable { get; set; }
    }


    public class PushbulletDevices
    {
        [JsonProperty("devices")]
        public PushbulletDevice[] Devices { get; set; }
    }
}