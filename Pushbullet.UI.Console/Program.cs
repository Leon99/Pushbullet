using System;
using System.Linq;
using CommandLine;
using Pushbullet.Api;
using Pushbullet.Api.Model;
using Pushbullet.UI.Console.Shared;

namespace Pushbullet.UI.Console
{
	internal class Program
	{
		internal const string ApplicationName = "Pushbullet Console";

		private static void Main(string[] args)
		{
			CLArguments parsedArgs = ParseArgs(args);
			if (parsedArgs != null)
			{
				if (string.IsNullOrEmpty(parsedArgs.ApiKey) && string.IsNullOrEmpty(parsedArgs.Device))
				{
					ConsoleHelpers.WriteErrorAndExit("Invalid arguments. Run with -h argument to see the usage reference.");
				}
				if (!string.IsNullOrEmpty(parsedArgs.Device))
				{
					if (string.IsNullOrEmpty(parsedArgs.ApiKey))
					{
						parsedArgs.ApiKey = SecureStorage.LoadToken();
						if (parsedArgs.ApiKey == null)
						{
							ConsoleHelpers.WriteErrorAndExit("No saved API key found.");
						}
					}
					DoPush(parsedArgs);
				}
				else
				{
					SecureStorage.SaveToken(parsedArgs.ApiKey);
				    System.Console.WriteLine("API key has been successfully saved. You don't need to specify it any more.");
				}
			}
			ConsoleHelpers.WaitInDebug();
		}

		private static void DoPush(CLArguments parsedArgs)
		{
			using (var client = new PushbulletClient(parsedArgs.ApiKey))
			{
				PushbulletDevices devices = client.GetDevices();
				string targetDeviceId = (
					devices.MyDevices.SingleOrDefault(device => String.Compare(PushbulletClient.GetDeviceName(device.Extras), parsedArgs.Device, StringComparison.OrdinalIgnoreCase) == 0)
					??
					new PushbulletDevice()
					).Id;

				if (string.IsNullOrEmpty(targetDeviceId))
				{
					ConsoleHelpers.WriteErrorAndExit("No device found with the given name.");
				}

				// validate or autodetect push type
				PushbulletMessageType pushType;
				if (String.IsNullOrEmpty(parsedArgs.Type))
				{
					pushType = PushbulletClient.DetectType(parsedArgs.Body);
				}
				else
				{
					if (!Enum.TryParse(parsedArgs.Type, true, out pushType))
					{
						ConsoleHelpers.WriteErrorAndExit("Incorrect push type specified.");
					}
				}
				dynamic response = null;
				try
				{
				    System.Console.Write("Pushing {0} {1}... ", pushType != PushbulletMessageType.Address ? "a" : "an", pushType.ToString().ToLowerInvariant());
				    response = client.Push(targetDeviceId, pushType, parsedArgs.Title, parsedArgs.Body);
				    System.Console.WriteLine("SUCCESS");
				}
				catch (Exception ex)
				{
					ConsoleHelpers.WriteErrorAndExit("Exception during pushing: " + ex.Message);
				}
			    if (parsedArgs.ShowResponse)
			    {
			        System.Console.WriteLine("Response:\r\n{0}", new[] {response.ToString()});
			    }
			}
		}

		private static CLArguments ParseArgs(string[] args)
		{
			var parsedArgs = new CLArguments();
			if (Parser.Default.ParseArguments(args, parsedArgs))
			{
				return parsedArgs;
			}
			else
			{
				return null;
			}
		}
	}
}