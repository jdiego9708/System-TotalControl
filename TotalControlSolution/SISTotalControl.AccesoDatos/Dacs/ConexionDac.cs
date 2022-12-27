using SISTotalControl.AccesoDatos.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SISTotalControl.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISTotalControl;

namespace SISTotalControl.AccesoDatos.Dacs
{
    public class ConexionDac : IConexionDac
    {
        public IConfiguration Configuration { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        public ConfiguracionSISTotalControl ConfiguracionSISTotalControl { get; set; }
        public ConexionDac(IConfiguration Configuration)
        {
            this.Configuration = Configuration;

            var settings = this.Configuration.GetSection("ConnectionStrings");
            this.ConnectionStrings = settings.Get<ConnectionStrings>() ?? new ConnectionStrings();

            settings = this.Configuration.GetSection("ConfiguracionSISTotalControl");
            this.ConfiguracionSISTotalControl = settings.Get<ConfiguracionSISTotalControl>() ?? new ConfiguracionSISTotalControl();
        }
        public string Cn()
        {
            string connectionDefault = this.ConfiguracionSISTotalControl.BDPredeterminada;

            if (connectionDefault.Equals("ConexionBDDesarrollo"))
                return ConnectionStrings.ConexionBDDesarrollo;
            else if (connectionDefault.Equals("ConexionBDProduccion"))
                return ConnectionStrings.ConexionBDProduccion;
            else
                return ConnectionStrings.ConexionBDDesarrollo;
        }
    }
}
