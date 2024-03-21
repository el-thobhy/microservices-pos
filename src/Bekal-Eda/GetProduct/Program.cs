using Product.Domain;
using Product.Domain.MapProfile;
using Product.Domain.Repositories;
using Product.Domain.Schema;
using Product.Domain.Services;
using Framework.Kafka;
using Framework.Core.Event;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGetDomainContext(builder.Configuration);
builder.Services.AddEventBus();
builder.Services.AddKafkaProducer();
builder.Services.AddKafkaConsumer();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<EntityToDtoProfile>();
});
builder.Services.AddProduct();

builder.Services
    .AddScoped<Query>()
    .AddScoped<ProductQuery>()
    .AddScoped<IProductQueryService, ProductQueryService>()
    .AddScoped<IProductRepository, ProductRepository>()
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddTypeExtension<ProductQuery>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGraphQL();

app.Run();