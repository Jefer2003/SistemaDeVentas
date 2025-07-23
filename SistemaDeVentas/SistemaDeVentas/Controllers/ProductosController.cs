using Microsoft.AspNetCore.Mvc;
using SistemaDeVentas.DTOs.Producto;
using SistemaDeVentas.Interfaces;

namespace SistemaDeVentas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoResponse>>> GetProductos()
        {
            var productos = await _productoService.GetAllAsync();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoResponse>> GetProducto(int id)
        {
            var producto = await _productoService.GetByIdAsync(id);
            if (producto == null)
                return NotFound(new { message = $"Producto con ID {id} no encontrado" });
            return Ok(producto);
        }

        [HttpGet("categoria/{categoriaId}")]
        public async Task<ActionResult<IEnumerable<ProductoResponse>>> GetProductosPorCategoria(int categoriaId)
        {
            var productos = await _productoService.GetByCategoriaAsync(categoriaId);
            return Ok(productos);
        }

        [HttpPost]
        public async Task<ActionResult<ProductoResponse>> PostProducto(ProductoRequest productoRequest)
        {
            var nuevoProducto = await _productoService.CreateAsync(productoRequest);
            return CreatedAtAction(nameof(GetProducto), new { id = nuevoProducto.Id }, nuevoProducto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, ProductoRequest productoRequest)
        {
            var actualizado = await _productoService.UpdateAsync(id, productoRequest);
            if (!actualizado)
                return NotFound(new { message = $"Producto con ID {id} no encontrado" });
            return Ok(new { message = "Producto actualizado exitosamente" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var eliminado = await _productoService.DeleteAsync(id);
            if (!eliminado)
                return NotFound(new { message = $"Producto con ID {id} no encontrado" });
            return Ok(new { message = "Producto eliminado exitosamente" });
        }
    }
} 