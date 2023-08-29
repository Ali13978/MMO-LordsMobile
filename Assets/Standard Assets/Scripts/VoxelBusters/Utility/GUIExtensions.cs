using System;
using System.Collections;
using UnityEngine;

namespace VoxelBusters.Utility
{
	public class GUIExtensions
	{
		public static string TextArea(string _text, Rect _normalisedBounds)
		{
			Rect screenSpaceBounds = GetScreenSpaceBounds(_normalisedBounds);
			GUILayout.BeginArea(screenSpaceBounds);
			if (_text != null)
			{
				_text = GUILayout.TextArea(_text, GUILayout.Width(screenSpaceBounds.width), GUILayout.Height(screenSpaceBounds.height));
			}
			GUILayout.EndArea();
			return _text;
		}

		public static void Buttons(IList _buttonsList, Action<string> _callbackOnPress, Rect _normalisedBounds)
		{
			if (_buttonsList != null)
			{
				Rect screenSpaceBounds = GetScreenSpaceBounds(_normalisedBounds);
				float num = (float)Screen.height * 0.05f;
				float num2 = (float)Screen.height * 0.01f;
				float num3 = num + num2;
				int count = _buttonsList.Count;
				int num4 = (int)(screenSpaceBounds.height / num3);
				int num5 = count / num4 + ((count % num4 != 0) ? 1 : 0);
				GUILayoutOption[] layoutOptions = new GUILayoutOption[1]
				{
					GUILayout.Height(num)
				};
				GUILayout.BeginArea(screenSpaceBounds);
				GUILayout.BeginHorizontal();
				for (int i = 0; i < num5; i++)
				{
					DrawButtonsLayout(_buttonsList, _callbackOnPress, i * num4, num4, layoutOptions);
				}
				GUILayout.EndHorizontal();
				GUILayout.EndArea();
			}
		}

		private static void DrawButtonsLayout(IList _buttonsList, Action<string> _callbackOnPress, int _startingIndex, int _buttonsPerColumn, params GUILayoutOption[] _layoutOptions)
		{
			int count = _buttonsList.Count;
			int num = _startingIndex + _buttonsPerColumn;
			GUILayout.BeginVertical();
			for (int i = _startingIndex; i < num && i < count; i++)
			{
				string text = _buttonsList[i] as string;
				GUILayout.FlexibleSpace();
				if (GUILayout.Button(text, _layoutOptions))
				{
					_callbackOnPress?.Invoke(text);
				}
				GUILayout.FlexibleSpace();
			}
			GUILayout.EndVertical();
		}

		private static Rect GetScreenSpaceBounds(Rect _normalisedBounds)
		{
			Rect result = default(Rect);
			result.xMin = _normalisedBounds.xMin * (float)Screen.width;
			result.xMax = _normalisedBounds.xMax * (float)Screen.width;
			result.yMin = _normalisedBounds.yMin * (float)Screen.height;
			result.yMax = _normalisedBounds.yMax * (float)Screen.height;
			return result;
		}
	}
}
