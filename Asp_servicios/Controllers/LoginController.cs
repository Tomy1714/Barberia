using Microsoft.AspNetCore.Mvc;
using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Nucleo;

namespace Asp_Servicios.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        // POST: api/Login
        [HttpPost]
        public IActionResult Autenticar([FromBody] Usuarios? entidad)
        {
            try
            {
                if (entidad == null ||
                    string.IsNullOrEmpty(entidad.Email) ||
                    string.IsNullOrEmpty(entidad.Contrasena))
                    return BadRequest(new { mensaje = "Credenciales incompletas" });

                Conexion conexion = new Conexion();
                conexion.StringConexion = Configuraciones.obtener("StringConexion");

                var usuario = conexion.Usuarios
                    .FirstOrDefault(u => u.Email      == entidad.Email
                                      && u.Contrasena == entidad.Contrasena
                                      && u.Activo     == true);

                if (usuario == null)
                    return Unauthorized(new { mensaje = "Credenciales incorrectas" });

                return Ok(new
                {
                    usuario.IdUsuario,
                    usuario.IdPersona,
                    usuario.Email,
                    usuario.Rol,
                    usuario.Activo
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
