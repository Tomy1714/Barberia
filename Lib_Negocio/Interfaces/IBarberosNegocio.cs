using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface IBarberosNegocio
    {
        void Configurar(string StringConexion);
        List<Barberos> Listar();
        Barberos? Guardar(Barberos? entidad);
        Barberos? Modificar(Barberos? entidad);
        Barberos? Borrar(Barberos? entidad);
        decimal CalcularSalario(Barberos entidad);
    }
}
