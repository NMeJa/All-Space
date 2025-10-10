using AllSpace.Data.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AllSpace.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddData(this IServiceCollection services)
	{
		services.AddScoped<IIconService, IconService>();
		services.AddScoped<IAssetPathResolver, AssetPathResolver>();
		return services;
	}
}