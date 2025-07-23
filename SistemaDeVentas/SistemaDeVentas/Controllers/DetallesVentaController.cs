using Microsoft.AspNetCore.Mvc;
using SistemaDeVentas.DTOs.DetalleVenta;
using SistemaDeVentas.Interfaces;

namespace SistemaDeVentas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetallesVentaController : ControllerBase
    {
        private readonly IDetalleVentaService _detalleVentaService;

        public DetallesVentaController(IDetalleVentaService detalleVentaService)
        {
            _detalleVentaService = detalleVentaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleVentaResponse>>> GetDetallesVenta()
        {
            var detalles = await _detalleVentaService.GetAllAsync();
            return Ok(detalles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleVentaResponse>> GetDetalleVenta(int id)
        {
            var detalle = await _detalleVentaService.GetByIdAsync(id);
            if (detalle == null)
                return NotFound(new { message = $"DetalleVenta con ID {id} no encontrado" });
            return Ok(detalle);
        }

        [HttpGet("venta/{ventaId}")]
        public async Task<ActionResult<IEnumerable<DetalleVentaResponse>>> GetDetallesPorVenta(int ventaId)
        {
            var detalles = await _detalleVentaService.GetByVentaAsync(ventaId);
            return Ok(detalles);
        }

        [HttpGet("producto/{productoId}")]
        public async Task<ActionResult<IEnumerable<DetalleVentaResponse>>> GetDetallesPorProducto(int productoId)
        {
            var detalles = await _detalleVentaService.GetByProductoAsync(productoId);
            return Ok(detalles);
        }

        [HttpPost]
        public async Task<ActionResult<DetalleVentaResponse>> PostDetalleVenta(DetalleVentaRequest detalleRequest)
        {
            var nuevoDetalle = await _detalleVentaService.CreateAsync(detalleRequest);
            return CreatedAtAction(nameof(GetDetalleVenta), new { id = nuevoDetalle.Id }, nuevoDetalle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleVenta(int id, DetalleVentaRequest detalleRequest)
        {
            var actualizado = await _detalleVentaService.UpdateAsync(id, detalleRequest);
            if (!actualizado)
                return NotFound(new { message = $"DetalleVenta con ID {id} no encontrado" });
            return Ok(new { message = "DetalleVenta actualizado exitosamente" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleVenta(int id)
        {
            var eliminado = await _detalleVentaService.DeleteAsync(id);
            if (!eliminado)
                return NotFound(new { message = $"DetalleVenta con ID {id} no encontrado" });
            return Ok(new { message = "DetalleVenta eliminado exitosamente" });
        }
    }
} 