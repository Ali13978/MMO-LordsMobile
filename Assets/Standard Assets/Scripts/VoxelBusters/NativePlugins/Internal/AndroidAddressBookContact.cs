using System.Collections;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins.Internal
{
	public sealed class AndroidAddressBookContact : AddressBookContact
	{
		private const string kDisplayName = "display-name";

		private const string kFamilyName = "family-name";

		private const string kGivenName = "given-name";

		private const string kImagePath = "image-path";

		private const string kPhoneNumList = "phone-number-list";

		private const string kEmailList = "email-list";

		public AndroidAddressBookContact(IDictionary _contactInfoJsontDict)
		{
			string ifAvailable = _contactInfoJsontDict.GetIfAvailable<string>("given-name");
			string ifAvailable2 = _contactInfoJsontDict.GetIfAvailable<string>("family-name");
			base.FirstName = ifAvailable;
			base.LastName = ifAvailable2;
			base.ImagePath = _contactInfoJsontDict.GetIfAvailable<string>("image-path");
			IList ifAvailable3 = _contactInfoJsontDict.GetIfAvailable<IList>("phone-number-list");
			string[] array = null;
			if (ifAvailable3 != null)
			{
				int count = ifAvailable3.Count;
				array = new string[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = (string)ifAvailable3[i];
				}
			}
			base.PhoneNumberList = array;
			IList ifAvailable4 = _contactInfoJsontDict.GetIfAvailable<IList>("email-list");
			string[] array2 = null;
			if (ifAvailable4 != null)
			{
				int count2 = ifAvailable4.Count;
				array2 = new string[count2];
				for (int j = 0; j < count2; j++)
				{
					array2[j] = (string)ifAvailable4[j];
				}
			}
			base.EmailIDList = array2;
		}
	}
}
