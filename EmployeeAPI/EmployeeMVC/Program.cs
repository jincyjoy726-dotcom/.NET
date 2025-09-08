//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllersWithViews();

//builder.Services.AddHttpClient("EmployeeAPI", httpClient =>
//{
//    // Find the URL your API is running on (e.g., https://localhost:7123)
//    // and put it here. This is the "phone number" for the power plant.
//    //httpClient.BaseAddress = new Uri("https://localhost:7177/");// <-- IMPORTANT: REPLACE XXXX with your API's port number
//    httpClient.BaseAddress = new Uri("http://localhost:5117/");
//    //string? apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7177/"; // <-- REPLACE XXXX with your API's local port
//    //httpClient.BaseAddress = new Uri(apiBaseUrl);
//});
//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();
using EmployeeMVC.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// --- REPLACE the existing AddHttpClient code with this ---

builder.Services.AddHttpClient("EmployeeAPI", httpClient =>
{
    // This line reads the "ApiBaseUrl" environment variable we set in docker-compose.yml.
    string? apiBaseUrl = builder.Configuration["ApiBaseUrl"];

    // This is a new, important check. If the environment variable is missing for any reason,
    // the application will now crash on startup with a clear error message instead of
    // silently trying to connect to the wrong address.
    if (string.IsNullOrEmpty(apiBaseUrl))
    {
        throw new InvalidOperationException("Fatal Error: ApiBaseUrl is not configured in the environment.");
    }

    // Set the base address for all calls made by this HttpClient.
    httpClient.BaseAddress = new Uri(apiBaseUrl);
});

// --- END OF REPLACEMENT ---


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Use this for better error details in development
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();