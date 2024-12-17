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
        builder => builder.WithOrigins("https://facturaspruebatecnicaqualita.vercel.app")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        var context = services.GetRequiredService<ModelContext>();
        context.Database.CanConnect();
        logger.LogInformation("Conexión a la base de datos exitosa.");

        // Consulta para validar si la tabla Clientes existe
        var clientes = context.Clientes.ToList();
        if (clientes.Any())
        {
            logger.LogInformation("La tabla Clientes existe y contiene datos.");
        }
        else
        {
            logger.LogInformation("La tabla Clientes existe pero no contiene datos.");
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Ocurrió un error al intentar conectar a la base de datos o al consultar la tabla Clientes.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Use CORS policy
app.UseCors("AllowLocalhost");

app.UseAuthorization();

app.MapControllers();

app.Run();
