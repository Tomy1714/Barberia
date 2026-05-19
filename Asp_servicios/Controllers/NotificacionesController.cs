using Microsoft.AspNetCore.Mvc;
using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Nucleo;

namespace Asp_Servicios.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificacionesController : ControllerBase
    {
        private readonly NotificacionesNegocio negocio;

        public NotificacionesController()
        {
            Conexion conexion = new Conexion();
            conexion.StringConexion = Configuraciones.obtener("StringConexion");
            negocio = new NotificacionesNegocio(conexion);
            negocio.Configurar(conexion.StringConexion!);
        }

        // GET: api/Notificaciones
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

        // GET: api/Notificaciones/5
        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            try
            {
                var lista  = negocio.Listar();
                var entidad = lista.FirstOrDefault(e => e.IdNotificacion == id);
                if (entidad == null)
                    return NotFound(new { mensaje = "Registro no encontrado" });
                return Ok(entidad);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // POST: api/Notificaciones
        [HttpPost]
        public IActionResult Guardar([FromBody] Notificaciones? entidad)
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

        // PUT: api/Notificaciones
        [HttpPut]
        public IActionResult Modificar([FromBody] Notificaciones? entidad)
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

        // DELETE: api/Notificaciones
        [HttpDelete]
        public IActionResult Borrar([FromBody] Notificaciones? entidad)
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
