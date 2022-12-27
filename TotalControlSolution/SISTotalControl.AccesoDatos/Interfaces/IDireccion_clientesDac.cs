using SISTotalControl.Entidades.Modelos;

namespace SISTotalControl.AccesoDatos.Interfaces
{
    public interface IDireccion_clientesDac
    {
        Task<string> InsertarDireccion(Direccion_clientes direccion);
    }
}
