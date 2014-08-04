using Newtonsoft.Json;

namespace Pushbullet.Api.Model
{
	public interface IIdentifiableObject
	{
		string Id { get; set; }
	}
}