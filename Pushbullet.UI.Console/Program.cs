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
					ConsoleHelpers.Write("API key has been successfully saved. You don't need to specify it in future.");
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
					devices.MyDevices.SingleOrDefault(device => PushbulletClient.GetDeviceName(device.Extras) == parsedArgs.Device)
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
					if (!Enum.TryParse(parsedArgs.Type, out pushType))
					{
						ConsoleHelpers.WriteErrorAndExit("Incorrect push type specified.");
					}
				}
				dynamic response = null;
				try
				{
					response = client.Push(targetDeviceId, pushType, parsedArgs.Title, parsedArgs.Body);
				}
				catch (Exception ex)
				{
					ConsoleHelpers.WriteErrorAndExit("Exception during pushing: " + ex.Message);
				}
				ConsoleHelpers.Write("Successfully pushed a {0}.", response.type);
				if (parsedArgs.ShowResponse)
				{
					ConsoleHelpers.Write("Response:\r\n{0}", response.ToString());
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