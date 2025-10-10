using System.Collections.Immutable;
using AllSpace.Domain.Models;

namespace AllSpace.Data.Services;

public static class MockDataGenerator
{
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

	public static ImmutableList<WorkspaceFolder> GetMockFolders()
	{
		return ImmutableList.Create(
									CreateProductivityFolder(),
									CreateGoogleFolder(),
									CreateMicrosoftFolder(),
									CreateCommunicationFolder(),
									CreateDevelopmentFolder(),
									CreateSocialFolder(),
									CreateDesignFolder()
								   );
	}

	public static ImmutableList<WorkspaceInfo> GetMockWorkspaces()
	{
		return ImmutableList.Create(
									new WorkspaceInfo(
													  Id: "ws-gmail-personal",
													  Name: "Gmail Personal",
													  Url: "https://mail.google.com",
													  BackgroundColor: BrandColors.Gmail,
													  Icon: "📧",
													  ProfileId: "google-personal"
													 ),
									new WorkspaceInfo(
													  Id: "ws-gmail-work",
													  Name: "Gmail Work",
													  Url: "https://mail.google.com/mail/u/1",
													  BackgroundColor: BrandColors.Gmail,
													  Icon: "💼",
													  ProfileId: "google-work"
													 ),
									new WorkspaceInfo(
													  Id: "ws-slack-team",
													  Name: "Slack Team",
													  Url: "https://app.slack.com",
													  BackgroundColor: BrandColors.Slack,
													  Icon: "💬",
													  ProfileId: "slack-team"
													 ),
									new WorkspaceInfo(
													  Id: "ws-notion",
													  Name: "Notion",
													  Url: "https://notion.so",
													  BackgroundColor: BrandColors.Notion,
													  Icon: "📝"
													 ),
									new WorkspaceInfo(
													  Id: "ws-discord",
													  Name: "Discord",
													  Url: "https://discord.com/app",
													  BackgroundColor: BrandColors.Discord,
													  Icon: "🎮"
													 ),
									new WorkspaceInfo(
													  Id: "ws-whatsapp",
													  Name: "WhatsApp",
													  Url: "https://web.whatsapp.com",
													  BackgroundColor: BrandColors.WhatsApp,
													  Icon: "💚"
													 ),
									new WorkspaceInfo(
													  Id: "ws-github",
													  Name: "GitHub",
													  Url: "https://github.com",
													  BackgroundColor: BrandColors.GitHub,
													  Icon: "🐙",
													  ProfileId: "github-main"
													 ),
									new WorkspaceInfo(
													  Id: "ws-youtube",
													  Name: "YouTube",
													  Url: "https://youtube.com",
													  BackgroundColor: BrandColors.YouTube,
													  Icon: "📺",
													  ProfileId: "google-personal"
													 )
								   );
	}

	public static ImmutableList<WorkspaceGroup> GetMockGroups()
	{
		return ImmutableList.Create(
									new WorkspaceGroup(
													   Id: "group-work",
													   Name: "Work",
													   Color: "#6366f1",
													   WorkspaceIds: ImmutableHashSet.Create("ws-gmail-work", "ws-slack-team", "ws-notion"),
													   Order: 1
													  ),
									new WorkspaceGroup(
													   Id: "group-personal",
													   Name: "Personal",
													   Color: "#8b5cf6",
													   WorkspaceIds: ImmutableHashSet.Create("ws-gmail-personal", "ws-discord", "ws-youtube"),
													   Order: 2
													  ),
									new WorkspaceGroup(
													   Id: "group-communication",
													   Name: "Communication",
													   Color: "#10b981",
													   WorkspaceIds: ImmutableHashSet.Create("ws-whatsapp", "ws-discord", "ws-slack-team"),
													   Order: 3
													  )
								   );
	}

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
							   "google-work",
							   "Work Google",
							   AuthProfile.Providers.Google,
							   new Dictionary<string, string>
							   {
								   ["email"] = "work@company.com",
								   ["token"] = "encrypted_token_work"
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
							  ),
			AuthProfile.Create(
							   "slack-team",
							   "Team Slack",
							   AuthProfile.Providers.Slack,
							   new Dictionary<string, string>
							   {
								   ["workspace"] = "team-workspace",
								   ["token"] = "encrypted_token_slack"
							   }
							  ),
			AuthProfile.Create(
							   "github-main",
							   "GitHub Account",
							   AuthProfile.Providers.Custom,
							   new Dictionary<string, string>
							   {
								   ["username"] = "developer",
								   ["token"] = "encrypted_token_github"
							   }
							  )
		};

		return profiles.ToImmutableDictionary(p => p.Id);
	}

	private static WorkspaceFolder CreateProductivityFolder()
	{
		return WorkspaceFolder.Create(
									  "folder-productivity",
									  "Productivity",
									  WorkspaceFolder.FolderType.DROPDOWN,
									  new AppInfo("app-notion", "Notion", "https://notion.so", BrandColors.Notion, "📝"),
									  new AppInfo("app-todoist", "Todoist", "https://todoist.com", BrandColors.Todoist, "✅"),
									  new AppInfo("app-trello", "Trello", "https://trello.com", BrandColors.Trello, "📋"),
									  new AppInfo("app-asana", "Asana", "https://asana.com", BrandColors.Asana, "🎯")
									 );
	}

	private static WorkspaceFolder CreateGoogleFolder()
	{
		return WorkspaceFolder.Create(
									  "folder-google",
									  "Google Workspace",
									  WorkspaceFolder.FolderType.SIDEBAR,
									  new AppInfo.Authenticated("app-gmail", "Gmail", "https://mail.google.com", BrandColors.Gmail, "google-personal",
																"📧"),
									  new AppInfo.Authenticated("app-calendar", "Calendar", "https://calendar.google.com", BrandColors.Calendar,
																"google-personal", "📅"),
									  new AppInfo.Authenticated("app-drive", "Drive", "https://drive.google.com", BrandColors.Drive,
																"google-personal", "☁️"),
									  new AppInfo.Authenticated("app-meet", "Meet", "https://meet.google.com", BrandColors.Meet, "google-personal",
																"📹")
									 );
	}

	private static WorkspaceFolder CreateMicrosoftFolder()
	{
		return WorkspaceFolder.Create(
									  "folder-microsoft",
									  "Microsoft 365",
									  WorkspaceFolder.FolderType.SIDEBAR,
									  new AppInfo.Authenticated("app-outlook", "Outlook", "https://outlook.live.com", BrandColors.Outlook,
																"microsoft-main", "📮"),
									  new AppInfo.Authenticated("app-teams", "Teams", "https://teams.microsoft.com", BrandColors.Teams,
																"microsoft-main", "👥"),
									  new AppInfo.Authenticated("app-onedrive", "OneDrive", "https://onedrive.live.com", BrandColors.OneDrive,
																"microsoft-main", "💾"),
									  new AppInfo.Authenticated("app-onenote", "OneNote", "https://onenote.com", BrandColors.Outlook,
																"microsoft-main", "📓")
									 );
	}

	private static WorkspaceFolder CreateCommunicationFolder()
	{
		return WorkspaceFolder.Create(
									  "folder-communication",
									  "Communication",
									  WorkspaceFolder.FolderType.DROPDOWN,
									  new AppInfo.Authenticated("app-slack", "Slack", "https://slack.com", BrandColors.Slack, "slack-team", "💬"),
									  new AppInfo("app-discord", "Discord", "https://discord.com", BrandColors.Discord, "🎮"),
									  new AppInfo("app-whatsapp", "WhatsApp", "https://web.whatsapp.com", BrandColors.WhatsApp, "💚"),
									  new AppInfo("app-telegram", "Telegram", "https://web.telegram.org", BrandColors.Telegram, "✈️")
									 );
	}

	private static WorkspaceFolder CreateDevelopmentFolder()
	{
		return WorkspaceFolder.Create(
									  "folder-development",
									  "Development",
									  WorkspaceFolder.FolderType.DROPDOWN,
									  new AppInfo.Authenticated("app-github", "GitHub", "https://github.com", BrandColors.GitHub, "github-main",
																"🐙"),
									  new AppInfo("app-jira", "Jira", "https://jira.atlassian.com", BrandColors.Jira, "🔧"),
									  new AppInfo("app-figma", "Figma", "https://figma.com", BrandColors.Figma, "🎨"),
									  new AppInfo("app-miro", "Miro", "https://miro.com", BrandColors.Miro, "🎯")
									 );
	}

	private static WorkspaceFolder CreateSocialFolder()
	{
		return WorkspaceFolder.Create(
									  "folder-social",
									  "Social Media",
									  WorkspaceFolder.FolderType.SIDEBAR,
									  new AppInfo("app-twitter", "Twitter", "https://twitter.com", BrandColors.Twitter, "🐦"),
									  new AppInfo("app-linkedin", "LinkedIn", "https://linkedin.com", BrandColors.LinkedIn, "💼"),
									  new AppInfo("app-facebook", "Facebook", "https://facebook.com", BrandColors.Facebook, "👤"),
									  new AppInfo("app-instagram", "Instagram", "https://instagram.com", BrandColors.Instagram, "📷")
									 );
	}

	private static WorkspaceFolder CreateDesignFolder()
	{
		return WorkspaceFolder.Create(
									  "folder-design",
									  "Design & Media",
									  WorkspaceFolder.FolderType.DROPDOWN,
									  new AppInfo("app-figma2", "Figma", "https://figma.com", BrandColors.Figma, "🎨"),
									  new AppInfo("app-spotify", "Spotify", "https://open.spotify.com", BrandColors.Spotify, "🎵"),
									  new AppInfo.Authenticated("app-youtube", "YouTube", "https://youtube.com", BrandColors.YouTube,
																"google-personal", "📺"),
									  new AppInfo("app-dropbox", "Dropbox", "https://dropbox.com", BrandColors.Dropbox, "📦")
									 );
	}

	// Generate complete app state
	public static AppCollectionState GetMockAppState()
	{
		return new AppCollectionState(
									  Folders: GetMockFolders(),
									  Workspaces: GetMockWorkspaces(),
									  Groups: GetMockGroups(),
									  Profiles: GetMockAuthProfiles()
									 );
	}

	// Generate sample notification settings
	public static ImmutableList<NotificationSettings> GetMockNotificationSettings()
	{
		var workspaces = GetMockWorkspaces();
		return workspaces.Select(ws => new NotificationSettings(
																WorkspaceId: ws.Id,
																Enabled: ws.Id.Contains("gmail") || ws.Id.Contains("slack"),
																PlaySound: ws.Id.Contains("slack"),
																CustomSound: ws.Id.Contains("slack") ? "slack-notification.mp3" : null,
																ShowBadge: true,
																ShowDesktopNotification: true
															   )).ToImmutableList();
	}

	// Generate random workspace for testing
	public static WorkspaceInfo GenerateRandomWorkspace()
	{
		var random = new Random();
		var names = new[] { "Gmail", "Slack", "Discord", "Teams", "Notion", "Trello" };
		var colors = new[] { BrandColors.Gmail, BrandColors.Slack, BrandColors.Discord, BrandColors.Teams, BrandColors.Notion, BrandColors.Trello };
		var icons = new[] { "📧", "💬", "🎮", "👥", "📝", "📋" };

		var index = random.Next(names.Length);
		return new WorkspaceInfo(
								 Id: $"ws-{Guid.NewGuid():N}",
								 Name: $"{names[index]} {random.Next(1, 100)}",
								 Url: "https://example.com",
								 BackgroundColor: colors[index],
								 Icon: icons[index]
								);
	}

	// Generate test data with specific counts
	public static ImmutableList<WorkspaceFolder> GenerateFolders(int count)
	{
		var folders = ImmutableList.CreateBuilder<WorkspaceFolder>();
		var allFolders = GetMockFolders();

		for (int i = 0; i < count; i++)
		{
			if (i < allFolders.Count)
			{
				folders.Add(allFolders[i]);
			}
			else
			{
				// Generate additional random folders if needed
				folders.Add(WorkspaceFolder.Create(
												   $"folder-{Guid.NewGuid():N}",
												   $"Folder {i + 1}",
												   i % 2 == 0 ? WorkspaceFolder.FolderType.DROPDOWN : WorkspaceFolder.FolderType.SIDEBAR,
												   GenerateRandomApps(7).ToArray()
												  ));
			}
		}

		return folders.ToImmutable();
	}

	private static ImmutableList<AppInfo> GenerateRandomApps(int count)
	{
		var apps = ImmutableList.CreateBuilder<AppInfo>();
		var names = new[] { "App", "Tool", "Service", "Platform", "Suite" };
		var colors = new[] { "#ff6b6b", "#4ecdc4", "#45b7d1", "#96ceb4", "#ffeaa7", "#dfe6e9" };
		var icons = new[] { "🚀", "⚡", "🔥", "💎", "🌟", "✨" };

		var random = new Random();
		for (int i = 0; i < count; i++)
		{
			apps.Add(new AppInfo(
								 $"app-{Guid.NewGuid():N}",
								 $"{names[random.Next(names.Length)]} {i + 1}",
								 "https://example.com",
								 colors[random.Next(colors.Length)],
								 icons[random.Next(icons.Length)]
								));
		}

		return apps.ToImmutable();
	}
}