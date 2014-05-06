using System;
using System.Net;

namespace Pushbullet.Api
{
	public class PushbulletException : Exception
	{
		public PushbulletException(HttpStatusCode statusCode, string reason)
		{
			StatusCode = statusCode;
			Reason = reason;
		}

		public HttpStatusCode StatusCode { get; set; }
		public string Reason { get; set; }

		public override string Message
		{
			get
			{
				return string.Format("Pushbullet returned an error. Code: {0} ({1}). Reason: {2}.",
					Enum.Format(typeof (HttpStatusCode), StatusCode, "d"), StatusCode, Reason);
			}
		}
	}
}