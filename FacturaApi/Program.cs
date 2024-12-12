using Microsoft.EntityFrameworkCore;
using ProyectoFactura.BLL.Service;
using ProyectoFactura.DAL.DataContext;
using ProyectoFactura.DAL.Repositories;
using ProyectoFactura.Models;
using Microsoft.AspNetCore.Cors; // Add this using directive

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("oracleConnection");
builder.Services.AddDbContext<ModelContext>(options => options.UseOracle(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Corrige el problema cíclico de los json
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        //Evita añadir id a cada referencia de los ciclos en los json
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddScoped<IGenericRepository<Cliente>, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();

builder.Services.AddScoped<IGenericRepository<Supervisor>, SupervisorRepository>();
builder.Services.AddScoped<ISupervisorService, SupervisorService>();

builder.Services.AddScoped<IGenericRepository<Mesero>, MeseroRepository>();
builder.Services.AddScoped<IMeseroService, MeseroService>();

builder.Services.AddScoped<IGenericRepository<Mesa>, MesaRepository>();
builder.Services.AddScoped<IMesaService, MesaService>();

builder.Services.AddScoped<IGenericRepository<Factura>, FacturaRepository>();
builder.Services.AddScoped<IFacturaService, FacturaService>();

builder.Services.AddScoped<IGenericRepository<Detallexfactura>, DetalleFacturaRepository>();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder => builder.WithOrigins("http://localhost:5173")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

// Use CORS policy
app.UseCors("AllowLocalhost");

app.MapControllers();

app.Run();
