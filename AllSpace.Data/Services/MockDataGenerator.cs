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

	// Generate random folder with optional nesting
	public static WorkspaceFolder GenerateRandomFolder(int currentLevel = 0, int maxLevel = 3)
	{
		var id = $"folder-{Guid.NewGuid():N}";
		var name = $"Folder {random.Next(1, 1000)}";

		// Choose folder type based on level and randomness
		var type = DetermineNestedFolderType(currentLevel, maxLevel);

		// Generate workspaces (1-5 workspaces per folder)
		var workspaceCount = random.Next(1, 6);
		var workspaces = GenerateRandomWorkspaces(workspaceCount);

		// Generate nested folders if we haven't reached max depth
		var nestedFolders = GenerateNestedFolders(currentLevel, maxLevel);

		return new WorkspaceFolder(
								   id,
								   name,
								   type,
								   workspaces.ToImmutableList(),
								   nestedFolders.ToImmutableList()
								  );
	}

	private static WorkspaceFolder.FolderType DetermineNestedFolderType(int currentLevel, int maxLevel)
	{
		// If we're at max level, don't create SIDEBAR type (no more nesting)
		if (currentLevel >= maxLevel)
		{
			return random.Next(0, 2) == 0
					   ? WorkspaceFolder.FolderType.DROPDOWN
					   : WorkspaceFolder.FolderType.DROPDOWN_COLLAPSABLE;
		}

		// 40% chance of SIDEBAR (nested), 30% DROPDOWN, 30% DROPDOWN_COLLAPSABLE
		var typeChance = random.Next(0, 10);
		return typeChance switch
		{
			< 4 => WorkspaceFolder.FolderType.SIDEBAR,
			< 7 => WorkspaceFolder.FolderType.DROPDOWN,
			_   => WorkspaceFolder.FolderType.DROPDOWN_COLLAPSABLE
		};
	}

	private static List<WorkspaceFolder> GenerateNestedFolders(int currentLevel, int maxLevel)
	{
		var nestedFolders = new List<WorkspaceFolder>();

		// Don't create nested folders if we're at max level
		if (currentLevel >= maxLevel)
			return nestedFolders;

		// 60% chance to have nested folders, with decreasing probability at deeper levels
		var nestingChance = Math.Max(0.2, 0.8 - (currentLevel * 0.15));

		if (random.NextDouble() > nestingChance)
			return nestedFolders;

		// Generate 1-4 nested folders, fewer at deeper levels
		var maxNested = Math.Max(1, 4 - currentLevel);
		var nestedCount = random.Next(1, maxNested + 1);

		for (int i = 0; i < nestedCount; i++)
		{
			nestedFolders.Add(GenerateRandomFolder(currentLevel + 1, maxLevel));
		}

		return nestedFolders;
	}

	// Generate multiple folders with nesting
	public static List<WorkspaceFolder> GenerateFolders(int count, int maxNestingLevel = 3)
	{
		var folders = new List<WorkspaceFolder>();
		var predefinedFolders = GetMockFoldersWithNesting();

		for (int i = 0; i < count; i++)
		{
			if (i < predefinedFolders.Count)
			{
				folders.Add(predefinedFolders[i]);
			}
			else
			{
				folders.Add(GenerateRandomFolder(0, maxNestingLevel));
			}
		}

		return folders;
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

	// Create predefined folders with realistic nesting structure
	public static ImmutableList<WorkspaceFolder> GetMockFoldersWithNesting()
	{
		// Level 1: Google Services
		var googleWorkspaces = new List<Workspace>
		{
			Workspace.CreateSingle("ws-gmail-1", new AppInfo("gmail-1", "Gmail Personal", "https://mail.google.com", BrandColors.Gmail)),
			Workspace.CreateSingle("ws-calendar", new AppInfo("calendar", "Calendar", "https://calendar.google.com", BrandColors.Calendar)),
		};

		// Level 2: Google nested folders
		var googleDriveFolder = new WorkspaceFolder(
													"folder-google-drive", "Drive & Docs", WorkspaceFolder.FolderType.SIDEBAR,
													new List<Workspace>
													{
														Workspace.CreateSingle("ws-drive",
																			   new AppInfo("drive", "Drive", "https://drive.google.com",
																						   BrandColors.Drive)),
														Workspace.CreateSingle("ws-docs",
																			   new AppInfo("docs", "Docs", "https://docs.google.com", "#4285f4"))
													}.ToImmutableList(),
													ImmutableList<WorkspaceFolder>.Empty
												   );

		var googleWorkFolder = new WorkspaceFolder(
												   "folder-google-work", "Work Tools", WorkspaceFolder.FolderType.SIDEBAR,
												   new List<Workspace>
												   {
													   Workspace.CreateSingle("ws-gmail-work",
																			  new AppInfo("gmail-work", "Gmail Work",
																						  "https://mail.google.com/mail/u/1", BrandColors.Gmail)),
													   Workspace.CreateSingle("ws-meet",
																			  new AppInfo("meet", "Meet", "https://meet.google.com",
																						  BrandColors.Meet))
												   }.ToImmutableList(),
												   // Level 3: Nested work categories
												   new List<WorkspaceFolder>
												   {
													   new WorkspaceFolder("folder-analytics", "Analytics", WorkspaceFolder.FolderType.DROPDOWN,
																		   new List<Workspace>
																		   {
																			   Workspace.CreateSingle("ws-analytics",
																									  new AppInfo("analytics", "Analytics",
																												  "https://analytics.google.com",
																												  "#ff6f00"))
																		   }.ToImmutableList(),
																		   ImmutableList<WorkspaceFolder>.Empty)
												   }.ToImmutableList()
												  );

		// Level 1: Development folder with deep nesting
		var frontendFolder = new WorkspaceFolder(
												 "folder-frontend", "Frontend", WorkspaceFolder.FolderType.SIDEBAR,
												 new List<Workspace>
												 {
													 Workspace.CreateSingle("ws-figma",
																			new AppInfo("figma", "Figma", "https://figma.com", BrandColors.Figma))
												 }.ToImmutableList(),
												 // Level 3: Tools within frontend
												 new List<WorkspaceFolder>
												 {
													 new WorkspaceFolder("folder-design-systems", "Design Systems",
																		 WorkspaceFolder.FolderType.DROPDOWN,
																		 new List<Workspace>
																		 {
																			 Workspace.CreateSingle("ws-storybook",
																									new AppInfo("storybook", "Storybook",
																												"https://storybook.js.org",
																												"#ff4785"))
																		 }.ToImmutableList(),
																		 ImmutableList<WorkspaceFolder>.Empty)
												 }.ToImmutableList()
												);

		var backendFolder = new WorkspaceFolder(
												"folder-backend", "Backend", WorkspaceFolder.FolderType.SIDEBAR,
												new List<Workspace>
												{
													Workspace.CreateSingle("ws-github",
																		   new AppInfo("github", "GitHub", "https://github.com", BrandColors.GitHub))
												}.ToImmutableList(),
												ImmutableList<WorkspaceFolder>.Empty
											   );

		var developmentFolder = new WorkspaceFolder(
													"folder-development", "Development", WorkspaceFolder.FolderType.SIDEBAR,
													ImmutableList<Workspace>.Empty,
													new List<WorkspaceFolder> { frontendFolder, backendFolder }.ToImmutableList()
												   );

		// Level 1: Communication with nested structure
		var teamCommFolder = new WorkspaceFolder(
												 "folder-team-comm", "Team Communication", WorkspaceFolder.FolderType.SIDEBAR,
												 new List<Workspace>
												 {
													 Workspace.CreateSingle("ws-slack",
																			new AppInfo("slack", "Slack", "https://slack.com", BrandColors.Slack)),
													 Workspace.CreateSingle("ws-teams",
																			new AppInfo("teams", "Teams", "https://teams.microsoft.com",
																						BrandColors.Teams))
												 }.ToImmutableList(),
												 // Level 3: Project-specific channels
												 new List<WorkspaceFolder>
												 {
													 new WorkspaceFolder("folder-project-alpha", "Project Alpha", WorkspaceFolder.FolderType.SIDEBAR,
																		 new List<Workspace>
																		 {
																			 Workspace.CreateSingle("ws-alpha-slack",
																									new AppInfo("alpha-slack", "Alpha Slack",
																												"https://alpha.slack.com",
																												BrandColors.Slack))
																		 }.ToImmutableList(),
																		 // Level 4: Sub-project channels
																		 new List<WorkspaceFolder>
																		 {
																			 new WorkspaceFolder("folder-alpha-dev", "Alpha Development",
																								 WorkspaceFolder.FolderType.DROPDOWN,
																								 new List<Workspace>
																								 {
																									 Workspace.CreateSingle("ws-alpha-dev",
																															new AppInfo("alpha-dev",
																																		"Dev Channel",
																																		"https://alpha-dev.slack.com",
																																		BrandColors
																																			.Slack))
																								 }.ToImmutableList(),
																								 ImmutableList<WorkspaceFolder>.Empty)
																		 }.ToImmutableList())
												 }.ToImmutableList()
												);

		var communicationFolder = new WorkspaceFolder(
													  "folder-communication", "Communication", WorkspaceFolder.FolderType.SIDEBAR,
													  new List<Workspace>
													  {
														  Workspace.CreateSingle("ws-discord",
																				 new AppInfo("discord", "Discord", "https://discord.com",
																							 BrandColors.Discord))
													  }.ToImmutableList(),
													  new List<WorkspaceFolder> { teamCommFolder }.ToImmutableList()
													 );

		// Main Google folder with nested structure
		var googleFolder = new WorkspaceFolder(
											   "folder-google", "Google Services", WorkspaceFolder.FolderType.SIDEBAR,
											   googleWorkspaces.ToImmutableList(),
											   new List<WorkspaceFolder> { googleDriveFolder, googleWorkFolder }.ToImmutableList()
											  );

		return ImmutableList.Create(
									googleFolder,
									developmentFolder,
									communicationFolder
								   );
	}

	// Get predefined mock folders (simple version without nesting)
	public static ImmutableList<WorkspaceFolder> GetMockFolders()
	{
		return GetMockFoldersWithNesting();
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
	public static AppCollectionState GenerateAppState(int folderCount, int workspaceCount, int maxNestingLevel = 3)
	{
		return new AppCollectionState(
									  Folders: GenerateFolders(folderCount, maxNestingLevel).ToImmutableList(),
									  Workspaces: GenerateRandomWorkspaces(workspaceCount).ToImmutableList(),
									  Profiles: GetMockAuthProfiles()
									 );
	}

	// Generate sample notification settings
	public static ImmutableList<NotificationSettings> GetMockNotificationSettings()
	{
		var workspaces = GetMockWorkspaces();
		return workspaces.Select(ws => new NotificationSettings(
																WorkspaceId: ws.Id,
																Enabled: ws.Name.Contains("Gmail") || ws.Name.Contains("Slack"),
																PlaySound: ws.Name.Contains("Slack"),
																CustomSound: ws.Name.Contains("Slack") ? "slack-notification.mp3" : null,
																ShowBadge: true,
																ShowDesktopNotification: true
															   )).ToImmutableList();
	}
}