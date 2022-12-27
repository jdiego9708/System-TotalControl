using SISTotalControl.Entidades.ModelosBindeo;
using SISTotalControl.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISTotalControl;

namespace SISTotalControl.Servicios.Interfaces
{
    public interface IUsuariosServicio
    {
        RespuestaServicioModel BuscarArchivos(BusquedaBindingModel busqueda);
        RespuestaServicioModel ProcesarLogin(LoginModel login);
        RespuestaServicioModel NuevoCliente(ClienteBindingModel cliente);
    }
}
