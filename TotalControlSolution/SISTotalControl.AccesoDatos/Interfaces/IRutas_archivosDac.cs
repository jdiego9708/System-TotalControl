using SISTotalControl.Entidades.Modelos;
using SISTotalControl.Entidades.ModelosBindeo;
using SISTotalControl.Entidades.Modelos;
using System.Data;

namespace SISTotalControl.AccesoDatos.Interfaces
{
    public interface IRutas_archivosDac
    {
        Task<string> InsertarRuta(Rutas_archivos ruta);
        Task<(DataTable dtRutas, string rpta)> BuscarRutas(string tipo_busqueda, string texto_busqueda);
    }
}
