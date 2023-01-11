using SISTotalControl.AccesoDatos.Dacs;
using SISTotalControl.AccesoDatos.Interfaces;
using SISTotalControl.AccesoDatos;
using System.Reflection;
using SISTotalControl.Servicios.Interfaces;
using SISTotalControl.Servicios.Servicios;
using SISTotalControl.Entidades.Helpers.Interfaces;
using SISTotalControl.Entidades.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddNewtonsoftJson();

Assembly GetAssemblyByName(string name)
{
    Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().
           SingleOrDefault(assembly => assembly.GetName().Name == name);

    if (assembly == null)
        return null;

    return assembly;
}

var a = GetAssemblyByName("SISTotalControl.Entidades");

using var stream = a.GetManifestResourceStream("SISTotalControl.Entidades.Configuracion.ConfiguracionAPI.appsettings.json");

var config = new ConfigurationBuilder()
    .AddJsonStream(stream)
    .Build();

builder.Configuration.AddConfiguration(config);

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

#region INYECCION DE ACCESO DATOS
builder.Services.AddTransient<IConexionDac, ConexionDac>()
    .AddTransient<IAgendamiento_cobrosDac, DAgendamiento_cobros>()
    .AddTransient<IUsuariosDac, DUsuarios>()
    .AddTransient<IVentasDac, DVentas>()
    .AddTransient<ITurnosDac, DTurnos>();
#endregion

#region INYECCION DE SERVICIOS
builder.Services.AddTransient<IUsuariosServicio, UsuariosServicio>();
builder.Services.AddTransient<IAgendamientosServicio, AgendamientosServicio>();
builder.Services.AddTransient<IVentasServicio, VentasServicio>();
builder.Services.AddTransient<IBlobStorageService, BlobStorageService>();
builder.Services.AddTransient<IDireccion_clientesDac, DDireccion_clientes>();
builder.Services.AddTransient<IAgendamiento_cobrosDac, DAgendamiento_cobros>();
builder.Services.AddTransient<IRutas_archivosDac, DRutas_archivos>();
builder.Services.AddTransient<ITurnosDac, DTurnos>();
builder.Services.AddTransient<IUsuariosDac, DUsuarios>();
builder.Services.AddTransient<IVentasDac, DVentas>();
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();

app.UseSwaggerUI();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseDefaultFiles();

app.UseStaticFiles();

app.Run();