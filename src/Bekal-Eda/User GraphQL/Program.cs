using User.Domain;
using User.Domain.MapProfile;
using User.Domain.Repositories;
using User.Domain.Services;
using User_GraphQL.Schema.Mutations;
using User_GraphQL.Schema.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDomainContext(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<EntityToDtoProfile>();
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddScoped<Query>()
    .AddScoped<AuthQuery>()
    .AddScoped<UserQuery>()
    .AddScoped<Mutation>()
    .AddScoped<UserMutation>()
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<IUserService, UserServices>()
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddTypeExtension<AuthQuery>()
    .AddTypeExtension<UserQuery>()
    .AddMutationType<Mutation>()
    .AddTypeExtension<UserMutation>(); 



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
