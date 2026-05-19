using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface IPuntosFidelidadNegocio
    {
        void Configurar(string StringConexion);
        List<PuntosFidelidad> Listar();
        PuntosFidelidad? Guardar(PuntosFidelidad? entidad);
        PuntosFidelidad? Modificar(PuntosFidelidad? entidad);
        PuntosFidelidad? Borrar(PuntosFidelidad? entidad);
        int GanarPuntos(PuntosFidelidad entidad, decimal montoPagado);
        decimal CanjearPuntos(PuntosFidelidad entidad, int puntosACanjear);
    }
}
