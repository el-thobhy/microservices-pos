using Framework.Core.Event;
using Framework.Kafka;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Store.Domain;
using Store.Domain.MapProfile;
using Store.Domain.Repositories;
using Store.Domain.Services;
using Store.GraphQL.Schema.Mutation;
using Store.GraphQL.Schema.Query;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDomainContext(builder.Configuration);
// Add services to the container.
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer("Bearer", opt =>
{
    var configuration = builder.Configuration;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = false,
        ValidIssuer = configuration["JWT:ValidIssuer"],
        ValidAudience = configuration["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
    opt.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            context.Response.OnStarting(async () =>
            {
                await context.Response.WriteAsync("You are not authorized!");
            });
            return Task.CompletedTask;
        },
        OnForbidden = context =>
        {
            context.Response.OnStarting(async () =>
            {
                await context.Response.WriteAsync("You are forbidden!");
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

builder.Services.AddControllers();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<EntityToDtoProfile>();
});

builder.Services.AddStore();
builder.Services.AddValidator();
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
    .AddTypeExtension<ProductMutation>()
    .AddAuthorization();

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();

app.UseAuthorization();

app.UseCors("AllowAnyOrigin");

app.MapControllers();

app.MapGraphQL();

app.Run();
