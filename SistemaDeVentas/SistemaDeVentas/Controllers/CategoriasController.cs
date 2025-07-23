using Microsoft.AspNetCore.Mvc;
using SistemaDeVentas.DTOs.Categoria;
using SistemaDeVentas.Interfaces;

namespace SistemaDeVentas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaResponse>>> GetCategorias()
        {
            var categorias = await _categoriaService.GetAllAsync();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaResponse>> GetCategoria(int id)
        {
            var categoria = await _categoriaService.GetByIdAsync(id);
            if (categoria == null)
                return NotFound(new { message = $"Categoría con ID {id} no encontrada" });
            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaResponse>> PostCategoria(CategoriaRequest categoriaRequest)
        {
            var nuevaCategoria = await _categoriaService.CreateAsync(categoriaRequest);
            return CreatedAtAction(nameof(GetCategoria), new { id = nuevaCategoria.Id }, nuevaCategoria);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, CategoriaRequest categoriaRequest)
        {
            var actualizado = await _categoriaService.UpdateAsync(id, categoriaRequest);
            if (!actualizado)
                return NotFound(new { message = $"Categoría con ID {id} no encontrada" });
            return Ok(new { message = "Categoría actualizada exitosamente" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var eliminado = await _categoriaService.DeleteAsync(id);
            if (!eliminado)
                return NotFound(new { message = $"Categoría con ID {id} no encontrada" });
            return Ok(new { message = "Categoría eliminada exitosamente" });
        }
    }
} 