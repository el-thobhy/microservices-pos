using Framework.Core.Event;
using Framework.Kafka;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Order.Domain;
using Order.Domain.MapProfile;
using Order.Domain.Repositories;
using Order.Domain.Services;
using Order.GraphQL.Schema.Mutation;
using Order.GraphQL.Schema.Query;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDomainContext(builder.Configuration);
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<EntityToDtoProfile>();
});

builder.Services.AddUser();
builder.Services.AddProduct();
builder.Services.AddEventBus();
builder.Services.AddKafkaConsumer();
builder.Services.AddKafkaProducer();
builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
            .AddScoped<Query>()
            .AddScoped<CartQuery>()
            .AddScoped<CartProductQuery>()
            .AddScoped<Mutation>()
            .AddScoped<CartMutation>()
            .AddScoped<CartProductMutation>()
            .AddScoped<ICartService, CartService>()
            .AddScoped<ICartRepository, CartRepository>()
            .AddScoped<ICartProductService, CartProductService>()
            .AddScoped<ICartProductRepository, CartProductRepository>()
            .AddGraphQLServer()
            .AddType<CartQuery>()
            .AddType<CartProductQuery>()
            .AddType<CartMutation>()
            .AddType<CartProductMutation>()
            .AddQueryType<Query>()
            .AddMutationType<Mutation>()
            .AddAuthorization();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer("Bearer", options =>
{
    var Configuration = builder.Configuration;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = Configuration["JWT:ValidIssuer"],
        ValidAudience = Configuration["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
    };
    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            context.Response.OnStarting(async () =>
            {
                await context.Response.WriteAsync("Account not authorized");
            });
            return Task.CompletedTask;
        },
        OnForbidden = context =>
        {
            context.Response.OnStarting(async () =>
            {
                await context.Response.WriteAsync("Forbidden Account");
            });
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddCors(o => o.AddPolicy("AllowAnyOrigin",
    builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    }
    ));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapGraphQL();

app.UseCors("AllowAnyOrigin");

app.MapControllers();

app.Run();
