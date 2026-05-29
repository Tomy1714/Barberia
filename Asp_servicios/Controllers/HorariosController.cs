using Microsoft.AspNetCore.Mvc;
using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Nucleo;

namespace Asp_Servicios.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HorariosController : ControllerBase
    {
        private readonly HorariosNegocio negocio;

        public HorariosController()
        {
            Conexion conexion = new Conexion();
            conexion.StringConexion = Configuraciones.obtener("StringConexion");
            negocio = new HorariosNegocio(conexion);
            negocio.Configurar(conexion.StringConexion!);
        }

        // GET: api/Horarios
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

        // GET: api/Horarios/5
        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            try
            {
                var lista  = negocio.Listar();
                var entidad = lista.FirstOrDefault(e => e.IdHorario == id);
                if (entidad == null)
                    return NotFound(new { mensaje = "Registro no encontrado" });
                return Ok(entidad);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // POST: api/Horarios
        [HttpPost]
        public IActionResult Guardar([FromBody] Horarios? entidad)
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

        // PUT: api/Horarios
        [HttpPut]
        public IActionResult Modificar([FromBody] Horarios? entidad)
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

        // DELETE: api/Horarios
        [HttpDelete]
        public IActionResult Borrar([FromBody] Horarios? entidad)
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

        // GET: api/[controller]/auditoria
        [HttpGet("auditoria")]
        public IActionResult ListarAuditoria()
        {
            try
            {
                var lista = negocio.ListarAuditoria();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
