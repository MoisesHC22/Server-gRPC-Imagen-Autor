using gRPC.Autor.Serve.Persistencia;
using gRPC.Autor.Serve.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.Configure<MongoDBSetting>(builder.Configuration.GetSection("Mongo"));
builder.Services.AddSingleton(sp =>
sp.GetRequiredService<IOptions<MongoDBSetting>>().Value);

builder.Services.AddSingleton<ImagenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<ImagenService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
