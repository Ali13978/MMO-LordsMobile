using UnityEngine;

namespace I2.Loc
{
	public class LocalizeTargetDesc_Type<T, G> : LocalizeTargetDesc<G> where T : Object where G : LocalizeTarget<T>
	{
		public override bool CanLocalize(Localize cmp)
		{
			return (Object)cmp.GetComponent<T>() != (Object)null;
		}

		public override ILocalizeTarget CreateTarget(Localize cmp)
		{
			T component = cmp.GetComponent<T>();
			if ((Object)component == (Object)null)
			{
				return null;
			}
			G val = ScriptableObject.CreateInstance<G>();
			val.mTarget = component;
			return val;
		}
	}
}
