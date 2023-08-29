namespace ExifLibrary
{
	public abstract class ExifProperty
	{
		protected ExifTag mTag;

		protected IFD mIFD;

		protected string mName;

		public ExifTag Tag => mTag;

		public IFD IFD => mIFD;

		public string Name
		{
			get
			{
				if (mName == null || mName.Length == 0)
				{
					return ExifTagFactory.GetTagName(mTag);
				}
				return mName;
			}
			set
			{
				mName = value;
			}
		}

		protected abstract object _Value
		{
			get;
			set;
		}

		public object Value
		{
			get
			{
				return _Value;
			}
			set
			{
				_Value = value;
			}
		}

		public abstract ExifInterOperability Interoperability
		{
			get;
		}

		public ExifProperty(ExifTag tag)
		{
			mTag = tag;
			mIFD = ExifTagFactory.GetTagIFD(tag);
		}
	}
}
