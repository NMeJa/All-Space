using Microsoft.Extensions.DependencyInjection;

namespace AllSpace.Security;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddSecurity(this IServiceCollection services)
	{
		return services;
	}
}