using SISTotalControl.Entidades.Modelos;
using System.Data;

namespace SISTotalControl.AccesoDatos.Interfaces
{
    public interface IProductosDac
    {
        Task<string> InsertarProducto(Productos producto);
        Task<string> EditarProducto(Productos producto);
        Task<(DataTable dtProductos, string rpta)> BuscarProductos(string tipo_busqueda, string texto_busqueda);
    }
}
