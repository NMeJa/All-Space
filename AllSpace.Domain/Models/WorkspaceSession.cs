namespace AllSpace.Domain.Models;

public record WorkspaceSession(
	string Id,
	string WorkspaceId,
	string UserDataPath,
	DateTime StartedAt,
	WorkspaceSession.SessionState State = WorkspaceSession.SessionState.Active)
{
	public enum SessionState
	{
		Active,
		Suspended,
		Terminated
	}

	public WorkspaceSession Suspend() => this with { State = SessionState.Suspended };

	public WorkspaceSession Terminate() => this with { State = SessionState.Terminated };
}