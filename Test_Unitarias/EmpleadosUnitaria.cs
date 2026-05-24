using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Interfaces;
using Lib_Negocio.Nucleo;
using Microsoft.EntityFrameworkCore;


namespace Test_Unitarias
{
    [TestClass]
    public class EmpleadosUnitaria
    {
        private IConexion?  iConexion;
        private Empleados?  entidad;

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

            var lista = this.iConexion.Empleados!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No se encontraron registros en Empleados");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad = new Empleados()
            {
                IdPersona = 1,
                Cargo = "UT-Barbero",
                SalarioBase = 1200000,
                Activo = true,
                FechaIngreso = DateTime.Now
            };

            this.iConexion.Empleados!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.IdEmpleado != 0)
                return;
            throw new Exception("No se guardo el registro en Empleados");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad!.Cargo = "Recepcionista";

            var entry = this.iConexion!.Entry<Empleados>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            if (this.entidad.IdEmpleado != 0)
                return;
            throw new Exception("No se modifico el registro en Empleados");
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Empleados!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
