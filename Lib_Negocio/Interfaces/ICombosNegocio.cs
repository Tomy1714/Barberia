using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface ICombosNegocio
    {
        void Configurar(string StringConexion);
        List<Combos> Listar();
        Combos? Guardar(Combos? entidad);
        Combos? Modificar(Combos? entidad);
        Combos? Borrar(Combos? entidad);
        decimal CalcularPrecioFinal(Combos entidad, decimal descuento);
    }
}
