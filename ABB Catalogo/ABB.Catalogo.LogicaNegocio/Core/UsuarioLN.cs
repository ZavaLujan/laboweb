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
        private static readonly ILog Log = LogManager.GetLogger(typeof(UsuariosLN));
        private readonly UsuariosDA _usuariosDA;

        public UsuariosLN()
        {
            _usuariosDA = new UsuariosDA();
        }

        public List<Usuario> ListarUsuarios()
        {
            try
            {
                return _usuariosDA.ListarUsuarios();
            }
            catch (Exception ex)
            {
                Log.Error("Error al listar usuarios", ex);
                throw;
            }
        }

        public Usuario BuscaUsuarioId(int pUsuarioId)
        {
            try
            {
                var usuario = _usuariosDA.BuscaUsuarioId(pUsuarioId);
                if (usuario == null)
                    throw new Exception($"Usuario con ID {pUsuarioId} no encontrado");
                return usuario;
            }
            catch (Exception ex)
            {
                Log.Error($"Error al buscar usuario con ID {pUsuarioId}", ex);
                throw;
            }
        }

        public int GetUsuarioId(string pUsuario, string pPassword)
        {
            try
            {
                if (string.IsNullOrEmpty(pUsuario) || string.IsNullOrEmpty(pPassword))
                    return -1;

                return _usuariosDA.GetUsuarioId(pUsuario, pPassword);
            }
            catch (Exception ex)
            {
                Log.Error($"Error al validar usuario {pUsuario}", ex);
                return -1;
            }
        }

        public Usuario InsertarUsuario(Usuario usuario)
        {
            try
            {
                if (ValidarUsuario(usuario))
                    return _usuariosDA.InsertarUsuario(usuario);
                throw new Exception("Datos de usuario inválidos");
            }
            catch (Exception ex)
            {
                Log.Error("Error al insertar usuario", ex);
                throw;
            }
        }

        public Usuario ModificarUsuario(int id, Usuario usuario)
        {
            try
            {
                if (id != usuario.IdUsuario)
                    throw new Exception("ID de usuario no coincide");

                if (ValidarUsuario(usuario))
                    return _usuariosDA.ModificarUsuario(id, usuario);
                throw new Exception("Datos de usuario inválidos");
            }
            catch (Exception ex)
            {
                Log.Error($"Error al modificar usuario con ID {id}", ex);
                throw;
            }
        }

        public void EliminarUsuario(int id)
        {
            try
            {
                _usuariosDA.EliminarUsuario(id);
            }
            catch (Exception ex)
            {
                Log.Error($"Error al eliminar usuario con ID {id}", ex);
                throw;
            }
        }

        private bool ValidarUsuario(Usuario usuario)
        {
            return !string.IsNullOrEmpty(usuario.CodUsuario) &&
                   !string.IsNullOrEmpty(usuario.Nombres) &&
                   usuario.IdRol > 0;
        }

        public Usuario BuscarUsuario(Usuario Usuario)
        {
            try
            {
                return new UsuariosDA().BuscarUsuario(Usuario);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

    }
}
