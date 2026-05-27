using Lib_Negocio.Entidades;
using Lib_Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio.Implementacion
{
    public class PromocionesNegocio : IPromocionesNegocio
    {
        private Conexion? IConexion = null;

        public PromocionesNegocio(Conexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.Database.GetConnectionString();
        }

        public List<Promociones> Listar()
        {
            return this.IConexion!.Promociones
                .Include(e => e.Servicio)
                .Take(50).ToList();
        }

        public List<PromocionesAuditoria> ListarAuditoria()
        {
            return this.IConexion!.PromocionesAuditoria
                .Take(50).ToList();
        }


        public Promociones? Guardar(Promociones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdPromocion != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Promociones.Add(entidad);
            this.IConexion.SaveChanges();

            this.IConexion!.PromocionesAuditoria.Add(new PromocionesAuditoria
            {
                IdPromocion = entidad.IdPromocion,
                Accion = "Insertar",
                Fecha = DateTime.Now
            });
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Promociones? Modificar(Promociones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdPromocion == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Promociones.Update(entidad);
            this.IConexion.SaveChanges();

            this.IConexion!.PromocionesAuditoria.Add(new PromocionesAuditoria
            {
                IdPromocion = entidad.IdPromocion,
                Accion = "Modificar",
                Fecha = DateTime.Now
            });
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Promociones? Borrar(Promociones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdPromocion == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Promociones.Remove(entidad);
            this.IConexion.SaveChanges();

            this.IConexion!.PromocionesAuditoria.Add(new PromocionesAuditoria
            {
                IdPromocion = entidad.IdPromocion,
                Accion = "Borrar",
                Fecha = DateTime.Now
            });
            this.IConexion.SaveChanges();
            return entidad;
        }

        public decimal CalcularDescuento(Promociones entidad, decimal precio)
        {
            return precio * (entidad.PorcentajeDescuento / 100);
        }
    }
}
