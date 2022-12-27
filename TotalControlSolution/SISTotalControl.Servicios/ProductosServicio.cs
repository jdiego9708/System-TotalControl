using Newtonsoft.Json;
using SISTotalControl.AccesoDatos.Interfaces;
using SISTotalControl.Entidades.Helpers.Interfaces;
using SISTotalControl.Entidades.Modelos;
using SISTotalControl.Entidades.ModelosBindeo;
using SISTotalControl.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISTotalControl;
using SISTotalControl.Servicios.Interfaces;
using System.Data;

namespace SISTotalControl.Servicios.Servicios
{
    public class ProductosServicio : IProductosServicio
    {
        #region CONSTRUCTOR
        public IUsuariosDac UsuariosDac { get; set; }
        public IDireccion_clientesDac Direccion_clientesDac { get; set; }
        public IVentasDac VentasDac { get; set; }
        public IAgendamiento_cobrosDac Agendamiento_cobrosDac { get; set; }
        public IBlobStorageService IBlobStorageService { get; set; }
        public IRutas_archivosDac IRutas_archivosDac { get; set; }
        public ITurnosDac ITurnosDac { get; set; }
        public IProductosDac IProductosDac { get; set; }
        public IStock_productosDac IStock_productosDac { get; set; }
        public ProductosServicio(IUsuariosDac UsuariosDac,
            IDireccion_clientesDac Direccion_clientesDac,
            IVentasDac VentasDac,
            IAgendamiento_cobrosDac Agendamiento_cobrosDac,
            IBlobStorageService IBlobStorageService,
            IRutas_archivosDac IRutas_archivosDac,
            ITurnosDac ITurnosDac,
            IProductosDac IProductosDac,
            IStock_productosDac IStock_productosDac)
        {
            this.UsuariosDac = UsuariosDac;
            this.Direccion_clientesDac = Direccion_clientesDac;
            this.VentasDac = VentasDac;
            this.Agendamiento_cobrosDac = Agendamiento_cobrosDac;
            this.IBlobStorageService = IBlobStorageService;
            this.IRutas_archivosDac = IRutas_archivosDac;
            this.ITurnosDac = ITurnosDac;
            this.IProductosDac = IProductosDac;
            this.IStock_productosDac = IStock_productosDac;
        }
        #endregion

        #region MÉTODOS
        public RespuestaServicioModel NuevoProducto(Productos producto, Stock_productos stock_inicial)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                string rpta = this.IProductosDac.InsertarProducto(producto).Result;
                if (!rpta.Equals("OK"))
                    throw new Exception($"Hubo un error insertando el producto, detalles: {rpta}");

                if (producto.Imagenes != null)
                {
                    if (producto.Imagenes.Count > 0)
                    {
                        int contador = 1;
                        foreach (string imagenbase64 in producto.Imagenes)
                        {
                            byte[] imagenByte = Convert.FromBase64String(imagenbase64);

                            Stream stream = new MemoryStream(imagenByte);

                            BlobResponse response = this.IBlobStorageService.SubirArchivoContainerBlobStorage(stream,
                                $"{producto.Id_producto}Imagen{contador}", "imagenesproducto");

                            if (!response.IsSuccess)
                                throw new Exception("Error guardando las imagenes en el blob");

                            if (response.Message == null)
                                throw new Exception("Error guardando las imagenes en el blob");

                            string uri = Convert.ToString(response.Message ?? "");

                            if (string.IsNullOrEmpty(uri))
                                throw new Exception("Error con la URL devuelta");

                            DirectoryInfo info = new(uri);

                            Rutas_archivos ruta = new()
                            {
                                Id_usuario = producto.Id_producto,
                                Tipo_archivo = "IMAGEN PRODUCTO",
                                Fecha_archivo = DateTime.Now,
                                Hora_archivo = DateTime.Now.TimeOfDay,
                                Nombre_archivo = Path.GetFileName(info.FullName),
                                Ruta_archivo = uri,
                                Extension_archivo = info.Extension,
                            };

                            rpta = this.IRutas_archivosDac.InsertarRuta(ruta).Result;
                            if (!rpta.Equals("OK"))
                                throw new Exception("Error guardando las imágenes en la BD");

                            contador++;
                        }
                    }
                }

                stock_inicial.Id_producto = producto.Id_producto;

                rpta = this.IStock_productosDac.InsertarStock(stock_inicial).Result;
                if (!rpta.Equals("OK"))
                    throw new Exception($"Hubo un error insertando el stock inicial, detalles: {rpta}");

                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(new { Producto = producto, Stock = stock_inicial });
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Correcto = false;
                respuesta.Respuesta = ex.Message;
                return respuesta;
            }
        }
        public RespuestaServicioModel BuscarProductos(BusquedaBindingModel busqueda)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                var result = this.IProductosDac.BuscarProductos(busqueda.Tipo_busqueda, busqueda.Texto_busqueda1).Result;

                if (result.dtProductos == null)
                    throw new Exception("Error al buscar los productos");

                if (result.dtProductos.Rows.Count < 1)
                    throw new Exception("No se encontraron productos");

                List<Productos> productos = (from DataRow row in result.dtProductos.Rows
                                              select new Productos(row)).ToList();

                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(productos);
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Correcto = false;
                respuesta.Respuesta = ex.Message;
                return respuesta;
            }
        }
        #endregion
    }
}
