using BackendlessAPI.Async;
using BackendlessAPI.Engine;
using BackendlessAPI.Exception;
using BackendlessAPI.Property;
using System;
using System.Collections.Generic;

namespace BackendlessAPI.Service
{
	public class UserService
	{
		private BackendlessUser _currentUser;

		public BackendlessUser CurrentUser
		{
			get
			{
				return _currentUser;
			}
			set
			{
				_currentUser = value;
			}
		}

		static UserService()
		{
		}

		public BackendlessUser Register(BackendlessUser user)
		{
			CheckUserToBeProper(user, passwordCheck: true);
			user.PutProperties(Invoker.InvokeSync<Dictionary<string, object>>(Invoker.Api.USERSERVICE_REGISTER, new object[1]
			{
				user.Properties
			}));
			return user;
		}

		public void Register(BackendlessUser user, AsyncCallback<BackendlessUser> callback)
		{
			try
			{
				CheckUserToBeProper(user, passwordCheck: true);
				AsyncCallback<Dictionary<string, object>> callback2 = new AsyncCallback<Dictionary<string, object>>(delegate(Dictionary<string, object> r)
				{
					user.PutProperties(r);
					if (callback != null)
					{
						callback.ResponseHandler(user);
					}
				}, delegate(BackendlessFault f)
				{
					if (callback != null)
					{
						callback.ErrorHandler(f);
						return;
					}
					throw new BackendlessException(f);
				});
				Invoker.InvokeAsync(Invoker.Api.USERSERVICE_REGISTER, new object[1]
				{
					user.Properties
				}, callback2);
			}
			catch (System.Exception ex)
			{
				if (callback == null)
				{
					throw;
				}
				callback.ErrorHandler(new BackendlessFault(ex.Message));
			}
		}

		public BackendlessUser Update(BackendlessUser user)
		{
			CheckUserToBeProper(user, passwordCheck: false);
			if (string.IsNullOrEmpty(user.UserId))
			{
				throw new ArgumentNullException("User not logged in or wrong user id.");
			}
			user.PutProperties(Invoker.InvokeSync<Dictionary<string, object>>(Invoker.Api.USERSERVICE_UPDATE, new object[2]
			{
				user.Properties,
				user.UserId
			}));
			return user;
		}

		public void Update(BackendlessUser user, AsyncCallback<BackendlessUser> callback)
		{
			try
			{
				CheckUserToBeProper(user, passwordCheck: false);
				if (string.IsNullOrEmpty(user.UserId))
				{
					throw new ArgumentNullException("User not logged in or wrong user id.");
				}
				AsyncCallback<Dictionary<string, object>> callback2 = new AsyncCallback<Dictionary<string, object>>(delegate(Dictionary<string, object> r)
				{
					user.PutProperties(r);
					if (callback != null)
					{
						callback.ResponseHandler(user);
					}
				}, delegate(BackendlessFault f)
				{
					if (callback != null)
					{
						callback.ErrorHandler(f);
						return;
					}
					throw new BackendlessException(f);
				});
				Invoker.InvokeAsync(Invoker.Api.USERSERVICE_UPDATE, new object[2]
				{
					user.Properties,
					user.UserId
				}, callback2);
			}
			catch (System.Exception ex)
			{
				if (callback == null)
				{
					throw;
				}
				callback.ErrorHandler(new BackendlessFault(ex.Message));
			}
		}

		public BackendlessUser Login(string login, string password)
		{
			if (CurrentUser != null)
			{
				Logout();
			}
			if (string.IsNullOrEmpty(login))
			{
				throw new ArgumentNullException("User login cannot be null or empty.");
			}
			if (string.IsNullOrEmpty(password))
			{
				throw new ArgumentNullException("User password cannot be null or empty.");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("login", login);
			dictionary.Add("password", password);
			HandleUserLogin(Invoker.InvokeSync<Dictionary<string, object>>(Invoker.Api.USERSERVICE_LOGIN, new object[1]
			{
				dictionary
			}));
			return CurrentUser;
		}

		public void Login(string login, string password, AsyncCallback<BackendlessUser> callback)
		{
			try
			{
				if (string.IsNullOrEmpty(login))
				{
					throw new ArgumentNullException("User login cannot be null or empty.");
				}
				if (string.IsNullOrEmpty(password))
				{
					throw new ArgumentNullException("User password cannot be null or empty.");
				}
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("login", login);
				dictionary.Add("password", password);
				Invoker.InvokeAsync(Invoker.Api.USERSERVICE_LOGIN, new object[1]
				{
					dictionary
				}, GetUserLoginAsyncHandler(callback));
			}
			catch (System.Exception ex)
			{
				if (callback == null)
				{
					throw;
				}
				callback.ErrorHandler(new BackendlessFault(ex.Message));
			}
		}

		public void Logout()
		{
			try
			{
				Invoker.InvokeSync<object>(Invoker.Api.USERSERVICE_LOGOUT, null);
			}
			catch (BackendlessException ex)
			{
				BackendlessFault backendlessFault = ex.BackendlessFault;
				if (backendlessFault != null)
				{
					int num = int.Parse(backendlessFault.FaultCode);
					if (num != 3064 && num != 3091 && num != 3090 && num != 3023)
					{
						throw ex;
					}
				}
			}
			CurrentUser = null;
			HeadersManager.GetInstance().RemoveHeader(HeadersEnum.USER_TOKEN_KEY);
		}

		public void Logout(AsyncCallback<object> callback)
		{
			AsyncCallback<object> callback2 = new AsyncCallback<object>(delegate
			{
				CurrentUser = null;
				HeadersManager.GetInstance().RemoveHeader(HeadersEnum.USER_TOKEN_KEY);
				if (callback != null)
				{
					callback.ResponseHandler(null);
				}
			}, delegate(BackendlessFault f)
			{
				if (callback != null)
				{
					callback.ErrorHandler(f);
					return;
				}
				throw new BackendlessException(f);
			});
			Invoker.InvokeAsync(Invoker.Api.USERSERVICE_LOGOUT, null, callback2);
		}

		public void RestorePassword(string identity)
		{
			if (string.IsNullOrEmpty(identity))
			{
				throw new ArgumentNullException("Identity cannot be null or empty.");
			}
			Invoker.InvokeSync<object>(Invoker.Api.USERSERVICE_RESTOREPASSWORD, new object[2]
			{
				null,
				identity
			});
		}

		public void RestorePassword(string identity, AsyncCallback<object> callback)
		{
			try
			{
				if (string.IsNullOrEmpty(identity))
				{
					throw new ArgumentNullException("Identity cannot be null or empty.");
				}
				Invoker.InvokeAsync(Invoker.Api.USERSERVICE_RESTOREPASSWORD, new object[2]
				{
					null,
					identity
				}, callback);
			}
			catch (System.Exception ex)
			{
				if (callback == null)
				{
					throw;
				}
				callback.ErrorHandler(new BackendlessFault(ex.Message));
			}
		}

		public IList<string> GetUserRoles()
		{
			return Invoker.InvokeSync<IList<string>>(Invoker.Api.USERSERVICE_GETUSERROLES, null);
		}

		public void GetUserRoles(AsyncCallback<IList<string>> callback)
		{
			try
			{
				Invoker.InvokeAsync(Invoker.Api.USERSERVICE_GETUSERROLES, null, callback);
			}
			catch (System.Exception ex)
			{
				if (callback == null)
				{
					throw;
				}
				callback.ErrorHandler(new BackendlessFault(ex.Message));
			}
		}

		public List<UserProperty> DescribeUserClass()
		{
			return Invoker.InvokeSync<List<UserProperty>>(Invoker.Api.USERSERVICE_DESCRIBEUSERCLASS, null);
		}

		public void DescribeUserClass(AsyncCallback<List<UserProperty>> callback)
		{
			Invoker.InvokeAsync(Invoker.Api.USERSERVICE_DESCRIBEUSERCLASS, null, callback);
		}

		private static void CheckUserToBeProper(BackendlessUser user, bool passwordCheck)
		{
			if (user == null)
			{
				throw new ArgumentNullException("User cannot be null or empty.");
			}
			if (passwordCheck && string.IsNullOrEmpty(user.Password))
			{
				throw new ArgumentNullException("User password cannot be null or empty.");
			}
		}

		private void HandleUserLogin(Dictionary<string, object> invokeResult)
		{
			HeadersManager.GetInstance().AddHeader(HeadersEnum.USER_TOKEN_KEY, invokeResult[HeadersEnum.USER_TOKEN_KEY.Header].ToString());
			if (CurrentUser == null)
			{
				CurrentUser = new BackendlessUser();
			}
			CurrentUser.PutProperties(invokeResult);
		}

		private AsyncCallback<Dictionary<string, object>> GetUserLoginAsyncHandler(AsyncCallback<BackendlessUser> callback)
		{
			return new AsyncCallback<Dictionary<string, object>>(delegate(Dictionary<string, object> r)
			{
				HeadersManager.GetInstance().AddHeader(HeadersEnum.USER_TOKEN_KEY, r[HeadersEnum.USER_TOKEN_KEY.Header].ToString());
				if (CurrentUser == null)
				{
					CurrentUser = new BackendlessUser();
				}
				CurrentUser.PutProperties(r);
				if (callback != null)
				{
					callback.ResponseHandler(CurrentUser);
				}
			}, delegate(BackendlessFault f)
			{
				if (callback != null)
				{
					callback.ErrorHandler(f);
					return;
				}
				throw new BackendlessException(f);
			});
		}
	}
}
