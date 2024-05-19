using todoApi.Service;
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;


// Add services to the container.

builder.Services.AddControllers();
var cosmosDbSettings = builder.Configuration.GetSection("CosmosDb");
builder.Services.AddSingleton<ICosmosDbService>(new CosmosDbService(
    cosmosDbSettings["DatabaseName"],
    cosmosDbSettings["ContainerName"],
    cosmosDbSettings["Account"],
    cosmosDbSettings["Key"]
));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddJsonFile("appsettings.Secrets.json", optional: true);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
