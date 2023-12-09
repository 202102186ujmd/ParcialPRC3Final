using BecarioAPP.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
// agregar el servicio de httpclient que contendra la url del api y la configuracion de la misma
builder.Services.AddHttpClient("", options =>
{
    options.BaseAddress = new Uri(builder.Configuration.GetSection("UrlData").GetValue<string>("BaseAddressURL"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
