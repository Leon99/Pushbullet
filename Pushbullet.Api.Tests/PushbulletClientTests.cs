using NUnit.Framework;
using Pushbullet.Api.Model;

namespace Pushbullet.Api.Tests
{
	[TestFixture]
	public class PushbulletClientTests
	{
	
		#region GetDeviceName

		[Test]
		public void GetDeviceName_OnlyNicknameSpecified_ReturnsNickname()
		{
			const string nickname = "My Device";
			var extras = new PushbulletDeviceExtras {Nickname = nickname};

			var deviceName = PushbulletClient.GetDeviceName(extras);

			Assert.AreEqual(nickname, deviceName);
		}

		[Test]
		public void GetDeviceName_OnlyModelSpecified_ReturnsModel()
		{
			const string model = "My Device Model";
			var extras = new PushbulletDeviceExtras {Model = model};

			var deviceName = PushbulletClient.GetDeviceName(extras);

			Assert.AreEqual(model, deviceName);
		}

		[Test]
		public void GetDeviceName_ModelAndNicknameSpecified_ReturnsNickname()
		{
			const string nickname = "My Device";
			const string model = "My Device Model";
			var extras = new PushbulletDeviceExtras { Nickname = nickname, Model = model };

			var deviceName = PushbulletClient.GetDeviceName(extras);

			Assert.AreEqual(nickname, deviceName);
		}

		#endregion

		#region DetectType

		[Test]
		public void DetectType_BodyWithHttpLink_ReturnsLinkType()
		{
			const string body = "http://fakedomain.au/subpage";

			var detectedType = PushbulletClient.DetectType(body);

			Assert.AreEqual(PushbulletMessageType.Link, detectedType);
		}

		[Test]
		public void DetectType_BodyWithHttpsLink_ReturnsLinkType()
		{
			const string body = "https://www.fakedomain.au/subpage";

			var detectedType = PushbulletClient.DetectType(body);

			Assert.AreEqual(PushbulletMessageType.Link, detectedType);
		}

		[Test]
		public void DetectType_BodyWithWww_ReturnsLinkType()
		{
			const string body = "www.fakedomain.au/subpage";

			var detectedType = PushbulletClient.DetectType(body);

			Assert.AreEqual(PushbulletMessageType.Link, detectedType);
		}

		[Test]
		public void DetectType_EmptyBody_ReturnsNoteType()
		{
			const string body = "";

			var detectedType = PushbulletClient.DetectType(body);

			Assert.AreEqual(PushbulletMessageType.Note, detectedType);
		}

		[Test]
		public void DetectType_NullBody_ReturnsNoteType()
		{
			const string body = null;

			var detectedType = PushbulletClient.DetectType(body);

			Assert.AreEqual(PushbulletMessageType.Note, detectedType);
		}

		[Test]
		public void DetectType_BodyWithFilePath_ReturnsFileType()
		{
			const string body = @"c:\temp\test.jpg";

			var detectedType = PushbulletClient.DetectType(body);

			Assert.AreEqual(PushbulletMessageType.File, detectedType);
		}

		[Test]
		public void DetectType_BodyWithFileScheme_ReturnsFileType()
		{
			const string body = "file:///c:/temp/test.jpg";

			var detectedType = PushbulletClient.DetectType(body);

			Assert.AreEqual(PushbulletMessageType.File, detectedType);
		}

		[Test]
		public void DetectType_BodyWithList_ReturnsListType()
		{
			const string body = "item 1;item 2;item 3";

			var detectedType = PushbulletClient.DetectType(body);

			Assert.AreEqual(PushbulletMessageType.List, detectedType);
		}

		[Test]
		public void DetectType_RegularText_ReturnsNoteType()
		{
			const string body = "just a note";

			var detectedType = PushbulletClient.DetectType(body);

			Assert.AreEqual(PushbulletMessageType.Note, detectedType);
		}

		#endregion
		
	}
}
