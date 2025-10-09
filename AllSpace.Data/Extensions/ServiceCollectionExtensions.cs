using Microsoft.Extensions.DependencyInjection;

namespace AllSpace.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddData(this IServiceCollection services)
	{
		return services;
	}
}