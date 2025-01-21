using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABB.Catalogo.Entidades.Core;

namespace ABB.Catalogo.AccesoDatos.Core
{
    public class RolDA
    {
        public List<Rol> ListarRoles()
        {
            List<Rol> roles = new List<Rol>();

            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString))
            {
                using (SqlCommand comando = new SqlCommand("ListarRol", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    conexion.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            roles.Add(new Rol
                            {
                                IdRol = Convert.ToInt32(reader["IdRol"]),
                                DesRol = Convert.ToString(reader["DesRol"])
                            });
                        }
                    }
                }
            }

            return roles;
        }
    }
}
