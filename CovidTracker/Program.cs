using AutoMapper;
using CovidTracker.Client.Clients;
using CovidTracker.Client.Factories;
using CovidTracker.Client.Resolvers;
using CovidTracker.Common.Profiles;
using CovidTracker.Services;
using Serilog;
using Polly;
using System.Net;
using CovidTracker.Client.JsonDeserializers;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

MapperConfiguration mapperConfiguration = new MapperConfiguration(mapperConfig => {
    mapperConfig.AddProfile<StateProfile>();
});

builder.Services.AddSingleton(mapperConfiguration.CreateMapper());


builder.Services.AddSingleton<ICovidTrackerService, CovidTrackerService>();
builder.Services.AddSingleton<CovidTrackerFactory>();
builder.Services.AddSingleton<JsonResolver>();
builder.Services.AddSingleton<StateDeserializer>();

var baseAddress = builder.Configuration.GetValue<string>("Api:BaseAddress");

builder.Services.AddHttpClient<CovidTrackerApiClient>("covidtrackerapi", client =>
{
    client.BaseAddress = new Uri(baseAddress);
    client.DefaultRequestHeaders.Add("accept", "application/json");
    
})
    .AddPolicyHandler(Policy<HttpResponseMessage>
    .Handle<HttpRequestException>()
        .OrResult(x => x.StatusCode is >= HttpStatusCode.InternalServerError or HttpStatusCode.GatewayTimeout)
        .WaitAndRetryAsync(3, retyAttempt => TimeSpan.FromSeconds(Math.Pow(2,retyAttempt)))
        );

//logging
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341"));

builder.Services.AddMudServices();
var app = builder.Build();
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
