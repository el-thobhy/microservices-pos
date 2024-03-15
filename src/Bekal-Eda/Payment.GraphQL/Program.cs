using Framework.Core.Event;
using Framework.Kafka;
using Payment.Domain;
using Payment.Domain.MapProfile;

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




var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

//app.MapGraphQL();

app.Run();