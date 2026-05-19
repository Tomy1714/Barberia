using Lib_Negocio.Entidades;
using Lib_Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio.Implementacion
{
    public class SedesNegocio : ISedesNegocio
    {
        private Conexion? IConexion = null;

        public SedesNegocio(Conexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.Database.GetConnectionString();
        }

        public List<Sedes> Listar()
        {
            return this.IConexion!.Sedes
                .Take(50).ToList();
        }

        public Sedes? Guardar(Sedes? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdSede != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Sedes.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Sedes? Modificar(Sedes? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdSede == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Sedes.Update(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Sedes? Borrar(Sedes? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdSede == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Sedes.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public bool HayPuestosDisponibles(Sedes entidad)
        {
            int empleadosActuales = this.IConexion!.EmpleadosSedes
                .Count(es => es.IdSede == entidad.IdSede);
            return empleadosActuales < entidad.CapacidadMaxima;
        }
    }
}
