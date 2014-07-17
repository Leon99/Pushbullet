namespace Pushbullet.UI.Win81.Common
{
	public class OrientationStateMessage
	{
		public OrientationStateMessage(PageOrientations orientation)
		{
			Orientation = orientation;
		}

		public PageOrientations Orientation { get; private set; }
	}
}