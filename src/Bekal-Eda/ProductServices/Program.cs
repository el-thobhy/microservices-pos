using CreateProductServices.Schema.Mutation;
using Product.Domain;
using Product.Domain.MapProfile;
using Product.Domain.Repositories;
using Product.Domain.Schema;
using Framework.Kafka;

using Product.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDomainContext(builder.Configuration);

builder.Services.AddKafkaProducer();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<EntityToDtoProfile>();
});

builder.Services
    .AddScoped<Query>()
    .AddScoped<EmptyQuery>()
    .AddScoped<Mutation>()
    .AddScoped<ProductMutation>()
    .AddScoped<IProductCommandService, ProductCommandService>()
    .AddScoped<IProductQueryService, ProductQueryService>()
    .AddScoped<IProductRepository, ProductRepository>()
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddTypeExtension<EmptyQuery>()
    .AddMutationType<Mutation>()
    .AddTypeExtension<ProductMutation>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapGraphQL();

app.Run();
