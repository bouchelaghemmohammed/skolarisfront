using Skolaris.Client.Components;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Options JSON partagées
var jsonOptions = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

jsonOptions.Converters.Add(new JsonStringEnumConverter());
builder.Services.AddSingleton(jsonOptions);

// HttpClient vers ton backend
builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri("http://localhost:5043/")
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();