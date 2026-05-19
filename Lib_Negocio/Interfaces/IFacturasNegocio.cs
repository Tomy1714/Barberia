using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface IFacturasNegocio
    {
        void Configurar(string StringConexion);
        List<Facturas> Listar();
        Facturas? Guardar(Facturas? entidad);
        Facturas? Modificar(Facturas? entidad);
        Facturas? Borrar(Facturas? entidad);
        List<Facturas> PorCliente(int idCliente);
    }
}
