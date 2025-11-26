using BespokeBikesApi.Data.Factories;
using BespokeBikesApi.Logic;
using BespokeBikesApi.Logic.Reports;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Bespoke Bikes API",
            Version = "v1",
            Description = "API for managing Bespoke Bikes sales and operations."
        });
    }
);

builder.Services.AddSingleton<IBespokeBikesContextFactory, BespokeBikesContextFactory>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<SaleService>();
builder.Services.AddScoped<SalespersonService>();
builder.Services.AddScoped<QuarterlyReportService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
