using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface IPromocionesNegocio
    {
        void Configurar(string StringConexion);
        List<Promociones> Listar();
        Promociones? Guardar(Promociones? entidad);
        Promociones? Modificar(Promociones? entidad);
        Promociones? Borrar(Promociones? entidad);
        decimal CalcularDescuento(Promociones entidad, decimal precio);
    }
}
