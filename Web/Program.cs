using ApplicationCore.Interfaces;
using Infrastructure;
using Web;
using Web.GrpcServices;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ISubscriberService, ApplicationCore.Services.SubscriberService>();
builder.Services.AddScoped<ITariffService, ApplicationCore.Services.TariffService>();
builder.Services.AddScoped<IServiceService, ApplicationCore.Services.ServiceService>();

// Add secrets to Program.cs
builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddDataContext(builder.Configuration["ConnectionString"]);
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName = "rediska";
});


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGrpc();
builder.Services.AddGraphQLServer()
    .AddQueryType<QueryProvider>()
    .AddProjections()
    .AddFiltering()
    .AddSorting();


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

// Добавляем gRPC сервис

app.MapGrpcService<SubscriberApiService>();
app.MapGrpcService<TariffApiService>();
app.MapGrpcService<ServiceApiService>();


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

app.MapGraphQL("/graphql");

app.Run();
