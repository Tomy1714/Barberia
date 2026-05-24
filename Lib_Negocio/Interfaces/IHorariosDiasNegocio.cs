using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface IHorariosDiasNegocio
    {
        void Configurar(string StringConexion);
        List<HorarioDias> Listar();
        HorarioDias? Guardar(HorarioDias? entidad);
        HorarioDias? Modificar(HorarioDias? entidad);
        HorarioDias? Borrar(HorarioDias? entidad);
    }
}
