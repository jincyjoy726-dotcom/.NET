var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("CalculatorApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7087/");
});

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern:"{controller=CalculatorClient}/{action=Index}/{id?}");
app.Run();