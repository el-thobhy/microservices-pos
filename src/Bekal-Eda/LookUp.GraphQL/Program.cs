using Framework.Kafka;
using LookUp.Domain.MapProfile;
using LookUp.Domain.Repositories;
using LookUp.Domain.Services;
using LookUp.GraphQL.Schema.Mutation;
using LookUp.GraphQL.Schema.Query;
using User.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDomainContext(builder.Configuration);
builder.Services.AddValidator();
builder.Services.AddKafkaProducer();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<EntityToDtoProfile>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddScoped<Query>()
    .AddScoped<AttributeQuery>()
    .AddScoped<Mutation>()
    .AddScoped<AttributeMutation>()
    .AddScoped<IAttributeReposiotories, AttributeRepositories>()
    .AddScoped<IAttributeService, AttributesServices>()
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddTypeExtension<AttributeQuery>()
    .AddMutationType<Mutation>()
    .AddTypeExtension<AttributeMutation>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGraphQL();

app.Run();
