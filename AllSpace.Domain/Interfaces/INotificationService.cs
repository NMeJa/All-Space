using AllSpace.Domain.Models;

namespace AllSpace.Domain.Interfaces;

public interface INotificationService
{
	Task<IEnumerable<Notification>> GetAllAsync();
	Task<IEnumerable<Notification>> GetByWorkspaceAsync(string workspaceId);
	Task<Notification> CreateAsync(Notification notification);
	Task MarkAsReadAsync(string notificationId);
	Task ClearWorkspaceNotificationsAsync(string workspaceId);
	event EventHandler<NotificationEventArgs> NotificationReceived;
}

public class NotificationEventArgs : EventArgs
{
	public Notification Notification { get; set; }
}