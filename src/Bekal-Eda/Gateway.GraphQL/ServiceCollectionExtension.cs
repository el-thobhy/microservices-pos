using System.Net.Http.Headers;

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
            services.AddHttpClient("User", async client =>
            {
                client.BaseAddress = new Uri(configuration["HttpClients:UserService"]);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetToken());
            });
            services.AddHttpClient("Store", async client =>
            {
                client.BaseAddress = new Uri(configuration["HttpClients:StoreService"]);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetToken());
            });
            services.AddHttpClient("Order", async client =>
            {
                client.BaseAddress = new Uri(configuration["HttpClients:OrderService"]);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetToken());
            });

            services.AddHttpClient("LookUp", async client =>
            {
                client.BaseAddress = new Uri(configuration["HttpClients:LookUpService"]);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetToken());
            });

            services.AddHttpClient("Payment", async client =>
            {
                client.BaseAddress = new Uri(configuration["HttpClients:PaymentService"]);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetToken());
            });

            var grapQLServer = services
                .AddGraphQLServer()
                .AddRemoteSchema("User")
                .AddRemoteSchema("Store")
                .AddRemoteSchema("Order")
                .AddRemoteSchema("LookUp") 
                .AddRemoteSchema("Payment"); 

            return services;
        }

        private async static Task<string?> GetToken()
        {
            HttpContextAccessor accessor = new HttpContextAccessor();
            HttpContext context = accessor.HttpContext;
            if(context != null)
            {
                var header = context.Request.Headers.Where(o => o.Key == "Authorization");
                if (header.Count() > 0)
                    return header.FirstOrDefault().Value.ToString().Replace("Bearer ", "");
            }
            return null;
        }
    }
}
