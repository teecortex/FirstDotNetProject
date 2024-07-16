using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ISubscriberService, SubscriberService>();
builder.Services.AddScoped<ITariffService, TariffService>();
builder.Services.AddScoped<IServiceService, ServiceService>();

builder.Services.AddDataContext();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "appsettings.json"));
Console.WriteLine(data);


var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
