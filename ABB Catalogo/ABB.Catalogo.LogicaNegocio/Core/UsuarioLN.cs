using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.AccesoDatos.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABB.Catalogo.Entidades.Base;
using log4net;


namespace ABB.Catalogo.LogicaNegocio.Core
{
    public class UsuariosLN
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(UsuariosLN)); // Declaración del logger

        public List<Usuario> ListarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();
            try
            {
                UsuariosDA usuarios = new UsuariosDA();
                return usuarios.ListarUsuarios();
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                //Logger.paginaNombre = this.GetType().Name;
                //Logger.Escribir(&quot;Error en Logica de Negocio: &quot; + ex.Message + &quot;. &quot; + ex.StackTrace + &quot;. &quot; + innerException);
                return lista;
            }
        }

        public int GetUsuarioId(string pUsuario, string pPassword)
        {
            try
            {
                UsuariosDA usuario = new UsuariosDA();
                return usuario.GetUsuarioId(pUsuario, pPassword);
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                //Logger.paginaNombre = this.GetType().Name;
                //Logger.Escribir("Error en Logica de Negocio: " + ex.Message + ". " + ex.StackTrace + ". " + innerException);
                return -1;
            }
        }

        public Usuario InsertarUsuario(Usuario usuario)
        {
            try
            {
                return new UsuariosDA().InsertarUsuario(usuario);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        public Usuario ModificarUsuario(int id, Usuario usuario)
        {
            try
            {
                return new UsuariosDA().ModificarUsuario(id, usuario);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        public Usuario BuscaUsuarioId(int pUsuarioId)
        {
            try
            {
                UsuariosDA usuario = new UsuariosDA();
                return usuario.BuscaUsuarioId(pUsuarioId);
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                //Logger.paginaNombre = this.GetType().Name;
                //Logger.Escribir("Error en Logica de Negocio: " + ex.Message + ". " + ex.StackTrace + ". " + innerException);
                throw;
            }
        }
    }
}
