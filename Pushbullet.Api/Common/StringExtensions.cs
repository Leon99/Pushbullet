using System.Diagnostics.Contracts;
using System.Linq;

namespace Pushbullet.Api.Common
{
    public static class StringExtensions
    {
        public static bool StartsWithAny(this string s, params string[] values)
        {
			Contract.Requires(s != null);

            return values.Any(s.StartsWith);
        }
    }
}