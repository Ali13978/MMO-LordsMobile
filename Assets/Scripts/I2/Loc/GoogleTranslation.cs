using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace I2.Loc
{
	public static class GoogleTranslation
	{
		private static List<WWW> mCurrentTranslations = new List<WWW>();

		public static bool CanTranslate()
		{
			return LocalizationManager.Sources.Count > 0 && !string.IsNullOrEmpty(LocalizationManager.GetWebServiceURL());
		}

		public static void Translate(string text, string LanguageCodeFrom, string LanguageCodeTo, Action<string, string> OnTranslationReady)
		{
			LocalizationManager.InitializeIfNeeded();
			if (!CanTranslate())
			{
				OnTranslationReady(null, "WebService is not set correctly or needs to be reinstalled");
				return;
			}
			if (LanguageCodeTo == LanguageCodeFrom)
			{
				OnTranslationReady(text, null);
				return;
			}
			Dictionary<string, TranslationQuery> queries = new Dictionary<string, TranslationQuery>();
			if (string.IsNullOrEmpty(LanguageCodeTo))
			{
				OnTranslationReady(string.Empty, null);
				return;
			}
			CreateQueries(text, LanguageCodeFrom, LanguageCodeTo, queries);
			Translate(queries, delegate(Dictionary<string, TranslationQuery> results, string error)
			{
				if (!string.IsNullOrEmpty(error) || results.Count == 0)
				{
					OnTranslationReady(null, error);
				}
				else
				{
					string arg = RebuildTranslation(text, queries, LanguageCodeTo);
					OnTranslationReady(arg, null);
				}
			});
		}

		public static string ForceTranslate(string text, string LanguageCodeFrom, string LanguageCodeTo)
		{
			Dictionary<string, TranslationQuery> dictionary = new Dictionary<string, TranslationQuery>();
			AddQuery(text, LanguageCodeFrom, LanguageCodeTo, dictionary);
			WWW translationWWW = GetTranslationWWW(dictionary);
			while (!translationWWW.isDone)
			{
			}
			if (!string.IsNullOrEmpty(translationWWW.error))
			{
				return string.Empty;
			}
			byte[] bytes = translationWWW.bytes;
			string @string = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
			string value = ParseTranslationResult(@string, dictionary);
			if (!string.IsNullOrEmpty(value))
			{
				return null;
			}
			return GetQueryResult(text, LanguageCodeTo, dictionary);
		}

		public static void CreateQueries(string text, string LanguageCodeFrom, string LanguageCodeTo, Dictionary<string, TranslationQuery> dict)
		{
			if (!text.Contains("[i2p_"))
			{
				AddQuery(text, LanguageCodeFrom, LanguageCodeTo, dict);
				return;
			}
			int num = 0;
			int num2 = text.IndexOf("[i2p_");
			if (num2 == 0)
			{
				num = text.IndexOf("]", num2) + 1;
				num2 = text.IndexOf("[i2p_");
				if (num2 < 0)
				{
					num2 = text.Length;
				}
			}
			string text2 = text.Substring(num, num2 - num);
			Regex regex = new Regex("{\\[(.*?)\\]}");
			for (ePluralType ePluralType = ePluralType.Zero; ePluralType <= ePluralType.Plural; ePluralType++)
			{
				if (GoogleLanguages.LanguageHasPluralType(LanguageCodeTo, ePluralType.ToString()))
				{
					string input = text2;
					input = regex.Replace(input, GoogleLanguages.GetPluralTestNumber(LanguageCodeTo, ePluralType).ToString());
					AddQuery(input, LanguageCodeFrom, LanguageCodeTo, dict);
				}
			}
		}

		public static void AddQuery(string text, string LanguageCodeFrom, string LanguageCodeTo, Dictionary<string, TranslationQuery> dict)
		{
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			if (!dict.ContainsKey(text))
			{
				TranslationQuery query = default(TranslationQuery);
				TranslationQuery translationQuery = query;
				translationQuery.OrigText = text;
				translationQuery.LanguageCode = LanguageCodeFrom;
				translationQuery.TargetLanguagesCode = new string[1]
				{
					LanguageCodeTo
				};
				query = translationQuery;
				query.Text = text;
				ParseNonTranslatableElements(ref query);
				dict[text] = query;
			}
			else
			{
				TranslationQuery value = dict[text];
				if (Array.IndexOf(value.TargetLanguagesCode, LanguageCodeTo) < 0)
				{
					value.TargetLanguagesCode = value.TargetLanguagesCode.Concat(new string[1]
					{
						LanguageCodeTo
					}).Distinct().ToArray();
				}
				dict[text] = value;
			}
		}

		private static int FindClosingTag(string tag, MatchCollection matches, int startIndex)
		{
			int i = startIndex;
			for (int count = matches.Count; i < count; i++)
			{
				string captureMatch = GetCaptureMatch(matches[i]);
				if (captureMatch[0] == '/' && tag.StartsWith(captureMatch.Substring(1)))
				{
					return i;
				}
			}
			return -1;
		}

		private static string GetCaptureMatch(Match match)
		{
			for (int num = match.Groups.Count - 1; num >= 0; num--)
			{
				if (match.Groups[num].Success)
				{
					return match.Groups[num].ToString();
				}
			}
			return match.ToString();
		}

		private static void ParseNonTranslatableElements(ref TranslationQuery query)
		{
			MatchCollection matchCollection = Regex.Matches(query.Text, "\\{\\[(.*?)]}|\\[(.*?)]|\\<(.*?)>");
			if (matchCollection == null || matchCollection.Count == 0)
			{
				return;
			}
			string text = query.Text;
			List<string> list = new List<string>();
			int i = 0;
			for (int count = matchCollection.Count; i < count; i++)
			{
				string captureMatch = GetCaptureMatch(matchCollection[i]);
				int num = FindClosingTag(captureMatch, matchCollection, i);
				if (num < 0)
				{
					string text2 = matchCollection[i].ToString();
					if (text2.StartsWith("{[") && text2.EndsWith("]}"))
					{
						text = text.Replace(text2, ((char)(ushort)(9728 + list.Count)).ToString());
						list.Add(text2);
					}
				}
				else if (captureMatch == "i2nt")
				{
					string text3 = query.Text.Substring(matchCollection[i].Index, matchCollection[num].Index - matchCollection[i].Index + matchCollection[num].Length);
					text = text.Replace(text3, ((char)(ushort)(9728 + list.Count)).ToString());
					list.Add(text3);
				}
				else
				{
					string text4 = matchCollection[i].ToString();
					text = text.Replace(text4, ((char)(ushort)(9728 + list.Count)).ToString());
					list.Add(text4);
					string text5 = matchCollection[num].ToString();
					text = text.Replace(text5, ((char)(ushort)(9728 + list.Count)).ToString());
					list.Add(text5);
				}
			}
			query.Text = text;
			query.Tags = list.ToArray();
		}

		public static string GetQueryResult(string text, string LanguageCodeTo, Dictionary<string, TranslationQuery> dict)
		{
			if (!dict.ContainsKey(text))
			{
				return null;
			}
			TranslationQuery translationQuery = dict[text];
			if (translationQuery.Results == null || translationQuery.Results.Length < 0)
			{
				return null;
			}
			if (string.IsNullOrEmpty(LanguageCodeTo))
			{
				return translationQuery.Results[0];
			}
			int num = Array.IndexOf(translationQuery.TargetLanguagesCode, LanguageCodeTo);
			if (num < 0)
			{
				return null;
			}
			return translationQuery.Results[num];
		}

		public static string RebuildTranslation(string text, Dictionary<string, TranslationQuery> dict, string LanguageCodeTo)
		{
			if (!text.Contains("[i2p_"))
			{
				return GetTranslation(text, LanguageCodeTo, dict);
			}
			int num = 0;
			int num2 = text.IndexOf("[i2p_");
			if (num2 == 0)
			{
				num = text.IndexOf("]", num2) + 1;
				num2 = text.IndexOf("[i2p_");
				if (num2 < 0)
				{
					num2 = text.Length;
				}
			}
			string text2 = text.Substring(num, num2 - num);
			Match match = Regex.Match(text2, "{\\[(.*?)\\]}");
			string text3 = (match != null) ? match.Value : string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			string text4 = text2;
			int pluralTestNumber = GoogleLanguages.GetPluralTestNumber(LanguageCodeTo, ePluralType.Plural);
			text4 = text4.Replace(text3, pluralTestNumber.ToString());
			string translation = GetTranslation(text4, LanguageCodeTo, dict);
			string text5 = translation.Replace(pluralTestNumber.ToString(), text3);
			stringBuilder.Append(text5);
			for (ePluralType ePluralType = ePluralType.Zero; ePluralType < ePluralType.Plural; ePluralType++)
			{
				if (GoogleLanguages.LanguageHasPluralType(LanguageCodeTo, ePluralType.ToString()))
				{
					text4 = text2;
					pluralTestNumber = GoogleLanguages.GetPluralTestNumber(LanguageCodeTo, ePluralType);
					text4 = text4.Replace(text3, pluralTestNumber.ToString());
					translation = GetTranslation(text4, LanguageCodeTo, dict);
					translation = translation.Replace(pluralTestNumber.ToString(), text3);
					if (!string.IsNullOrEmpty(translation) && translation != text5)
					{
						stringBuilder.Append("[i2p_");
						stringBuilder.Append(ePluralType.ToString());
						stringBuilder.Append(']');
						stringBuilder.Append(translation);
					}
				}
			}
			return stringBuilder.ToString();
		}

		private static string GetTranslation(string text, string LanguageCodeTo, Dictionary<string, TranslationQuery> dict)
		{
			if (!dict.ContainsKey(text))
			{
				return null;
			}
			TranslationQuery translationQuery = dict[text];
			int num = Array.IndexOf(translationQuery.TargetLanguagesCode, LanguageCodeTo);
			if (num < 0)
			{
				return string.Empty;
			}
			if (translationQuery.Results == null)
			{
				return string.Empty;
			}
			return translationQuery.Results[num];
		}

		public static string UppercaseFirst(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return string.Empty;
			}
			char[] array = s.ToLower().ToCharArray();
			array[0] = char.ToUpper(array[0]);
			return new string(array);
		}

		public static string TitleCase(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return string.Empty;
			}
			return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s);
		}

		public static void Translate(Dictionary<string, TranslationQuery> requests, Action<Dictionary<string, TranslationQuery>, string> OnTranslationReady, bool usePOST = true)
		{
			WWW translationWWW = GetTranslationWWW(requests, usePOST);
			CoroutineManager.Start(WaitForTranslation(translationWWW, OnTranslationReady, requests));
		}

		public static bool ForceTranslate(Dictionary<string, TranslationQuery> requests, bool usePOST = true)
		{
			WWW translationWWW = GetTranslationWWW(requests, usePOST);
			while (!translationWWW.isDone)
			{
			}
			string error = translationWWW.error;
			if (!string.IsNullOrEmpty(error))
			{
				if (error.Contains("rewind"))
				{
					return ForceTranslate(requests, usePOST: false);
				}
				return false;
			}
			byte[] bytes = translationWWW.bytes;
			string @string = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
			string value = ParseTranslationResult(@string, requests);
			return string.IsNullOrEmpty(value);
		}

		public static WWW GetTranslationWWW(Dictionary<string, TranslationQuery> requests, bool usePOST = true)
		{
			usePOST = false;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, TranslationQuery> request in requests)
			{
				TranslationQuery value = request.Value;
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append("<I2Loc>");
				}
				stringBuilder.Append(GoogleLanguages.GetGoogleLanguageCode(value.LanguageCode));
				stringBuilder.Append(":");
				for (int i = 0; i < value.TargetLanguagesCode.Length; i++)
				{
					if (i != 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(GoogleLanguages.GetGoogleLanguageCode(value.TargetLanguagesCode[i]));
				}
				stringBuilder.Append("=");
				string text = (!(TitleCase(value.Text) == value.Text)) ? value.Text : value.Text.ToLowerInvariant();
				if (usePOST)
				{
					stringBuilder.Append(text);
				}
				else
				{
					stringBuilder.Append(Uri.EscapeDataString(text));
					if (stringBuilder.Length > 4000)
					{
						break;
					}
				}
			}
			if (usePOST)
			{
				WWWForm wWWForm = new WWWForm();
				wWWForm.AddField("action", "Translate");
				wWWForm.AddField("list", stringBuilder.ToString());
				return new WWW(LocalizationManager.GetWebServiceURL(), wWWForm);
			}
			string url = $"{LocalizationManager.GetWebServiceURL()}?action=Translate&list={stringBuilder.ToString()}";
			return new WWW(url);
		}

		private static IEnumerator WaitForTranslation(WWW www, Action<Dictionary<string, TranslationQuery>, string> OnTranslationReady, Dictionary<string, TranslationQuery> requests)
		{
			mCurrentTranslations.Add(www);
			while (!www.isDone)
			{
				yield return null;
			}
			int numWWW = mCurrentTranslations.Count;
			mCurrentTranslations.Remove(www);
			if (numWWW == mCurrentTranslations.Count)
			{
				yield break;
			}
			string errorMsg2 = www.error;
			if (!string.IsNullOrEmpty(errorMsg2))
			{
				if (errorMsg2.Contains("rewind"))
				{
					Translate(requests, OnTranslationReady, usePOST: false);
				}
				else
				{
					OnTranslationReady(requests, www.error);
				}
			}
			else
			{
				byte[] bytes = www.bytes;
				string wwwText = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
				errorMsg2 = ParseTranslationResult(wwwText, requests);
				OnTranslationReady(requests, errorMsg2);
			}
		}

		public static string ParseTranslationResult(string html, Dictionary<string, TranslationQuery> requests)
		{
			if (html.StartsWith("<!DOCTYPE html>") || html.StartsWith("<HTML>"))
			{
				if (html.Contains("The script completed but did not return anything"))
				{
					return "The current Google WebService is not supported.\nPlease, delete the WebService from the Google Drive and Install the latest version.";
				}
				if (html.Contains("Service invoked too many times in a short time"))
				{
					return string.Empty;
				}
				return "There was a problem contacting the WebService. Please try again later\n" + html;
			}
			string[] array = html.Split(new string[1]
			{
				"<I2Loc>"
			}, StringSplitOptions.None);
			string[] separator = new string[1]
			{
				"<i2>"
			};
			int num = 0;
			string[] array2 = requests.Keys.ToArray();
			string[] array3 = array2;
			foreach (string text in array3)
			{
				TranslationQuery value = FindQueryFromOrigText(text, requests);
				string text2 = array[num++];
				if (value.Tags != null)
				{
					int j = 0;
					for (int num3 = value.Tags.Length; j < num3; j++)
					{
						text2 = text2.Replace(((char)(ushort)(9728 + j)).ToString(), value.Tags[j]);
					}
				}
				value.Results = text2.Split(separator, StringSplitOptions.None);
				if (TitleCase(text) == text)
				{
					for (int k = 0; k < value.Results.Length; k++)
					{
						value.Results[k] = TitleCase(value.Results[k]);
					}
				}
				requests[value.OrigText] = value;
			}
			return null;
		}

		private static TranslationQuery FindQueryFromOrigText(string origText, Dictionary<string, TranslationQuery> dict)
		{
			foreach (KeyValuePair<string, TranslationQuery> item in dict)
			{
				TranslationQuery value = item.Value;
				if (value.OrigText == origText)
				{
					return item.Value;
				}
			}
			return default(TranslationQuery);
		}

		public static bool IsTranslating()
		{
			return mCurrentTranslations.Count > 0;
		}

		public static void CancelCurrentGoogleTranslations()
		{
			mCurrentTranslations.Clear();
		}
	}
}
