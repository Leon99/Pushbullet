using CommandLine;
using CommandLine.Text;

namespace Pushbullet.UI.Console
{
	internal class CLArguments
	{
		[Option('a', "apiKey", HelpText = 
"Pushbullet API key. When no other options specified, stored in the encrypted per-user storage.")]
		public string ApiKey { get; set; }


		[Option('d', "device", HelpText = 
			"Name of the target device.")]
		public string Device { get; set; }


		[Option('p', "type", HelpText = 
"Type of the push. Allowed values: note|link|address|list|file. When not specified, will be autodetected.")]
		public string Type { get; set; }


		[Option('t', "title", HelpText = 
			"Title of the push. Not supported for file pushes.")]
		public string Title { get; set; }


		[Option('b', "body", HelpText = 
"Body of the push. For file pushes, should be a path to the file. For list, individual items should be separated by a semicolon.")]
		public string Body { get; set; }

		[Option("response", HelpText = 
"Display the response from server (JSON).", DefaultValue = false)]
		public bool ShowResponse { get; set; }

		[HelpOption('h', "help")]
		public string GetUsage()
		{
			return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
		}
	}
}