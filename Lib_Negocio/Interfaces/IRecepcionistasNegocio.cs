using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface IRecepcionistasNegocio
    {
        void Configurar(string StringConexion);
        List<Recepcionistas> Listar();
        Recepcionistas? Guardar(Recepcionistas? entidad);
        Recepcionistas? Modificar(Recepcionistas? entidad);
        Recepcionistas? Borrar(Recepcionistas? entidad);
        decimal CalcularSalario(Recepcionistas entidad);
    }
}
