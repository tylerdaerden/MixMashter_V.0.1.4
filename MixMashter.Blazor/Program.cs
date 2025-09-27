using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MixMashter.Blazor;
using MixMashter.Blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// LocalStorage
builder.Services.AddBlazoredLocalStorage();

// Authorization handler
builder.Services.AddTransient<AuthorizationMessageHandler>();

// Auth API (pas besoin de token)
builder.Services.AddHttpClient<AuthApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7141/api/");
});

// Playlist API (avec token automatique)
builder.Services.AddHttpClient<PlaylistApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7141/api/");
}).AddHttpMessageHandler<AuthorizationMessageHandler>();

// Mashup API (avec token automatique)
builder.Services.AddHttpClient<MashupApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7141/api/");
}).AddHttpMessageHandler<AuthorizationMessageHandler>();

// Auth custom (JwtAuthStateProvider)
builder.Services.AddScoped<JwtAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<JwtAuthStateProvider>());

// Ajout nécessaire pour <AuthorizeView>, roles, policies (oubli avant et ça plantait mon démarrage)
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
