using System.Collections.Generic;
using Newtonsoft.Json;

namespace Pushbullet.Api.Model
{
	public class PushbulletDevices
	{
		[JsonProperty(PropertyName = "devices")]
		public List<PushbulletDevice> MyDevices { get; set; }

		public List<PushbulletDevice> SharedDevices { get; set; }
	}

	public class PushbulletDevice
	{
		[JsonProperty(PropertyName = "iden")]
		public string Id { get; set; }

		public PushbulletDeviceExtras Extras { get; set; }
	}

	public class PushbulletDeviceExtras
	{
		public string Manufacturer { get; set; }
		public string Model { get; set; }
		public string AndroidVersion { get; set; }
		public string SdkVersion { get; set; }
		public string AppVersion { get; set; }
		public string Nickname { get; set; }
	}
}