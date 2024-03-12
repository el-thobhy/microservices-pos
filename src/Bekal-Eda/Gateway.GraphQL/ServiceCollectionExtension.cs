namespace Gateway.GraphQL
{
    public class HttpClientConfig
    {
        public string Name { get; set; }
        public string Url { get; set; }

    }
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            var httpClients = configuration.GetSection("HttpClients").Get<IEnumerable<HttpClientConfig>>();
            var grapQLServer = services.AddGraphQLServer().AddAuthorization(); 
            foreach(var item in httpClients)
            {
                services.AddHttpClient(item.Name, async client =>
                {
                    client.Timeout = TimeSpan.FromSeconds(10);
                    client.BaseAddress = new Uri(item.Url);
                });
                grapQLServer.AddRemoteSchema(item.Name);
            }
            return services;
        }
    }
}
