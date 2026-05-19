using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface IHorariosNegocio
    {
        void Configurar(string StringConexion);
        List<Horarios> Listar();
        Horarios? Guardar(Horarios? entidad);
        Horarios? Modificar(Horarios? entidad);
        Horarios? Borrar(Horarios? entidad);
        bool EstaDisponible(Barberos barbero, int hora);
    }
}
