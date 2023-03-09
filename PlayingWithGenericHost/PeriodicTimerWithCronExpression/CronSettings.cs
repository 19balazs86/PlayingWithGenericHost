namespace PlayingWithGenericHost.PeriodicTimerWithCronExpression;

public sealed class CronSettings<T>
{
    public string CronExpression { get; set; } = default!;
    public TimeZoneInfo TimeZone { get; set; } = default!;
}
