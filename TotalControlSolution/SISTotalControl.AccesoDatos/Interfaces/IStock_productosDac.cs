using SISTotalControl.Entidades.Modelos;

namespace SISTotalControl.AccesoDatos.Interfaces
{
    public interface IStock_productosDac
    {
        Task<string> InsertarStock(Stock_productos stock);
    }
}
