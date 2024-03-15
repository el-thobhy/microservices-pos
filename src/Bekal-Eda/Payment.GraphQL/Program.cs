using Framework.Core.Event;
using Framework.Kafka;
using Payment.Domain;
using Payment.Domain.MapProfile;
using Payment.Domain.Repositories;
using Payment.Domain.Services;
using Payment.GraphQL.Schema.Mutation;
using Payment.GraphQL.Schema.Query;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDomainContext(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<EntityToDtoProfile>();
});
builder.Services.AddPayment();
builder.Services.AddCart();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEventBus();
builder.Services.AddKafkaConsumer();
builder.Services.AddKafkaProducer();
builder.Services.AddHttpContextAccessor();


builder.Services
    .AddScoped<Query>()
    .AddScoped<PaymentQuery>()
    .AddScoped<Mutation>()
    .AddScoped<PaymentMutation>()
    .AddScoped<ICartProductRepository, CartProductRepository>()
    .AddScoped<IPaymentRepository, PaymentRepository>()
    .AddScoped<IProductRepository, ProductRepository>()
    .AddScoped<IPaymentService, PaymentServices>()
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddTypeExtension<PaymentQuery>()
    .AddMutationType<Mutation>()
    .AddTypeExtension<PaymentMutation>();
//.AddAuthorization();



var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.MapGraphQL();

app.Run();