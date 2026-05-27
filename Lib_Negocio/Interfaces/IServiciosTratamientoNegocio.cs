using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface IServiciosTratamientoNegocio
    {
        void Configurar(string StringConexion);
        List<ServiciosTratamiento> Listar();
        List<ServiciosTratamientoAuditoria> ListarAuditoria();
        ServiciosTratamiento? Guardar(ServiciosTratamiento? entidad);
        ServiciosTratamiento? Modificar(ServiciosTratamiento? entidad);
        ServiciosTratamiento? Borrar(ServiciosTratamiento? entidad);
        decimal CalcularPrecioFinal(ServiciosTratamiento entidad, decimal descuento);
    }
}
