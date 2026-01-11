using LostColonyManager.Interface.ExceptionHandler;

namespace LostColonyManager.Interface.ExceptionHandling
{
    public static class ExceptionHandlingExtensions
    {
        public static IServiceCollection AddApiExceptionHandling(this IServiceCollection services)
        {
            services.AddExceptionHandler<ApiExceptionHandler>();
            services.AddProblemDetails();
            return services;
        }

        public static IApplicationBuilder UseApiExceptionHandling(this IApplicationBuilder app)
        {
            app.UseExceptionHandler();
            return app;
        }
    }
}
