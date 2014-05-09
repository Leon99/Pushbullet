using System.Linq;

namespace Pushbullet.Api
{
    public static class StringExtensions
    {
        public static bool StartsWithAny(this string s, params string[] values)
        {
            return values.Any(s.StartsWith);
        }
    }
}