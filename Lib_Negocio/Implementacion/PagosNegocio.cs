using Lib_Negocio.Entidades;
using Lib_Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio.Implementacion
{
    public class PagosNegocio : IPagosNegocio
    {
        private Conexion? IConexion = null;

        public PagosNegocio(Conexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.Database.GetConnectionString();
        }

        public List<Pagos> Listar()
        {
            return this.IConexion!.Pagos
                .Include(e => e.Cita)
                .Take(50).ToList();
        }

        public List<PagosAuditoria> ListarAuditoria()
        {
            return this.IConexion!.PagosAuditoria
                .Take(50).ToList();
        }

        public Pagos? Guardar(Pagos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdPago != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Pagos.Add(entidad);
            this.IConexion.SaveChanges();

            this.IConexion!.PagosAuditoria.Add(new PagosAuditoria
            {
                IdPago = entidad.IdPago,
                Accion = "Insertar",
                Fecha = DateTime.Now
            });
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Pagos? Modificar(Pagos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdPago == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Pagos.Update(entidad);
            this.IConexion.SaveChanges();

            this.IConexion!.PagosAuditoria.Add(new PagosAuditoria
            {
                IdPago = entidad.IdPago,
                Accion = "Modificar",
                Fecha = DateTime.Now
            });
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Pagos? Borrar(Pagos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdPago == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Pagos.Remove(entidad);
            this.IConexion.SaveChanges();

            this.IConexion!.PagosAuditoria.Add(new PagosAuditoria
            {
                IdPago = entidad.IdPago,
                Accion = "Borrar",
                Fecha = DateTime.Now
            });
            this.IConexion.SaveChanges();
            return entidad;
        }

        public decimal CalcularTotal(Pagos entidad)
        {
            return entidad.Monto - entidad.Descuento;
        }

        public bool ProcesarPago(Pagos entidad)
        {
            if (CalcularTotal(entidad) > 0)
            {
                entidad.EstadoPago = "Aprobado";
                entidad.Total = CalcularTotal(entidad);
                this.IConexion!.SaveChanges();
                return true;
            }
            entidad.EstadoPago = "Rechazado";
            this.IConexion!.SaveChanges();
            return false;
        }
    }
}
