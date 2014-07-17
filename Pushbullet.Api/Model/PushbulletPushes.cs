using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Pushbullet.Api.Common;

namespace Pushbullet.Api.Model
{
	public interface ICaptionedPush
	{
		string Caption { get; }
	}

	public class PushbulletPushes
	{
		[JsonProperty("pushes")]
		public InactivePush[] Pushes { get; set; }
	}


	public class InactivePush
	{
		[JsonProperty("iden")]
		public string Id { get; set; }

		[JsonProperty("active")]
		public bool Active { get; set; }

		[JsonProperty("created")]
		public double CreatedRaw { get; set; }

		public DateTime Created
		{
			get { return DateTimeConvertors.UnixTimestampToDateTime(CreatedRaw); }
		}

		[JsonProperty("modified")]
		public double ModifiedRaw { get; set; }

		public DateTime Modified
		{
			get { return DateTimeConvertors.UnixTimestampToDateTime(ModifiedRaw); }
		}

	}



	public abstract class ActivePush : InactivePush
	{
		[JsonProperty("type")]
		public PushbulletPushType Type { get; set; }

		[JsonProperty("dismissed")]
		public bool Dismissed { get; set; }

		[JsonProperty("owner_iden")]
		public string OwnerId { get; set; }

		[JsonProperty("target_device_iden")]
		public string TargetDeviceIden { get; set; }

		[JsonProperty("sender_iden")]
		public string SenderId { get; set; }

		[JsonProperty("sender_email")]
		public string SenderEmail { get; set; }

		[JsonProperty("sender_email_normalized")]
		public string SenderEmailNormalized { get; set; }

		[JsonProperty("receiver_iden")]
		public string ReceiverId { get; set; }

		[JsonProperty("receiver_email")]
		public string ReceiverEmail { get; set; }

		[JsonProperty("receiver_email_normalized")]
		public string ReceiverEmailNormalized { get; set; }
	}



	public abstract class TitledPush : ActivePush, ICaptionedPush
	{
		[JsonProperty("title")]
		public string Title { get; set; }

		public string Caption
		{
			get { return Title; }
		}
	}



	public class NotePush : TitledPush
	{
		public NotePush()
		{
			Type = PushbulletPushType.Note;
		}

		[JsonProperty("body")]
		public string Body { get; set; }
	}



	public class LinkPush : TitledPush
	{
		public LinkPush()
		{
			Type = PushbulletPushType.Link;
		}

		[JsonProperty("url")]
		public string Url { get; set; }
	}



	public class ListPush : TitledPush
	{
		public ListPush()
		{
			Type = PushbulletPushType.List;
		}

		[JsonProperty("items")]
		public ListPushItem[] Items { get; set; }
	}



	public class ListPushItem
	{
		[JsonProperty("text")]
		public string Text { get; set; }

		[JsonProperty("checked")]
		public bool Checked { get; set; }
	}


	public class AddressPush : ActivePush, ICaptionedPush
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("address")]
		public string Address { get; set; }

		public string Caption
		{
			get { return Name; }
		}
	}



	public class FilePush : ActivePush, ICaptionedPush
	{
		[JsonProperty("file_name")]
		public string FileName { get; set; }

		[JsonProperty("file_type")]
		public string FileType { get; set; }

		[JsonProperty("file_url")]
		public string FileUrl { get; set; }

		public string Caption
		{
			get { return FileName; }
		}
	}
}