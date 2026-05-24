using Lib_Negocio.Entidades;
using Lib_Negocio.Interfaces;

namespace Lib_Negocio.Implementacion
{
    public class UsuariosNegocio : IUsuariosNegocio
    {
        private IConexion? IConexion = null;

        public UsuariosNegocio(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public List<Usuarios> Listar()
        {
            return this.IConexion!.Usuarios
                .Take(50).ToList();
        }

        public Usuarios? Guardar(Usuarios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdUsuario != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Usuarios.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Usuarios? Modificar(Usuarios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdUsuario == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Usuarios.Update(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Usuarios? Borrar(Usuarios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdUsuario == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Usuarios.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        // Verifica email y contrasena, retorna el usuario si es valido
        public Usuarios? Login(string email, string contrasena)
        {
            return this.IConexion!.Usuarios
                .FirstOrDefault(u => u.Email      == email
                                  && u.Contrasena == contrasena
                                  && u.Activo     == true);
        }
    }
}
