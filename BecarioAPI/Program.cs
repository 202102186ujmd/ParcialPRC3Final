using BecarioAPI.Models;
using BecarioAPI.Models.Interfaces;
using BecarioAPI.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;

#region Nlog Service

var logger = LogManager.Setup().
    LoadConfigurationFromAppSettings().
    GetCurrentClassLogger();

logger.Debug("Init main");
#endregion

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    //REgistramos la conexion a la base de datos
    builder.Services.AddDbContext<BecarioDBContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("BecarioconectionDb"));
    });
    //Agregamos las dependencias 
    builder.Services.AddScoped<ISolicitudesRepository, SolicitudesRepository>();
    builder.Services.AddScoped<ISolicitantesRepository, SolicitantesRepository>();
    builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    logger.Error(ex, "Error en la aplicación");
    throw;
}
finally
{
    LogManager.Shutdown();
}