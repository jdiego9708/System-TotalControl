using SISTotalControl.Entidades.ModelosBindeo;
using SISTotalControl.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISTotalControl;

namespace SISTotalControl.Servicios.Interfaces
{
    public interface IVentasServicio
    {
        RespuestaServicioModel BuscarEstadisticasDiarias(BusquedaBindingModel busqueda);
        RespuestaServicioModel BuscarVentas(BusquedaBindingModel busqueda);
    }
}
