using GadgetRepairApi.Data;
using GadgetRepairApi.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://0.0.0.0:8080");

builder.Services.AddDbContext<GadgetRepairDbContext>(options =>
    options.UseInMemoryDatabase("GadgetRepairDb"));

builder.Services.AddScoped<IGadgetRepository, GadgetRepository>();
builder.Services.AddScoped<IRepairOrderRepository, RepairOrderRepository>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<GadgetRepairDbContext>().Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "API is running");

app.Run();
