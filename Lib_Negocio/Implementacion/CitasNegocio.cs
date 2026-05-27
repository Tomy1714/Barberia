using Lib_Negocio.Entidades;
using Lib_Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio.Implementacion
{
    public class CitasNegocio : ICitasNegocio
    {
        private Conexion? IConexion = null;

        public CitasNegocio(Conexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.Database.GetConnectionString();
        }

        public List<Citas> Listar()
        {
            return this.IConexion!.Citas
                .Include(e => e.Cliente)
                .Include(e => e.Barbero)
                .Include(e => e.Servicio)
                .Take(50).ToList();
        }

        public List<CitasAuditoria> ListarAuditoria()
        {
            return this.IConexion!.CitasAuditoria
                .Take(50).ToList();
        }

        public Citas? Guardar(Citas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdCita != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Citas.Add(entidad);
            this.IConexion.SaveChanges();

            this.IConexion!.CitasAuditoria.Add(new CitasAuditoria
            {
                IdCita = entidad.IdCita,
                Accion = "Insertar",
                Fecha = DateTime.Now
            });
            return entidad;
        }

        public Citas? Modificar(Citas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdCita == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Citas.Update(entidad);
            this.IConexion.SaveChanges();

            this.IConexion!.CitasAuditoria.Add(new CitasAuditoria
            {
                IdCita = entidad.IdCita,
                Accion = "Modificar",
                Fecha = DateTime.Now
            });
            return entidad;
        }

        public Citas? Borrar(Citas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdCita == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Citas.Remove(entidad);
            this.IConexion.SaveChanges();

            this.IConexion!.CitasAuditoria.Add(new CitasAuditoria
            {
                IdCita = entidad.IdCita,
                Accion = "Borrar",
                Fecha = DateTime.Now
            });
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Citas> PorCliente(int idCliente)
        {
            return this.IConexion!.Citas
                .Where(c => c.IdCliente == idCliente)
                .Take(50).ToList();
        }

        public List<Citas> PorEstado(string estado)
        {
            return this.IConexion!.Citas
                .Where(c => c.Estado == estado)
                .Take(50).ToList();
        }

        public bool ConfirmarCita(Citas entidad)
        {
            if (entidad.Estado == "Pendiente")
            {
                entidad.Estado = "Confirmada";
                this.IConexion!.SaveChanges();
                return true;
            }
            return false;
        }

        public bool CancelarCita(Citas entidad)
        {
            entidad.Estado = "Cancelada";
            this.IConexion!.SaveChanges();
            return true;
        }
    }
}
