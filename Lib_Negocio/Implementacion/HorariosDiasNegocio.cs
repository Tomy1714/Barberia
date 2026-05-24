using Lib_Negocio.Entidades;
using Lib_Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio.Implementacion
{
    public class HorariosDiasNegocio : IHorariosDiasNegocio
    {
        private Conexion? IConexion = null;

        public HorariosDiasNegocio(Conexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.Database.GetConnectionString();
        }

        public List<HorarioDias> Listar()
        {
            return this.IConexion!.HorarioDias
                .Include(e => e.Horario)
                .Take(50).ToList();
        }

        public HorarioDias? Guardar(HorarioDias? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdHorarioDia != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.HorarioDias.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public HorarioDias? Modificar(HorarioDias? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdHorarioDia == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.HorarioDias.Update(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public HorarioDias? Borrar(HorarioDias? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdHorarioDia == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.HorarioDias.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}
