using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.DebugPRO;

public class AndroidPluginUtility
{
	private static Dictionary<string, AndroidJavaObject> sSingletonInstances = new Dictionary<string, AndroidJavaObject>();

	public static AndroidJavaObject GetSingletonInstance(string _className, string _methodName = "getInstance")
	{
		sSingletonInstances.TryGetValue(_className, out AndroidJavaObject value);
		if (value == null)
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(_className);
			if (androidJavaClass == null)
			{
				Console.LogError("Native Plugins", "Class=" + _className + " not found!");
				return null;
			}
			value = androidJavaClass.CallStatic<AndroidJavaObject>(_methodName, new object[0]);
			sSingletonInstances.Add(_className, value);
		}
		return value;
	}

	public static AndroidJavaClass CreateClassObject(string _className)
	{
		AndroidJavaClass androidJavaClass = new AndroidJavaClass(_className);
		if (androidJavaClass == null)
		{
			Console.LogError("Native Plugins", "Class=" + _className + " not found!");
		}
		return androidJavaClass;
	}
}
