using NUnit.Framework;
using Pushbullet.Api.Common;

namespace Pushbullet.Api.Tests.Common
{
	[TestFixture]
	public class KeyValuePairListTests
	{
		[Test]
		public void InnerList_JustCreated_NoItemsAdded()
		{
			var list = new KeyValuePairList<string, string>();

			Assert.AreEqual(0, list.InnerList.Count);
		}

		[Test]
		public void Add_OneItem_AddsCorrectItem()
		{
			var list = new KeyValuePairList<string, string>();
			const string key = "key1";
			const string value = "value1";
			
			list.Add(key, value);

			Assert.AreEqual(key, list.InnerList[0].Key);
			Assert.AreEqual(value, list.InnerList[0].Value);
		}

		[Test]
		public void Add_SomeItems_ItemsAdded()
		{
			var list = new KeyValuePairList<string, string>
			{
				{"key1", "value1"},
				{"key2", "value2"},
				{"key3", "value3"}
			};

			Assert.AreEqual(3, list.InnerList.Count);
		}

		[Test]
		public void IEnumerable_ListWithSomeItems_EnumeratesAllItems()
		{
			var list = new KeyValuePairList<string, string>
			{
				{"key1", "value1"}, 
				{"key2", "value2"}, 
				{"key3", "value3"}
			};

			int itemsCnt = 0;
			foreach (var pair in list)
			{
				itemsCnt++;
			}

			Assert.AreEqual(3, itemsCnt);
		}
	}
}
