using Lib_Negocio.Entidades;
using Lib_Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio.Implementacion
{
    public class RecepcionistasNegocio : IRecepcionistasNegocio
    {
        private Conexion? IConexion = null;

        public RecepcionistasNegocio(Conexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.Database.GetConnectionString();
        }

        public List<Recepcionistas> Listar()
        {
            return this.IConexion!.Recepcionistas
                .Include(e => e.Empleado)
                .Include(e => e.Sede)
                .Take(50).ToList();
        }

        public List<RecepcionistasAuditoria> ListarAuditoria()
        {
            return this.IConexion!.RecepcionistasAuditoria
                .Take(50).ToList();
        }

        public Recepcionistas? Guardar(Recepcionistas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdRecepcionista != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Recepcionistas.Add(entidad);
            this.IConexion.SaveChanges();

            this.IConexion!.RecepcionistasAuditoria.Add(new RecepcionistasAuditoria
            {
                IdRecepcionista = entidad.IdRecepcionista,
                Accion = "Insertar",
                Fecha = DateTime.Now
            });
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Recepcionistas? Modificar(Recepcionistas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdRecepcionista == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Recepcionistas.Update(entidad);
            this.IConexion.SaveChanges();

            this.IConexion!.RecepcionistasAuditoria.Add(new RecepcionistasAuditoria
            {
                IdRecepcionista = entidad.IdRecepcionista,
                Accion = "Modificar",
                Fecha = DateTime.Now
            });
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Recepcionistas? Borrar(Recepcionistas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdRecepcionista == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Recepcionistas.Remove(entidad);
            this.IConexion.SaveChanges();

            this.IConexion!.RecepcionistasAuditoria.Add(new RecepcionistasAuditoria
            {
                IdRecepcionista = entidad.IdRecepcionista,
                Accion = "Borrar",
                Fecha = DateTime.Now
            });
            this.IConexion.SaveChanges();
            return entidad;
        }

        public decimal CalcularSalario(Recepcionistas entidad)
        {
            if (entidad.CitasGestionadasMes > 100)
                return entidad.Empleado!.SalarioBase + entidad.BonoPorMeta;
            return entidad.Empleado!.SalarioBase;
        }
    }
}
