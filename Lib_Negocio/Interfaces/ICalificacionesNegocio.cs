using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface ICalificacionesNegocio
    {
        void Configurar(string StringConexion);
        List<Calificaciones> Listar();
        Calificaciones? Guardar(Calificaciones? entidad);
        Calificaciones? Modificar(Calificaciones? entidad);
        Calificaciones? Borrar(Calificaciones? entidad);
        bool EsValida(Calificaciones entidad);
        List<Calificaciones> PorBarbero(int idBarbero);
    }
}
