using System;

namespace BackendlessAPI.Exception
{
	public class BackendlessFault
	{
		private readonly string _faultCode;

		private readonly string _message;

		private readonly string _detail;

		public string Detail => _detail;

		public string FaultCode => _faultCode;

		public string Message => _message;

		internal BackendlessFault(string message)
		{
			_message = message;
		}

		internal BackendlessFault(BackendlessException backendlessException)
		{
			_faultCode = backendlessException.FaultCode;
			_message = backendlessException.Message;
			_detail = backendlessException.Detail;
		}

		internal BackendlessFault(string faultCode, string message, string detail)
		{
			_faultCode = faultCode;
			_message = message;
			_detail = detail;
		}

		internal BackendlessFault(System.Exception ex)
		{
			_faultCode = ex.GetType().Name;
			_message = ex.Message;
			_detail = ex.StackTrace;
		}

		public override string ToString()
		{
			return string.Format("Backendless BackendlessFault. Code: {0}, Message: {1}", FaultCode ?? "N/A", Message ?? "N/A");
		}
	}
}
