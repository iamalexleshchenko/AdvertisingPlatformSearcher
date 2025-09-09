using AdvertisingPlatformsSearcher.Interfaces;
using AdvertisingPlatformsSearcher.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IPlatformParser, PlatformParserService>();
builder.Services.AddSingleton<IUrlTreeBuilder, UrlTreeBuilderService>();
builder.Services.AddSingleton<IPlatformSearcher, PlatformSearcherService>();
builder.Services.AddSingleton<IAdvertisingDataStore, AdvertisingDataStore>();
builder.WebHost.UseUrls("http://localhost:5003", "https://localhost:5004");

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();