using GadgetRepairApi.Data;
using GadgetRepairApi.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GadgetRepairDbContext>(options =>
    options.UseInMemoryDatabase("GadgetRepairDb"));

builder.Services.AddScoped<IGadgetRepository, GadgetRepository>();
builder.Services.AddScoped<IRepairOrderRepository, RepairOrderRepository>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Hello from Gadget Repair API!");

app.Run();
