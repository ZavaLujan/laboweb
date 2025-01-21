using ABB.Catalogo.Entidades.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABB.Catalogo.AccesoDatos.Core
{
    public class CategoriaDA
    {
        private readonly string _connectionString;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(CategoriaDA));

        public CategoriaDA()
        {
            _connectionString = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString;
        }

        private Categoria LlenarEntidad(IDataReader reader)
        {
            Categoria categoria = new Categoria();

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName = 'IdCategoria'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["IdCategoria"]))
                    categoria.IdCategoria = Convert.ToInt32(reader["IdCategoria"]);
            }

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName = 'DescCategoria'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["DescCategoria"]))
                    categoria.DescCategoria = Convert.ToString(reader["DescCategoria"]);
            }

            return categoria;
        }

        public List<Categoria> ListarCategorias()
        {
            List<Categoria> ListaEntidad = new List<Categoria>();

            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                using (SqlCommand comando = new SqlCommand("paListarCategorias", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    conexion.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListaEntidad.Add(LlenarEntidad(reader));
                        }
                    }
                }
            }

            return ListaEntidad;
        }
    }
}
