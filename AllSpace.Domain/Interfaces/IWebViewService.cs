namespace AllSpace.Domain.Interfaces;

public interface IWebViewService
{
	Task<string> CreateWebViewAsync(string workspaceId, string url);
	Task NavigateAsync(string workspaceId, string url);
	Task<string> ExecuteScriptAsync(string workspaceId, string script);
	Task InjectScriptAsync(string workspaceId, string script);
	Task SetUserDataFolderAsync(string workspaceId, string folderPath);
	Task ClearCacheAsync(string workspaceId);
	event EventHandler<WebViewEventArgs> NavigationCompleted;
	event EventHandler<WebViewEventArgs> DomContentLoaded;
}

public class WebViewEventArgs : EventArgs
{
	public string WorkspaceId { get; set; }
	public string Url { get; set; }
	public bool IsSuccess { get; set; }
}