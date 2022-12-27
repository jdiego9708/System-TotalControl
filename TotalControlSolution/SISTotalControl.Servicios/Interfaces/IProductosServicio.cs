using SISTotalControl.Entidades.Modelos;
using SISTotalControl.Entidades.ModelosBindeo;
using SISTotalControl.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISTotalControl;

namespace SISTotalControl.Servicios.Interfaces
{
    public interface IProductosServicio
    {
        RespuestaServicioModel NuevoProducto(Productos producto, Stock_productos stock_inicial);
        RespuestaServicioModel BuscarProductos(BusquedaBindingModel busqueda);
    }
}
