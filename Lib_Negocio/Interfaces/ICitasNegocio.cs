using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface ICitasNegocio
    {
        void Configurar(string StringConexion);
        List<Citas> Listar();
        Citas? Guardar(Citas? entidad);
        Citas? Modificar(Citas? entidad);
        Citas? Borrar(Citas? entidad);
        List<Citas> PorCliente(int idCliente);
        List<Citas> PorEstado(string estado);
        bool ConfirmarCita(Citas entidad);
        bool CancelarCita(Citas entidad);
    }
}
