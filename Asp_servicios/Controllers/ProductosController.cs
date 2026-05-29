using Microsoft.AspNetCore.Mvc;
using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Nucleo;

namespace Asp_Servicios.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly ProductosNegocio negocio;

        public ProductosController()
        {
            Conexion conexion = new Conexion();
            conexion.StringConexion = Configuraciones.obtener("StringConexion");
            negocio = new ProductosNegocio(conexion);
            negocio.Configurar(conexion.StringConexion!);
        }

        // GET: api/Productos
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

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            try
            {
                var lista  = negocio.Listar();
                var entidad = lista.FirstOrDefault(e => e.IdProducto == id);
                if (entidad == null)
                    return NotFound(new { mensaje = "Registro no encontrado" });
                return Ok(entidad);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // POST: api/Productos
        [HttpPost]
        public IActionResult Guardar([FromBody] Productos? entidad)
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

        // PUT: api/Productos
        [HttpPut]
        public IActionResult Modificar([FromBody] Productos? entidad)
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

        // DELETE: api/Productos
        [HttpDelete]
        public IActionResult Borrar([FromBody] Productos? entidad)
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
