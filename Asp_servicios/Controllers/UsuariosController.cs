using Microsoft.AspNetCore.Mvc;
using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Nucleo;

namespace Asp_Servicios.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuariosNegocio negocio;

        public UsuariosController()
        {
            Conexion conexion = new Conexion();
            conexion.StringConexion = Configuraciones.obtener("StringConexion");
            negocio = new UsuariosNegocio(conexion);
            negocio.Configurar(conexion.StringConexion!);
        }

        // GET: api/Usuarios
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var lista = negocio.Listar();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            try
            {
                var lista = negocio.Listar();
                var entidad = lista.FirstOrDefault(e => e.IdUsuario == id);
                if (entidad == null)
                    return NotFound(new { mensaje = "Registro no encontrado" });
                return Ok(entidad);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // POST: api/Usuarios
        [HttpPost]
        public IActionResult Guardar([FromBody] Usuarios? entidad)
        {
            try
            {
                var resultado = negocio.Guardar(entidad);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // PUT: api/Usuarios
        [HttpPut]
        public IActionResult Modificar([FromBody] Usuarios? entidad)
        {
            try
            {
                var resultado = negocio.Modificar(entidad);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // DELETE: api/Usuarios
        [HttpDelete]
        public IActionResult Borrar([FromBody] Usuarios? entidad)
        {
            try
            {
                var resultado = negocio.Borrar(entidad);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
