namespace SCM.Core.Extensions
{
    public static class ResilienceExtensions
    {
        public static IHttpClientBuilder AddResiliencePolicies(this IHttpClientBuilder builder)
        {
            return builder
                .AddTransientHttpErrorPolicy(p =>
                    p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(500)))
                .AddTransientHttpErrorPolicy(p =>
                    p.CircuitBreakerAsync(
                        handledEventsAllowedBeforeBreaking: 3,
                        durationOfBreak: TimeSpan.FromSeconds(30)
                    ));
        }
    }
}
