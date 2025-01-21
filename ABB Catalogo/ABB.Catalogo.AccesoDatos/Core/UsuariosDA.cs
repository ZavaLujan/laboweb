using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.Utiles.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABB.Catalogo.AccesoDatos.Core
{
    public class UsuariosDA
    {
        private readonly string _connectionString;

        public UsuariosDA()
        {
            _connectionString = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString;
        }

        public List<Usuario> ListarUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                using (SqlCommand comando = new SqlCommand("ListarUsuarios", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    conexion.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuarios.Add(LlenarEntidad(reader));
                        }
                    }
                }
            }

            return usuarios;
        }

        public Usuario BuscaUsuarioId(int pUsuarioId)
        {
            Usuario usuario = null;

            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                using (SqlCommand comando = new SqlCommand("paUsuario_BuscaUserId", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@ParamUsuario", pUsuarioId);
                    conexion.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = LlenarEntidad(reader);
                        }
                    }
                }
            }

            return usuario;
        }

        public int GetUsuarioId(string pUsuario, string pPassword)
        {
            byte[] UserPass = EncriptacionHelper.EncriptarByte(pPassword);

            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                using (SqlCommand comando = new SqlCommand("paUsuario_BuscaCodUserClave", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@ParamUsuario", pUsuario);
                    comando.Parameters.AddWithValue("@ParamPass", UserPass);
                    conexion.Open();

                    var result = comando.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }

        public Usuario InsertarUsuario(Usuario usuario)
        {
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                using (SqlCommand comando = new SqlCommand("paUsuario_insertar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@CodUsuario", usuario.CodUsuario);
                    comando.Parameters.AddWithValue("@Clave", EncriptacionHelper.EncriptarByte(usuario.ClaveTxt));
                    comando.Parameters.AddWithValue("@Nombres", usuario.Nombres);
                    comando.Parameters.AddWithValue("@IdRol", usuario.IdRol);

                    conexion.Open();
                    usuario.IdUsuario = Convert.ToInt32(comando.ExecuteScalar());
                }
            }

            return usuario;
        }

        public Usuario ModificarUsuario(int id, Usuario usuario)
        {
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                using (SqlCommand comando = new SqlCommand("paUsuario_Modificar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdUsuario", id);
                    comando.Parameters.AddWithValue("@CodUsuario", usuario.CodUsuario);
                    comando.Parameters.AddWithValue("@Clave", EncriptacionHelper.EncriptarByte(usuario.ClaveTxt));
                    comando.Parameters.AddWithValue("@Nombres", usuario.Nombres);
                    comando.Parameters.AddWithValue("@IdRol", usuario.IdRol);

                    conexion.Open();
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return LlenarEntidad(reader);
                        }
                    }
                }
            }

            return null;
        }

        public void EliminarUsuario(int id)
        {
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                using (SqlCommand comando = new SqlCommand("paUsuario_Eliminar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdUsuario", id);
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        private Usuario LlenarEntidad(IDataReader reader)
        {
            Usuario usuario = new Usuario();

            if (!Convert.IsDBNull(reader["IdUsuario"]))
                usuario.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);

            if (!Convert.IsDBNull(reader["CodUsuario"]))
                usuario.CodUsuario = Convert.ToString(reader["CodUsuario"]);

            if (!Convert.IsDBNull(reader["Nombres"]))
                usuario.Nombres = Convert.ToString(reader["Nombres"]);

            if (!Convert.IsDBNull(reader["IdRol"]))
                usuario.IdRol = Convert.ToInt32(reader["IdRol"]);

            if (!Convert.IsDBNull(reader["DesRol"]))
                usuario.DesRol = Convert.ToString(reader["DesRol"]);

            return usuario;
        }
    }
}
