using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pushbullet.Api.Common
{
	public static class DateTimeConvertors
	{
		public static DateTime UnixTimestampToDateTime(double unixTime)
		{
			var unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			var unixTimeStampInTicks = (long)(unixTime * TimeSpan.TicksPerSecond);
			return new DateTime(unixStart.Ticks + unixTimeStampInTicks);
		}
	}
}
