using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Interfaces;
using Lib_Negocio.Nucleo;
using Microsoft.EntityFrameworkCore;


namespace Test_Unitarias
{
    [TestClass]
    public class RecepcionistasUnitaria
    {
        private IConexion?  iConexion;
        private Recepcionistas?  entidad;

        [TestMethod]
        public void Ejecutar()
        {
            Guardar();
            Consultar();
            Modificar();
            Borrar();
        }

        private void Consultar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var lista = this.iConexion.Recepcionistas!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No se encontraron registros en Recepcionistas");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad = new Recepcionistas()
            {
                IdEmpleado = 1,
                IdSede = 1,
                TurnoAsignado = "Manana",
                CitasGestionadasMes = 0,
                BonoPorMeta = 150000
            };

            this.iConexion.Recepcionistas!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.IdRecepcionista != 0)
                return;
            throw new Exception("No se guardo el registro en Recepcionistas");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad!.TurnoAsignado = "Tarde";

            var entry = this.iConexion!.Entry<Recepcionistas>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            if (this.entidad.IdRecepcionista != 0)
                return;
            throw new Exception("No se modifico el registro en Recepcionistas");
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Recepcionistas!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
