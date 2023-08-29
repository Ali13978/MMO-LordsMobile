using System.Collections;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins.Internal
{
	public sealed class iOSAddressBookContact : AddressBookContact
	{
		private const string kLastName = "last-name";

		private const string kImagePath = "image-path";

		private const string kFirstName = "first-name";

		private const string kPhoneNumList = "phone-number-list";

		private const string kEmailIDList = "emailID-list";

		public iOSAddressBookContact(IDictionary _contactInfoDict)
		{
			base.FirstName = _contactInfoDict.GetIfAvailable<string>("first-name");
			base.LastName = _contactInfoDict.GetIfAvailable<string>("last-name");
			base.ImagePath = _contactInfoDict.GetIfAvailable<string>("image-path");
			IList ifAvailable = _contactInfoDict.GetIfAvailable<IList>("phone-number-list");
			string[] array = null;
			if (ifAvailable != null)
			{
				int count = ifAvailable.Count;
				array = new string[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = (string)ifAvailable[i];
				}
			}
			base.PhoneNumberList = array;
			IList ifAvailable2 = _contactInfoDict.GetIfAvailable<IList>("emailID-list");
			string[] array2 = null;
			if (ifAvailable2 != null)
			{
				int count2 = ifAvailable2.Count;
				array2 = new string[count2];
				for (int j = 0; j < count2; j++)
				{
					array2[j] = (string)ifAvailable2[j];
				}
			}
			base.EmailIDList = array2;
		}
	}
}
