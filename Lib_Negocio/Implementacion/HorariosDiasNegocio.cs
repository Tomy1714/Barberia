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

        public List<HorariosDias> Listar()
        {
            return this.IConexion!.HorariosDias
                .Include(e => e.Horarios)
                .Take(50).ToList();
        }

        public HorariosDias? Guardar(HorariosDias? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdHorarioDia != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.HorariosDias.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public HorariosDias? Modificar(HorariosDias? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdHorarioDia == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.HorariosDias.Update(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public HorariosDias? Borrar(HorariosDias? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdHorarioDia == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.HorariosDias.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}
