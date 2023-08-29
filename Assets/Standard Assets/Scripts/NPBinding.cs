using System;
using UnityEngine;
using VoxelBusters.DesignPatterns;
using VoxelBusters.NativePlugins;
using VoxelBusters.Utility;

[RequireComponent(typeof(PlatformBindingHelper))]
public class NPBinding : SingletonPattern<NPBinding>
{
	private static Sharing sharing;

	private static UI userInterface;

	private static Utility utility;

	public static Sharing Sharing
	{
		get
		{
			NPBinding instance = SingletonPattern<NPBinding>.Instance;
			if (instance == null)
			{
				return null;
			}
			if (sharing == null)
			{
				sharing = instance.AddComponentBasedOnPlatformOnlyIfRequired<Sharing>();
			}
			return sharing;
		}
	}

	public static UI UI
	{
		get
		{
			NPBinding instance = SingletonPattern<NPBinding>.Instance;
			if (instance == null)
			{
				return null;
			}
			if (userInterface == null)
			{
				userInterface = instance.AddComponentBasedOnPlatformOnlyIfRequired<UI>();
			}
			return userInterface;
		}
	}

	public static Utility Utility
	{
		get
		{
			NPBinding instance = SingletonPattern<NPBinding>.Instance;
			if (instance == null)
			{
				return null;
			}
			if (utility == null)
			{
				utility = instance.CachedGameObject.AddComponentIfNotFound<Utility>();
			}
			return utility;
		}
	}

	protected override void Init()
	{
		base.Init();
		if (!(SingletonPattern<NPBinding>.instance != this))
		{
			if (sharing == null)
			{
				sharing = AddComponentBasedOnPlatformOnlyIfRequired<Sharing>();
			}
			if (userInterface == null)
			{
				userInterface = AddComponentBasedOnPlatformOnlyIfRequired<UI>();
			}
			if (utility == null)
			{
				utility = base.CachedGameObject.AddComponentIfNotFound<Utility>();
			}
		}
	}

	private T AddComponentBasedOnPlatformOnlyIfRequired<T>() where T : MonoBehaviour
	{
		T component = GetComponent<T>();
		if ((UnityEngine.Object)component != (UnityEngine.Object)null)
		{
			return component;
		}
		Type typeFromHandle = typeof(T);
		string str = typeFromHandle.ToString();
		string text = null;
		text = str + "Android";
		if (!string.IsNullOrEmpty(text))
		{
			Type type = typeFromHandle.Assembly.GetType(text, throwOnError: false);
			return base.CachedGameObject.AddComponent(type) as T;
		}
		return base.CachedGameObject.AddComponent<T>();
	}
}
