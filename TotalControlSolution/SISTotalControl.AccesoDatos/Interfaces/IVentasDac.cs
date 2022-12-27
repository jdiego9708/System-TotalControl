﻿using SISTotalControl.Entidades.Modelos;
using System.Data;

namespace SISTotalControl.AccesoDatos.Interfaces
{
    public interface IVentasDac
    {
        Task<string> InsertarVentas(Ventas venta);
        string EditarVentas(int id_venta, Ventas venta);
        string EditarCobroVenta(int id_venta, int id_cobro);
        Task<string> CambiarEstadoVenta(int id_venta, string estado);
        Task<(DataTable dtVentas, string rpta)> BuscarVentas(string tipo_busqueda, string texto_busqueda);
        Task<(DataSet ds, string rpta)> BuscarEstadisticasDiarias(string texto_busqueda, string fecha);
    }
}
