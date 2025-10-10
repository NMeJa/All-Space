using System.Collections.Immutable;
using AllSpace.Domain.Models;

namespace AllSpace.Data.Services;

public static class MockDataGenerator
{
	private static readonly Random random = new();

	// Predefined colors for consistency
	private static class BrandColors
	{
		public const string Slack = "#4a154b";
		public const string Gmail = "#ea4335";
		public const string Outlook = "#0078d4";
		public const string Notion = "#000000";
		public const string Trello = "#0079bf";
		public const string Discord = "#5865f2";
		public const string Teams = "#6264a7";
		public const string WhatsApp = "#25d366";
		public const string Telegram = "#0088cc";
		public const string Spotify = "#1db954";
		public const string YouTube = "#ff0000";
		public const string GitHub = "#181717";
		public const string LinkedIn = "#0077b5";
		public const string Twitter = "#1da1f2";
		public const string Facebook = "#1877f2";
		public const string Instagram = "#e4405f";
		public const string Drive = "#4285f4";
		public const string Dropbox = "#0061ff";
		public const string OneDrive = "#0078d4";
		public const string Todoist = "#e44332";
		public const string Asana = "#f06a6a";
		public const string Monday = "#ff3d57";
		public const string Jira = "#0052cc";
		public const string Figma = "#f24e1e";
		public const string Miro = "#050038";
		public const string Calendar = "#1967d2";
		public const string Meet = "#00897b";
		public const string Zoom = "#2d8cff";
		public const string Skype = "#00aff0";
	}

	private static readonly (string Name, string Url, string Color)[] ServiceTemplates =
	{
		("Gmail", "https://mail.google.com", BrandColors.Gmail),
		("Slack", "https://slack.com", BrandColors.Slack),
		("Discord", "https://discord.com", BrandColors.Discord),
		("Teams", "https://teams.microsoft.com", BrandColors.Teams),
		("Notion", "https://notion.so", BrandColors.Notion),
		("Trello", "https://trello.com", BrandColors.Trello),
		("GitHub", "https://github.com", BrandColors.GitHub),
		("WhatsApp", "https://web.whatsapp.com", BrandColors.WhatsApp),
		("Telegram", "https://web.telegram.org", BrandColors.Telegram),
		("YouTube", "https://youtube.com", BrandColors.YouTube),
		("Spotify", "https://open.spotify.com", BrandColors.Spotify),
		("LinkedIn", "https://linkedin.com", BrandColors.LinkedIn),
		("Twitter", "https://twitter.com", BrandColors.Twitter),
		("Figma", "https://figma.com", BrandColors.Figma),
		("Dropbox", "https://dropbox.com", BrandColors.Dropbox),
		("Todoist", "https://todoist.com", BrandColors.Todoist),
		("Asana", "https://asana.com", BrandColors.Asana),
		("Jira", "https://jira.atlassian.com", BrandColors.Jira),
		("Calendar", "https://calendar.google.com", BrandColors.Calendar),
		("Drive", "https://drive.google.com", BrandColors.Drive),
	};

	// Generate random app
	public static AppInfo GenerateRandomApp()
	{
		var template = ServiceTemplates[random.Next(ServiceTemplates.Length)];
		var id = $"app-{Guid.NewGuid():N}";

		return new AppInfo(
						   id,
						   $"{template.Name} {random.Next(1, 100)}",
						   template.Url,
						   template.Color
						  );
	}

	// Generate multiple random apps
	public static ImmutableList<AppInfo> GenerateRandomApps(int count)
	{
		return Enumerable.Range(0, count)
						 .Select(_ => GenerateRandomApp())
						 .ToImmutableList();
	}

	// Generate random workspace (with 1-3 apps)
	public static Workspace GenerateRandomWorkspace()
	{
		var id = $"ws-{Guid.NewGuid():N}";
		var appCount = random.Next(1, 4); // 1 to 3 apps
		var apps = GenerateRandomApps(appCount);

		// Single app workspace uses app name, multi-app gets custom name
		var name = appCount == 1
					   ? apps[0].Name
					   : $"Workspace {random.Next(1, 1000)}";

		return new Workspace(id, name, apps);
	}

	// Generate multiple random workspaces
	public static List<Workspace> GenerateRandomWorkspaces(int count)
	{
		return Enumerable.Range(0, count)
						 .Select(_ => GenerateRandomWorkspace())
						 .ToList();
	}

	// Generate random folder with workspaces
	public static WorkspaceFolder GenerateRandomFolder()
	{
		var id = $"folder-{Guid.NewGuid():N}";
		var name = $"Folder {random.Next(1, 1000)}";
		var type = (WorkspaceFolder.FolderType)random.Next(0, 3);
		var workspaceCount = random.Next(2, 8); // 2 to 7 workspaces
		var workspaces = GenerateRandomWorkspaces(workspaceCount);

		return new WorkspaceFolder(id, name, type, workspaces.ToImmutableList(), ImmutableList<WorkspaceFolder>.Empty);
	}

	// Generate multiple folders with a specific count
	public static List<WorkspaceFolder> GenerateFolders(int count)
	{
		var folders = ImmutableList.CreateBuilder<WorkspaceFolder>();
		var predefinedFolders = GetMockFolders();

		for (int i = 0; i < count; i++)
		{
			if (i < predefinedFolders.Count)
			{
				folders.Add(predefinedFolders[i]);
			}
			else
			{
				folders.Add(GenerateRandomFolder());
			}
		}

		return folders.ToList();
	}

	// Get predefined mock workspaces
	public static List<Workspace> GetMockWorkspaces()
	{
		// Create individual apps
		var gmailApp = new AppInfo("gmail", "Gmail", "https://mail.google.com", BrandColors.Gmail);
		var slackApp = new AppInfo("slack", "Slack", "https://slack.com", BrandColors.Slack);
		var notionApp = new AppInfo("notion", "Notion", "https://notion.so", BrandColors.Notion);
		var githubApp = new AppInfo("github", "GitHub", "https://github.com", BrandColors.GitHub);
		var discordApp = new AppInfo("discord", "Discord", "https://discord.com", BrandColors.Discord);

		return new List<Workspace>
		{
			// Single app workspaces
			Workspace.CreateSingle("ws-gmail", gmailApp),
			Workspace.CreateSingle("ws-slack", slackApp),
			Workspace.CreateSingle("ws-notion", notionApp),
			Workspace.CreateSingle("ws-github", githubApp),
			Workspace.CreateSingle("ws-discord", discordApp),

			// Multi-app workspace (for side-by-side view)
			Workspace.CreateMulti("ws-productivity", "Productivity Suite",
								  null, null, notionApp, githubApp),

			Workspace.CreateMulti("ws-communication", "Communication Hub",
								  null, null, slackApp, discordApp)
		};
	}

	// Get predefined mock folders
	public static ImmutableList<WorkspaceFolder> GetMockFolders()
	{
		var workspaces = GetMockWorkspaces();

		// Create Google folder with Google workspaces
		var googleWorkspaces = new List<Workspace>
		{
			Workspace.CreateSingle("ws-gmail-1",
								   new AppInfo("gmail-1", "Gmail Personal", "https://mail.google.com", BrandColors.Gmail)),
			Workspace.CreateSingle("ws-gmail-2",
								   new AppInfo("gmail-2", "Gmail Work", "https://mail.google.com/mail/u/1", BrandColors.Gmail)),
			Workspace.CreateSingle("ws-calendar",
								   new AppInfo("calendar", "Calendar", "https://calendar.google.com", BrandColors.Calendar)),
			Workspace.CreateSingle("ws-drive",
								   new AppInfo("drive", "Drive", "https://drive.google.com", BrandColors.Drive))
		};

		// Create Communication folder
		var commWorkspaces = workspaces.Where(w =>
												  w.Name.Contains("Slack") || w.Name.Contains("Discord") ||
												  w.Name.Contains("Communication")).ToList();

		// Create Productivity folder
		var prodWorkspaces = workspaces.Where(w =>
												  w.Name.Contains("Notion") || w.Name.Contains("GitHub") ||
												  w.Name.Contains("Productivity")).ToList();

		return ImmutableList.Create(
									WorkspaceFolder.Create("folder-google", "Google",
														   WorkspaceFolder.FolderType.DROPDOWN_COLLAPSABLE,
														   null, null, googleWorkspaces.ToArray()),
									WorkspaceFolder.Create("folder-communication", "Communication",
														   WorkspaceFolder.FolderType.DROPDOWN,
														   null, null, commWorkspaces.ToArray()),
									WorkspaceFolder.Create("folder-productivity", "Productivity",
														   WorkspaceFolder.FolderType.SIDEBAR,
														   null, null, prodWorkspaces.ToArray())
								   );
	}

	// Get mock auth profiles
	public static ImmutableDictionary<string, AuthProfile> GetMockAuthProfiles()
	{
		var profiles = new[]
		{
			AuthProfile.Create(
							   "google-personal",
							   "Personal Google",
							   AuthProfile.Providers.Google,
							   new Dictionary<string, string>
							   {
								   ["email"] = "personal@gmail.com",
								   ["token"] = "encrypted_token_personal"
							   }
							  ),
			AuthProfile.Create(
							   "microsoft-main",
							   "Microsoft Account",
							   AuthProfile.Providers.Microsoft,
							   new Dictionary<string, string>
							   {
								   ["email"] = "user@outlook.com",
								   ["token"] = "encrypted_token_ms"
							   }
							  )
		};

		return profiles.ToImmutableDictionary(p => p.Id);
	}

	// Generate complete app state
	public static AppCollectionState GetMockAppState()
	{
		return new AppCollectionState(
									  Folders: GetMockFolders(),
									  Workspaces: GetMockWorkspaces().ToImmutableList(),
									  Profiles: GetMockAuthProfiles()
									 );
	}

	// Generate app state with specific counts
	public static AppCollectionState GenerateAppState(int folderCount, int workspaceCount)
	{
		return new AppCollectionState(Folders: GenerateFolders(folderCount).ToImmutableList(),
									  Workspaces: GenerateRandomWorkspaces(workspaceCount).ToImmutableList(),
									  Profiles: GetMockAuthProfiles()
									 );
	}
}