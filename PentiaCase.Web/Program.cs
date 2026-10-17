using PentiaCase.Web.Mappings;
using PentiaCase.Web.Services;
using PentiaCase.Web.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register AutoMapper.
builder.Services.AddAutoMapper(
    cfg => { },
    typeof(MappingProfile)
);

builder.Services.AddHttpClient<IWingineersApiClient, WingineersApiClient>(
    (serviceProvider, client) =>
    {
        var configuration =
            serviceProvider.GetRequiredService<IConfiguration>();

        var baseUrl = configuration["WingineersApi:BaseUrl"]
            ?? throw new InvalidOperationException(
                "Wingineers API BaseUrl is missing.");

        var apiKey = configuration["WingineersApi:ApiKey"]
            ?? throw new InvalidOperationException(
                "Wingineers API key is missing.");

        client.BaseAddress = new Uri(baseUrl);

        client.DefaultRequestHeaders.Add("ApiKey", apiKey);
        client.Timeout = TimeSpan.FromSeconds(30);
    });

builder.Services.AddScoped<ISalesPersonService, SalesPersonService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
