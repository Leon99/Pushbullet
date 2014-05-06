using System;

namespace Pushbullet.UI.Console.Shared
{
	internal static class ConsoleHelpers
	{
		internal static void Write(string message)
		{
			System.Console.WriteLine(message);			
		}

		internal static void Write(string message, params object[] args)
		{
				System.Console.WriteLine(message, args);
		}

		public static void WriteErrorAndExit(string message)
		{
			System.Console.WriteLine("ERROR: " + message);
			WaitInDebug();
			Environment.Exit(-1);
		}

		public static void WaitInDebug()
		{
#if DEBUG
			System.Console.ReadLine();
#endif
		}
	}
}