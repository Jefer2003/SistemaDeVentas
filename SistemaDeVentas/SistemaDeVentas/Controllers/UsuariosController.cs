using Microsoft.AspNetCore.Mvc;
using SistemaDeVentas.DTOs.Usuario;
using SistemaDeVentas.Interfaces;

namespace SistemaDeVentas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioResponse>>> GetUsuarios()
        {
            var usuarios = await _usuarioService.GetAllAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioResponse>> GetUsuario(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null)
                return NotFound(new { message = $"Usuario con ID {id} no encontrado" });
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioResponse>> PostUsuario(UsuarioRequest usuarioRequest)
        {
            var nuevoUsuario = await _usuarioService.CreateAsync(usuarioRequest);
            return CreatedAtAction(nameof(GetUsuario), new { id = nuevoUsuario.Id }, nuevoUsuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, UsuarioRequest usuarioRequest)
        {
            var actualizado = await _usuarioService.UpdateAsync(id, usuarioRequest);
            if (!actualizado)
                return NotFound(new { message = $"Usuario con ID {id} no encontrado" });
            return Ok(new { message = "Usuario actualizado exitosamente" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var eliminado = await _usuarioService.DeleteAsync(id);
            if (!eliminado)
                return NotFound(new { message = $"Usuario con ID {id} no encontrado" });
            return Ok(new { message = "Usuario eliminado exitosamente" });
        }
    }
} 