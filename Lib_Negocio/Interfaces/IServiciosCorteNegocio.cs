using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface IServiciosCorteNegocio
    {
        void Configurar(string StringConexion);
        List<ServiciosCorte> Listar();
        ServiciosCorte? Guardar(ServiciosCorte? entidad);
        ServiciosCorte? Modificar(ServiciosCorte? entidad);
        ServiciosCorte? Borrar(ServiciosCorte? entidad);
        decimal CalcularPrecioFinal(ServiciosCorte entidad, decimal descuento);
    }
}
