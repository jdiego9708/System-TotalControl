using SISTotalControl.Entidades.ModelosBindeo;
using SISTotalControl.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISTotalControl;

namespace SISTotalControl.Servicios.Interfaces
{
    public interface IAgendamientosServicio
    {
        RespuestaServicioModel SincronizarFilas(BusquedaBindingModel busqueda);
        RespuestaServicioModel AplazarCuota(PagarCuotaBindingModel cuota);
        RespuestaServicioModel PagarCuota(PagarCuotaBindingModel cuota);
        RespuestaServicioModel BuscarAgendamientos(BusquedaBindingModel busqueda);
    }
}
