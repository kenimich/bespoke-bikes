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

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
