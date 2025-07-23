using Microsoft.AspNetCore.Mvc;
using SistemaDeVentas.DTOs.Venta;
using SistemaDeVentas.Interfaces;

namespace SistemaDeVentas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : ControllerBase
    {
        private readonly IVentaService _ventaService;

        public VentasController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VentaResponse>>> GetVentas()
        {
            try
            {
                var ventas = await _ventaService.GetAllVentasAsync();
                return Ok(ventas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener las ventas", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VentaResponse>> GetVenta(int id)
        {
            try
            {
                var venta = await _ventaService.GetVentaByIdAsync(id);
                if (venta == null)
                    return NotFound(new { message = $"Venta con ID {id} no encontrada" });

                return Ok(venta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener la venta", details = ex.Message });
            }
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<VentaResponse>>> GetVentasPorUsuario(int usuarioId)
        {
            try
            {
                var ventas = await _ventaService.GetVentasByUsuarioAsync(usuarioId);
                return Ok(ventas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener las ventas del usuario", details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<VentaResponse>> PostVenta(VentaRequest ventaRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var venta = await _ventaService.CreateVentaAsync(ventaRequest);
                return CreatedAtAction(nameof(GetVenta), new { id = venta.Id }, venta);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear la venta", details = ex.Message });
            }
        }

        [HttpPut("{id}/estado")]
        public async Task<IActionResult> ActualizarEstadoVenta(int id, [FromBody] string estado)
        {
            try
            {
                var actualizado = await _ventaService.UpdateEstadoVentaAsync(id, estado);
                if (!actualizado)
                    return NotFound(new { message = $"Venta con ID {id} no encontrada" });

                return Ok(new { message = "Estado de venta actualizado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar el estado de la venta", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenta(int id)
        {
            try
            {
                var eliminado = await _ventaService.DeleteVentaAsync(id);
                if (!eliminado)
                    return NotFound(new { message = $"Venta con ID {id} no encontrada" });

                return Ok(new { message = "Venta eliminada exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar la venta", details = ex.Message });
            }
        }
    }
} 