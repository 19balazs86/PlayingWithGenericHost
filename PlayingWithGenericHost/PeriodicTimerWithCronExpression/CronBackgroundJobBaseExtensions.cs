namespace PlayingWithGenericHost.PeriodicTimerWithCronExpression
{
    public static class CronBackgroundJobBaseExtensions
    {
        public static IServiceCollection AddCronJob<T>(
            this IServiceCollection services,
            Action<CronSettings<T>> options) where T : CronBackgroundJobBase
        {
            ArgumentNullException.ThrowIfNull(options, nameof(options));

            var config = new CronSettings<T>();

            options.Invoke(config);

            if (string.IsNullOrWhiteSpace(config.CronExpression))
            {
                throw new ArgumentNullException(nameof(CronSettings<T>.CronExpression));
            }

            services.AddSingleton(config);
            services.AddHostedService<T>();

            return services;
        }
    }
}
