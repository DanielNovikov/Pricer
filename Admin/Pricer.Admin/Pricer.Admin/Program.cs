using Microsoft.Extensions.Caching.Memory;
using Microsoft.OpenApi.Writers;
using MudBlazor.Services;
using Pricer.Common;
using Pricer.Data.InMemory;
using Pricer.Data.InMemory.Seed;
using Pricer.Data.Persistent;
using Pricer.Data.Service;
using Pricer.Parser;
using Pricer.Service;
using Pricer.Telegram;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMudServices();

builder.Services
    .AddServices()
    .AddPersistentDataRepositories()
    .AddInMemoryDataRepositories()
    .AddDbContext(builder.Configuration)
    .AddMemoryCache()
    .AddDataServices()
    .AddParserServices(builder.Configuration)
    .AddCommonServices(builder.Configuration)
    .AddTelegramBot(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAuthorization();

app.UseRouting();
app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

var cache = app.Services.GetService<IMemoryCache>();
InMemorySeeder.Seed(cache);

app.Run();
