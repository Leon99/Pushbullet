using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Pushbullet.Api.Model
{
	public class PushJsonConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JObject item = JObject.Load(reader);
			if (item["active"].Value<bool>())
			{
				var pushType = (PushbulletPushType)Enum.Parse(typeof(PushbulletPushType), (string)item["type"], true);
				switch (pushType)
				{
					case PushbulletPushType.Note:
						return item.ToObject<NotePush>();
					case PushbulletPushType.Link:
						return item.ToObject<LinkPush>();
					case PushbulletPushType.List:
						return item.ToObject<ListPush>();
					case PushbulletPushType.Address:
						return item.ToObject<AddressPush>();
					case PushbulletPushType.File:
						return item.ToObject<FilePush>();
					default:
						return item.ToObject<NotePush>();
				}
			}
			else
			{
				return item.ToObject<InactivePush>();
			}
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(InactivePush);
		}
	}
}