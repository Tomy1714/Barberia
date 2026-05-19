using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface IPagosNegocio
    {
        void Configurar(string StringConexion);
        List<Pagos> Listar();
        Pagos? Guardar(Pagos? entidad);
        Pagos? Modificar(Pagos? entidad);
        Pagos? Borrar(Pagos? entidad);
        decimal CalcularTotal(Pagos entidad);
        bool ProcesarPago(Pagos entidad);
    }
}
