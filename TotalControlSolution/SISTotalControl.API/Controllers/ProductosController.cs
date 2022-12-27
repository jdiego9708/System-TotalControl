using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SISTotalControl.Entidades.Modelos;
using SISTotalControl.Entidades.ModelosBindeo;
using SISTotalControl.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISTotalControl;
using SISTotalControl.Servicios.Interfaces;

namespace SISTotalControl.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ILogger<ProductosController> logger;
        private IProductosServicio IProductosServicio { get; set; }
        public ProductosController(ILogger<ProductosController> logger,
            IProductosServicio IProductosServicio)
        {
            this.logger = logger;
            this.IProductosServicio = IProductosServicio;
        }

        [HttpPost]
        [Route("NuevoProducto")]
        public IActionResult NuevoProducto(JObject productoJson)
        {
            try
            {
                logger.LogInformation("Inicio de nuevo producto");

                //loginJson = this.IEncriptacionHelper.ProcessJObject(loginJson);

                if (productoJson == null)
                    throw new Exception("Error con el producto de entrada");

                Productos productoModel = productoJson.ToObject<Productos>();

                if (productoModel == null)
                {
                    logger.LogInformation("Sin información de nuevo producto");
                    throw new Exception("Sin información de nuevo producto");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IProductosServicio.NuevoProducto(productoModel, 
                        productoModel.Stock_producto);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"Nuevo producto correcto Id producto: {productoModel.Id_producto}");
                        return Ok(rpta.Respuesta);
                    }
                    else
                    {
                        return BadRequest(rpta.Respuesta);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error nuevo producto", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("BuscarProductos")]
        public IActionResult BuscarProductos(JObject busquedaJson)
        {
            try
            {
                logger.LogInformation("Inicio de buscar productos");

                //loginJson = this.IEncriptacionHelper.ProcessJObject(loginJson);

                BusquedaBindingModel busquedaModel = busquedaJson.ToObject<BusquedaBindingModel>();

                if (busquedaModel == null)
                {
                    logger.LogInformation("Sin información de buscar productos");
                    throw new Exception("Sin información de buscar productos");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IProductosServicio.BuscarProductos(busquedaModel);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"Buscar productos correcto");
                        return Ok(rpta.Respuesta);
                    }
                    else
                    {
                        return BadRequest(rpta.Respuesta);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error buscar productos", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
