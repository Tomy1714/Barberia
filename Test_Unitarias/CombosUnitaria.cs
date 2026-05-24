using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Interfaces;
using Lib_Negocio.Nucleo;
using Microsoft.EntityFrameworkCore;


namespace Test_Unitarias
{
    [TestClass]
    public class CombosUnitaria
    {
        private IConexion?  iConexion;
        private Combos?  entidad;

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

            var lista = this.iConexion.Combos!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No se encontraron registros en Combos");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad = new Combos()
            {
                IdServicio = 1,
                DescuentoCombo = 10,
                Descripcion = "Paquete completo"
            };

            this.iConexion.Combos!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.IdCombo != 0)
                return;
            throw new Exception("No se guardo el registro en Combos");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad!.DescuentoCombo = 15;

            var entry = this.iConexion!.Entry<Combos>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            if (this.entidad.IdCombo != 0)
                return;
            throw new Exception("No se modifico el registro en Combos");
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Combos!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
