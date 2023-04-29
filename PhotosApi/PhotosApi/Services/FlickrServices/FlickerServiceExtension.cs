namespace PhotosApi.Services.FlickrServices
{
    public static class FlickerServiceExtension
    {
        public static void RegisterFlickerService(this IServiceCollection services)
        {
            services.AddScoped<IFlickerService, FlickerService>();
        }
    }
}
