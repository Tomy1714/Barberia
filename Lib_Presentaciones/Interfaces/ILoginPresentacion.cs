using Lib_Negocio.Entidades;

namespace LibPresentaciones.Interfaces
{
    public interface ILoginPresentacion
    {
        Task<Usuarios?> Login(string email, string contrasena);
    }
}
