using Lib_Negocio.Entidades;
using Lib_Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio.Implementacion
{
    public class PersonasNegocio : IPersonasNegocio
    {
        private Conexion? IConexion = null;

        public PersonasNegocio(Conexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.Database.GetConnectionString();
        }

        public List<Personas> Listar()
        {
            return this.IConexion!.Personas
                .Take(50).ToList();
        }

        public Personas? Guardar(Personas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdPersona != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Personas.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Personas? Modificar(Personas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdPersona == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Personas.Update(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Personas? Borrar(Personas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdPersona == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Personas.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}
