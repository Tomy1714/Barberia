using Microsoft.AspNetCore.Mvc;
using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Nucleo;

namespace Asp_Servicios.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadosSedesController : ControllerBase
    {
        private readonly EmpleadosSedesNegocio negocio;

        public EmpleadosSedesController()
        {
            Conexion conexion = new Conexion();
            conexion.StringConexion = Configuraciones.obtener("StringConexion");
            negocio = new EmpleadosSedesNegocio(conexion);
            negocio.Configurar(conexion.StringConexion!);
        }

        // GET: api/EmpleadosSedes
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

        // GET: api/EmpleadosSedes/5
        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            try
            {
                var lista  = negocio.Listar();
                var entidad = lista.FirstOrDefault(e => e.IdEmpleadoSede == id);
                if (entidad == null)
                    return NotFound(new { mensaje = "Registro no encontrado" });
                return Ok(entidad);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // POST: api/EmpleadosSedes
        [HttpPost]
        public IActionResult Guardar([FromBody] EmpleadoSede? entidad)
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

        // PUT: api/EmpleadosSedes
        [HttpPut]
        public IActionResult Modificar([FromBody] EmpleadoSede? entidad)
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

        // DELETE: api/EmpleadosSedes
        [HttpDelete]
        public IActionResult Borrar([FromBody] EmpleadoSede? entidad)
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
