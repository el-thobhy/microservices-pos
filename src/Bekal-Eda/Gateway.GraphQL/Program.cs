using Gateway.GraphQL;
using Gateway.GraphQL.Schema.Mutation;
using Gateway.GraphQL.Schema.Query;
using HotChocolate.Execution.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddHttpClientServices(builder.Configuration);

//builder.Services
//    .AddScoped<Query>()
//    .AddScoped<Mutation>()
//    .AddGraphQLServer()
//    .AddQueryType<Query>()
//    .AddMutationType<Mutation>();

// Add services to the container.

var app = builder.Build();

app.MapGraphQL();

app.Run();