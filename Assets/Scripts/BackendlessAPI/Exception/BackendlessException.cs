using System;

namespace BackendlessAPI.Exception
{
	public class BackendlessException : System.Exception
	{
		private readonly BackendlessFault _backendlessFault;

		public BackendlessFault BackendlessFault => _backendlessFault;

		public string FaultCode => _backendlessFault.FaultCode;

		public override string Message => _backendlessFault.Message;

		public string Detail => _backendlessFault.Detail;

		public BackendlessException(BackendlessFault backendlessFault)
		{
			_backendlessFault = backendlessFault;
		}

		public BackendlessException(string message)
		{
			_backendlessFault = new BackendlessFault(message);
		}

		public override string ToString()
		{
			return string.Format("Error code: {0}, Message: {1}", _backendlessFault.FaultCode ?? "N/A", Message ?? "N/A");
		}
	}
}
