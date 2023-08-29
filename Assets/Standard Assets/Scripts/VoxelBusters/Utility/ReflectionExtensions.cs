using System;
using System.Reflection;

namespace VoxelBusters.Utility
{
	public static class ReflectionExtensions
	{
		public static void SetFieldValue(this object _object, string _name, object _value)
		{
			_object.SetFieldValue(_object.GetType(), _name, _value);
		}

		private static void SetFieldValue(this object _object, Type _objectType, string _name, object _value)
		{
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			FieldInfo field = _objectType.GetField(_name, bindingAttr);
			if (field != null)
			{
				field.SetValue(_object, _value);
				return;
			}
			if (_objectType.BaseType != null)
			{
				_object.SetFieldValue(_objectType.BaseType, _name, _value);
				return;
			}
			throw new MissingFieldException($"[ReflectionExtension] Field {_name} not found.");
		}

		public static object GetStaticValue(this Type _objectType, string _name)
		{
			BindingFlags bindingAttr = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			FieldInfo field = _objectType.GetField(_name, bindingAttr);
			if (field != null)
			{
				return field.GetValue(null);
			}
			if (_objectType.BaseType != null)
			{
				return _objectType.BaseType.GetStaticValue(_name);
			}
			throw new MissingFieldException($"[ReflectionExtension] Field {_name} not found.");
		}

		public static void SetStaticValue(this Type _objectType, string _name, object _value)
		{
			BindingFlags bindingAttr = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			FieldInfo field = _objectType.GetField(_name, bindingAttr);
			if (field != null)
			{
				field.SetValue(null, _value);
			}
			else if (_objectType.BaseType != null)
			{
				_objectType.BaseType.SetStaticValue(_name, _value);
			}
		}

		public static void InvokeMethod(this object _object, string _method, object _value = null)
		{
			if (_object == null)
			{
				throw new NullReferenceException("Target Object is null.");
			}
			Type type = _object.GetType();
			object[] array = null;
			Type[] array2 = null;
			if (_value == null)
			{
				array = new object[0];
				array2 = new Type[0];
			}
			else
			{
				array = new object[1]
				{
					_value
				};
				array2 = new Type[1]
				{
					_value.GetType()
				};
			}
			InvokeMethod(_object, type, _method, array, array2);
		}

		public static void InvokeMethod(this object _object, string _method, object[] _argValues, Type[] _argTypes)
		{
			InvokeMethod(_object, _object.GetType(), _method, _argValues, _argTypes);
		}

		private static void InvokeMethod(object _object, Type _objectType, string _method, object[] _argValues, Type[] _argTypes)
		{
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.OptionalParamBinding;
			MethodInfo methodInfo = null;
			methodInfo = ((_argValues == null) ? _objectType.GetMethod(_method, bindingAttr) : _objectType.GetMethod(_method, bindingAttr, null, _argTypes, null));
			if (methodInfo != null)
			{
				methodInfo.Invoke(_object, _argValues);
				return;
			}
			if (_objectType.BaseType != null)
			{
				InvokeMethod(_object, _objectType.BaseType, _method, _argValues, _argTypes);
				return;
			}
			throw new MissingMethodException();
		}

		public static void InvokeStaticMethod(this Type _objectType, string _method, object[] _argValues, Type[] _argTypes)
		{
			BindingFlags bindingAttr = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.OptionalParamBinding;
			MethodInfo methodInfo = null;
			methodInfo = ((_argValues == null) ? _objectType.GetMethod(_method, bindingAttr) : _objectType.GetMethod(_method, bindingAttr, null, _argTypes, null));
			if (methodInfo != null)
			{
				methodInfo.Invoke(null, _argValues);
				return;
			}
			if (_objectType.BaseType != null)
			{
				_objectType.BaseType.InvokeStaticMethod(_method, _argValues, _argTypes);
				return;
			}
			throw new MissingMethodException();
		}

		public static T GetAttribute<T>(this Type _type, bool _inherit) where T : Attribute
		{
			object[] customAttributes = _type.GetCustomAttributes(_inherit);
			for (int i = 0; i < customAttributes.Length; i++)
			{
				if (customAttributes[i] as T != null)
				{
					return (T)customAttributes[i];
				}
			}
			return (T)null;
		}

		public static T GetAttribute<T>(this FieldInfo _fieldInfo, bool _inherit) where T : Attribute
		{
			object[] customAttributes = _fieldInfo.GetCustomAttributes(_inherit);
			for (int i = 0; i < customAttributes.Length; i++)
			{
				if (customAttributes[i] as T != null)
				{
					return (T)customAttributes[i];
				}
			}
			return (T)null;
		}

		public static FieldInfo GetFieldWithName(this Type _type, string _name, bool _isPublic)
		{
			BindingFlags bindingFlags = BindingFlags.Instance;
			bindingFlags = ((!_isPublic) ? (bindingFlags | BindingFlags.NonPublic) : (bindingFlags | BindingFlags.Public));
			FieldInfo field = _type.GetField(_name, bindingFlags);
			if (field == null)
			{
				Type baseType = _type.BaseType;
				if (baseType != null)
				{
					return baseType.GetFieldWithName(_name, _isPublic);
				}
			}
			return field;
		}
	}
}
