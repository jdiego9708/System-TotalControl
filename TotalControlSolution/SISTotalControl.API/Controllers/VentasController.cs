﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SISTotalControl.Entidades.ModelosBindeo;
using SISTotalControl.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISTotalControl;
using SISTotalControl.Servicios.Interfaces;

namespace SISTotalControl.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly ILogger<VentasController> logger;
        private IVentasServicio IVentasServicio { get; set; }
        public VentasController(ILogger<VentasController> logger,
            IVentasServicio IVentasServicio)
        {
            this.logger = logger;
            this.IVentasServicio = IVentasServicio;
        }

        [HttpPost]
        [Route("BuscarEstadisticasDiarias")]
        public IActionResult BuscarEstadisticasDiarias(JObject busquedaJson)
        {
            try
            {
                logger.LogInformation("Inicio de Busqueda de estadisticas");

                BusquedaBindingModel busquedaModel = busquedaJson.ToObject<BusquedaBindingModel>();

                if (busquedaModel == null)
                {
                    logger.LogInformation("Sin información de busquedaModel");
                    throw new Exception("Sin información de busquedaModel");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IVentasServicio.BuscarEstadisticasDiarias(busquedaModel);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"Busqueda correcta");
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
                logger.LogError("Error busqueda de agendamientos", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("BuscarVentas")]
        public IActionResult BuscarVentas(JObject busquedaJson)
        {
            try
            {
                logger.LogInformation("Inicio de Busqueda de ventas");

                BusquedaBindingModel busquedaModel = busquedaJson.ToObject<BusquedaBindingModel>();

                if (busquedaModel == null)
                {
                    logger.LogInformation("Sin información de busquedaModel");
                    throw new Exception("Sin información de busquedaModel");
                }
                else
                {
                    RespuestaServicioModel rpta = this.IVentasServicio.BuscarVentas(busquedaModel);
                    if (rpta.Correcto)
                    {
                        logger.LogInformation($"Busqueda correcta");
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
                logger.LogError("Error busqueda de agendamientos", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
