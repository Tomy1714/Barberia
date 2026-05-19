using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface IHorariosDiasNegocio
    {
        void Configurar(string StringConexion);
        List<HorariosDias> Listar();
        HorariosDias? Guardar(HorariosDias? entidad);
        HorariosDias? Modificar(HorariosDias? entidad);
        HorariosDias? Borrar(HorariosDias? entidad);
    }
}
