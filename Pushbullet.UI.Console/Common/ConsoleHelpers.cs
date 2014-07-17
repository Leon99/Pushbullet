using System;
using System.Diagnostics;

namespace Pushbullet.UI.Console.Common
{
	internal static class ConsoleHelpers
	{
	    public static void WriteErrorAndExit(string message)
		{
			System.Console.WriteLine("ERROR: " + message);
			WaitInDebug();
			Environment.Exit(-1);
		}

		public static void WaitInDebug()
		{
#if DEBUG
			if (Debugger.IsAttached)
			{
				System.Console.WriteLine();
				System.Console.Write("Press ENTER to continue.");
				System.Console.ReadLine();
			}
#endif
		}
	}
}