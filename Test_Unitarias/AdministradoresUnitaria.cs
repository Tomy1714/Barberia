using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Interfaces;
using Lib_Negocio.Nucleo;
using Microsoft.EntityFrameworkCore;


namespace Test_Unitarias
{
    [TestClass]
    public class AdministradoresUnitaria
    {
        private IConexion?  iConexion;
        private Administradores?  entidad;

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

            var lista = this.iConexion.Administradores!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No se encontraron registros en Administradores");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad = new Administradores()
            {
                IdEmpleado = 1,
                NombreNegocio = "BarberPro",
                PorcentajeUtilidades = 30,
                UtilidadesUltimoMes = 5000000,
                FechaFundacion = DateTime.Now
            };

            this.iConexion.Administradores!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.IdAdministrador != 0)
                return;
            throw new Exception("No se guardo el registro en Administradores");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad!.NombreNegocio = "Barberia Central";

            var entry = this.iConexion!.Entry<Administradores>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            if (this.entidad.IdAdministrador != 0)
                return;
            throw new Exception("No se modifico el registro en Administradores");
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Administradores!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
