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
        public List<Rol> ListaRol()
        {
            List<Rol> listaEntidad = new List<Rol>();
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString))
                {
                    using (SqlCommand comando = new SqlCommand("ListarRol", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        conexion.Open();
                        SqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            Rol entidad = new Rol();
                            entidad.IdRol = Convert.ToInt32(reader["IdRol"]);
                            entidad.DesRol = Convert.ToString(reader["DesRol"]);
                            listaEntidad.Add(entidad);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Aquí podrías agregar logging si lo necesitas
            }
            return listaEntidad;
        }
    }
}
