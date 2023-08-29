using UnityEngine;

namespace I2.Loc
{
	public abstract class LocalizeTarget<T> : ILocalizeTarget where T : Object
	{
		public T mTarget;

		public override bool IsValid(Localize cmp)
		{
			if ((Object)mTarget != (Object)null)
			{
				Component component = mTarget as Component;
				if (component != null && component.gameObject != cmp.gameObject)
				{
					mTarget = (T)null;
				}
			}
			if ((Object)mTarget == (Object)null)
			{
				mTarget = cmp.GetComponent<T>();
			}
			return (Object)mTarget != (Object)null;
		}
	}
}
