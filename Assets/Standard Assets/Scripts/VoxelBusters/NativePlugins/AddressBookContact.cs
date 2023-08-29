using System;
using UnityEngine;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins
{
	[Serializable]
	public class AddressBookContact
	{
		private static Texture2D defaultImage;

		[SerializeField]
		private string m_firstName;

		[SerializeField]
		private string m_lastName;

		[SerializeField]
		private Texture2D m_image;

		private string m_imageDownloadError;

		[SerializeField]
		private string[] m_phoneNumberList;

		[SerializeField]
		private string[] m_emailIDList;

		private DownloadTexture m_downloadRequest;

		public string FirstName
		{
			get
			{
				return m_firstName;
			}
			protected set
			{
				m_firstName = value;
			}
		}

		public string LastName
		{
			get
			{
				return m_lastName;
			}
			protected set
			{
				m_lastName = value;
			}
		}

		protected string ImagePath
		{
			get;
			set;
		}

		public string[] PhoneNumberList
		{
			get
			{
				return m_phoneNumberList;
			}
			protected set
			{
				m_phoneNumberList = value;
			}
		}

		public string[] EmailIDList
		{
			get
			{
				return m_emailIDList;
			}
			protected set
			{
				m_emailIDList = value;
			}
		}

		protected AddressBookContact()
		{
			FirstName = null;
			LastName = null;
			ImagePath = null;
			m_image = null;
			m_imageDownloadError = null;
			PhoneNumberList = new string[0];
			EmailIDList = new string[0];
		}

		protected AddressBookContact(AddressBookContact _source)
		{
			FirstName = _source.FirstName;
			LastName = _source.LastName;
			ImagePath = _source.ImagePath;
			m_image = _source.m_image;
			m_imageDownloadError = _source.m_imageDownloadError;
			PhoneNumberList = _source.PhoneNumberList;
			EmailIDList = _source.EmailIDList;
		}

		public static Texture2D GetDefaultImage()
		{
			if (defaultImage == null)
			{
				defaultImage = (Resources.Load("Default/ContactImage") as Texture2D);
			}
			return defaultImage;
		}

		public void GetImageAsync(DownloadTexture.Completion _onCompletion)
		{
			if (m_image != null || m_imageDownloadError != null)
			{
				_onCompletion(m_image, m_imageDownloadError);
			}
			else if (string.IsNullOrEmpty(ImagePath))
			{
				_onCompletion(GetDefaultImage(), null);
			}
			else if (m_downloadRequest == null)
			{
				URL uRL = URL.FileURLWithPath(ImagePath);
				m_downloadRequest = new DownloadTexture(uRL, _isAsynchronous: true, _autoFixOrientation: true);
				m_downloadRequest.OnCompletion = delegate(Texture2D _newTexture, string _error)
				{
					m_image = _newTexture;
					m_imageDownloadError = _error;
					m_downloadRequest = null;
					if (_onCompletion != null)
					{
						_onCompletion(_newTexture, _error);
					}
				};
				m_downloadRequest.StartRequest();
			}
		}

		public override string ToString()
		{
			return $"[AddressBookContact: FirstName={FirstName}, LastName={LastName}]";
		}
	}
}
