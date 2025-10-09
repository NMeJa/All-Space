using AllSpace.UI;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
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
		builder.Services.AddMudServices();

		// Register root component
		builder.RootComponents.Add<App>("#app");

		var app = builder.Build();

		app.MainWindow.SetTitle("AllSpace");

		AppDomain.CurrentDomain.UnhandledException += (sender, err) =>
			{
				app.MainWindow.ShowMessage("Unhandled Exception", err.ExceptionObject.ToString());
			};

		app.Run();
	}
}