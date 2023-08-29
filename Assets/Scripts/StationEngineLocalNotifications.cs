using UnityEngine;
using UTNotifications;

public class StationEngineLocalNotifications : MonoBehaviour
{
	private StationEngine stationEngine;

	private bool isDebugEnabled;

	private StationEngine.ComponentStatus actualStatus;

	public void Initialize(StationEngine stationEngine, bool isDebugEnabled)
	{
		actualStatus = StationEngine.ComponentStatus.INITIALIZING;
		this.stationEngine = stationEngine;
		this.isDebugEnabled = isDebugEnabled;
		if (this.isDebugEnabled)
		{
			stationEngine.PostDebugInfo("LOCAL NOTIFICATIONS - Initializing");
		}
		if (Application.platform != RuntimePlatform.WindowsEditor)
		{
			Manager.Instance.Initialize(willHandleReceivedNotifications: false);
			if (Manager.Instance.NotificationsEnabled())
			{
				actualStatus = StationEngine.ComponentStatus.INITIALIZED;
			}
			else
			{
				actualStatus = StationEngine.ComponentStatus.ERROR;
			}
		}
		else
		{
			actualStatus = StationEngine.ComponentStatus.ERROR;
		}
	}

	public StationEngine.ComponentStatus GetStatus()
	{
		return actualStatus;
	}

	public void SetStatusTimeOut()
	{
		actualStatus = StationEngine.ComponentStatus.TIME_OUT;
	}

	public void SetNotification(int timeInSeconds, string title, string body, int id)
	{
		if (actualStatus == StationEngine.ComponentStatus.INITIALIZED)
		{
			if (isDebugEnabled)
			{
				stationEngine.PostDebugInfo("LOCAL NOTIFICATIONS - Setting notification");
			}
			Manager.Instance.ScheduleNotification(timeInSeconds, title, body, id);
		}
		else
		{
			stationEngine.PostDebugError("LOCAL NOTIFICATIONS - Trying to set notification but not initialized");
		}
	}

	public void SetRepeatNotification(int timeInSeconds, int timeRepeatInSeconds, string title, string body, int id)
	{
		if (actualStatus == StationEngine.ComponentStatus.INITIALIZED)
		{
			if (isDebugEnabled)
			{
				stationEngine.PostDebugInfo("LOCAL NOTIFICATIONS - Setting repeat notification");
			}
			Manager.Instance.ScheduleNotificationRepeating(timeInSeconds, timeRepeatInSeconds, title, body, id);
		}
		else
		{
			stationEngine.PostDebugError("LOCAL NOTIFICATIONS - Trying to set repeat notification but not initialized");
		}
	}

	public void CancelNotification(int id)
	{
		if (actualStatus == StationEngine.ComponentStatus.INITIALIZED)
		{
			if (isDebugEnabled)
			{
				stationEngine.PostDebugInfo("LOCAL NOTIFICATIONS - Cancelled notification");
			}
			Manager.Instance.CancelNotification(id);
		}
		else
		{
			stationEngine.PostDebugError("LOCAL NOTIFICATIONS - Trying to cancel notification but not initialized");
		}
	}

	public void CancelAllNotifications()
	{
		if (actualStatus == StationEngine.ComponentStatus.INITIALIZED)
		{
			if (isDebugEnabled)
			{
				stationEngine.PostDebugInfo("LOCAL NOTIFICATIONS - Cancelled all notifications");
			}
			Manager.Instance.CancelAllNotifications();
		}
		else
		{
			stationEngine.PostDebugError("LOCAL NOTIFICATIONS - Trying to cancel all notifications but not initialized");
		}
	}
}
