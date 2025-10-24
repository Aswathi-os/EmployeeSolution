using EmployeeApp;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Read API base URL from appsettings.json
var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"];

// Register HttpClient using the API base URL
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(apiBaseUrl)
});

// Register EmployeeService
builder.Services.AddScoped<EmployeeApp.Services.EmployeeService>();

await builder.Build().RunAsync();
