using AllSpace.Core;
using AllSpace.Data;
using AllSpace.Security;
using AllSpace.UI;
using Microsoft.Extensions.DependencyInjection;
using Photino.Blazor;

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable once CheckNamespace
internal class Program
{
	[STAThread]
	private static void Main(string[] args)
	{
		var builder = PhotinoBlazorAppBuilder.CreateDefault(args);

		// Add services
		builder.Services.AddLogging();

		//Services from Other connected projects
		builder.Services.AddData();
		builder.Services.AddCore();
		builder.Services.AddSecurity();

		// Register root component
		builder.RootComponents.Add<App>("#app");

		var app = builder.Build();

		app.MainWindow
		   .SetSize(1400, 900)
		   .Center()
		   .SetDevToolsEnabled(true)
		   .SetLogVerbosity(0)
		   .SetTitle("AllSpace");

		AppDomain.CurrentDomain.UnhandledException += (sender, err) =>
			{
				app.MainWindow.ShowMessage("Unhandled Exception", err.ExceptionObject.ToString());
			};

		app.Run();
	}
}