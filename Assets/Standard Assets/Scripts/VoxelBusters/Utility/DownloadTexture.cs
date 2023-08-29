using ExifLibrary;
using System.IO;
using UnityEngine;

namespace VoxelBusters.Utility
{
	public class DownloadTexture : Request
	{
		public delegate void Completion(Texture2D _texture, string _error);

		public bool AutoFixOrientation
		{
			get;
			set;
		}

		public float ScaleFactor
		{
			get;
			set;
		}

		public Completion OnCompletion
		{
			get;
			set;
		}

		public DownloadTexture(URL _URL, bool _isAsynchronous, bool _autoFixOrientation)
			: base(_URL, _isAsynchronous)
		{
			AutoFixOrientation = _autoFixOrientation;
			base.WWWObject = new WWW(_URL.URLString);
			ScaleFactor = 1f;
		}

		protected override void DidFailStartRequestWithError(string _error)
		{
			if (OnCompletion != null)
			{
				OnCompletion(null, _error);
			}
		}

		protected override void OnFetchingResponse()
		{
			Texture2D texture = null;
			if (!string.IsNullOrEmpty(base.WWWObject.error))
			{
				UnityEngine.Debug.Log("[DownloadTexture] Failed to download texture. Error = " + base.WWWObject.error + ".");
				if (OnCompletion != null)
				{
					OnCompletion(null, base.WWWObject.error);
					return;
				}
			}
			Texture2D texture2 = base.WWWObject.texture;
			if (AutoFixOrientation)
			{
				Stream fileStream = new MemoryStream(base.WWWObject.bytes);
				ExifFile exifFile = ExifFile.Read(fileStream);
				texture2 = texture2.Scale(ScaleFactor);
				if (exifFile != null && exifFile.Properties.ContainsKey(ExifTag.Orientation))
				{
					Orientation orientation = (Orientation)(ushort)exifFile.Properties[ExifTag.Orientation].Value;
					UnityEngine.Debug.Log("[DownloadTexture] Orientation=" + orientation);
					switch (orientation)
					{
					case Orientation.Normal:
						texture = texture2;
						break;
					case Orientation.MirroredVertically:
						texture = texture2.MirrorTexture(_mirrorHorizontally: true, _mirrorVertically: false);
						break;
					case Orientation.Rotated180:
						texture = texture2.MirrorTexture(_mirrorHorizontally: true, _mirrorVertically: true);
						break;
					case Orientation.MirroredHorizontally:
						texture = texture2.MirrorTexture(_mirrorHorizontally: false, _mirrorVertically: true);
						break;
					case Orientation.RotatedLeftAndMirroredVertically:
						texture = texture2.MirrorTexture(_mirrorHorizontally: true, _mirrorVertically: false).Rotate(-90f);
						break;
					case Orientation.RotatedRight:
						texture = texture2.Rotate(90f);
						break;
					case Orientation.RotatedLeft:
						texture = texture2.MirrorTexture(_mirrorHorizontally: false, _mirrorVertically: true).Rotate(-90f);
						break;
					case Orientation.RotatedRightAndMirroredVertically:
						texture = texture2.Rotate(-90f);
						break;
					}
				}
				else
				{
					texture = texture2;
				}
			}
			else
			{
				texture2 = texture2.Scale(ScaleFactor);
				texture = texture2;
			}
			if (OnCompletion != null)
			{
				OnCompletion(texture, null);
			}
		}
	}
}
