using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface IUsuariosNegocio
    {
        void Configurar(string StringConexion);
        List<Usuarios> Listar();
        Usuarios? Guardar(Usuarios? entidad);
        Usuarios? Modificar(Usuarios? entidad);
        Usuarios? Borrar(Usuarios? entidad);
        Usuarios? Login(string email, string contrasena);
    }
}
