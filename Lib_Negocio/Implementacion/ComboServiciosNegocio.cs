using Lib_Negocio.Entidades;
using Lib_Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio.Implementacion
{
    public class ComboServiciosNegocio : IComboServiciosNegocio
    {
        private Conexion? IConexion = null;

        public ComboServiciosNegocio(Conexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.Database.GetConnectionString();
        }

        public List<ComboServicios> Listar()
        {
            return this.IConexion!.ComboServicios
                .Include(e => e.Combo)
                .Include(e => e.Servicio)
                .Take(50).ToList();
        }

        public ComboServicios? Guardar(ComboServicios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdComboServicio != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.ComboServicios.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public ComboServicios? Modificar(ComboServicios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdComboServicio == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.ComboServicios.Update(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public ComboServicios? Borrar(ComboServicios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdComboServicio == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.ComboServicios.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}
