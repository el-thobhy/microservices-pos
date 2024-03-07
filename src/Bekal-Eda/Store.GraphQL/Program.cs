using Framework.Core.Event;
using Framework.Kafka;
using Store.Domain;
using Store.Domain.MapProfile;
using Store.Domain.Repositories;
using Store.Domain.Services;
using Store.GraphQL.Schema.Mutation;
using Store.GraphQL.Schema.Query;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDomainContext(builder.Configuration);
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<EntityToDtoProfile>();
});

builder.Services.AddStore();
builder.Services.UpdateStoreAttribute();
builder.Services.ChangeStatusAttribute();
builder.Services.AddEventBus();
builder.Services.AddKafkaProducer();
builder.Services.AddKafkaConsumer();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddScoped<Query>()
    .AddScoped<CategoryQuery>()
    .AddScoped<ProductQuery>()
    .AddScoped<Mutation>()
    .AddScoped<CategoryMutation>()
    .AddScoped<ProductMutation>()
    .AddScoped<ICategoryRepository, CategoryRepository>()
    .AddScoped<ICategoryService, CategoryService>()
    .AddScoped<IProductService, ProductService>()
    .AddScoped<IProductRepository, ProductRepository>()
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddTypeExtension<CategoryQuery>()
    .AddTypeExtension<ProductQuery>()
    .AddMutationType<Mutation>()
    .AddTypeExtension<CategoryMutation>()
    .AddTypeExtension<ProductMutation>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MapGraphQL();

app.Run();
