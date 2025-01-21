using ABB.Catalogo.Entidades.Core;
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
    public class ProductoDA
    {
        private readonly string _connectionString;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(ProductoDA));

        public ProductoDA()
        {
            _connectionString = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString;
        }
        public Producto LlenarEntidad(IDataReader reader)
        {
            Producto producto = new Producto();

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName = 'IdProducto'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["IdProducto"]))
                    producto.IdProducto = Convert.ToInt32(reader["IdProducto"]);
            }

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName = 'IdCategoria'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["IdCategoria"]))
                    producto.IdCategoria = Convert.ToInt32(reader["IdCategoria"]);
            }

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName = 'NomProducto'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["NomProducto"]))
                    producto.NomProducto = Convert.ToString(reader["NomProducto"]);
            }

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName = 'MarcaProducto'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["MarcaProducto"]))
                    producto.MarcaProducto = Convert.ToString(reader["MarcaProducto"]);
            }

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName = 'ModeloProducto'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["ModeloProducto"]))
                    producto.ModeloProducto = Convert.ToString(reader["ModeloProducto"]);
            }

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName = 'LineaProducto'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["LineaProducto"]))
                    producto.LineaProducto = Convert.ToString(reader["LineaProducto"]);
            }

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName = 'GarantiaProducto'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["GarantiaProducto"]))
                    producto.GarantiaProducto = Convert.ToString(reader["GarantiaProducto"]);
            }

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName = 'Precio'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["Precio"]))
                    producto.Precio = Convert.ToDecimal(reader["Precio"]);
            }

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName = 'Imagen'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["Imagen"]))
                    producto.Imagen = (byte[])reader["Imagen"];
            }

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName = 'DescripcionTecnica'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["DescripcionTecnica"]))
                    producto.DescripcionTecnica = Convert.ToString(reader["DescripcionTecnica"]);
            }

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName = 'DescCategoria'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["DescCategoria"]))
                    producto.DescCategoria = Convert.ToString(reader["DescCategoria"]);
            }

            return producto;
        }

        public List<Producto> ListarProductos()
        {
            List<Producto> ListaEntidad = new List<Producto>();

            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                using (SqlCommand comando = new SqlCommand("paListarProductos", conexion))
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

        public Producto ObtenerProducto(int id)
        {
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                using (SqlCommand comando = new SqlCommand("paObtenerProducto", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdProducto", id);
                    conexion.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                            return LlenarEntidad(reader);
                    }
                }
            }
            return null;
        }

        public bool InsertarProducto(Producto producto)
        {
            try
            {
                // Validaciones básicas antes de intentar la inserción
                if (producto == null)
                    throw new ArgumentNullException(nameof(producto), "El producto no puede ser null");

                if (string.IsNullOrEmpty(producto.NomProducto))
                    throw new ArgumentException("El nombre del producto es requerido");

                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    using (SqlCommand comando = new SqlCommand("paInsertarProducto", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros con validación y tipos específicos
                        comando.Parameters.Add("@IdCategoria", SqlDbType.Int).Value = producto.IdCategoria;
                        comando.Parameters.Add("@NomProducto", SqlDbType.NVarChar).Value = producto.NomProducto;
                        comando.Parameters.Add("@MarcaProducto", SqlDbType.NVarChar).Value = (object)producto.MarcaProducto ?? DBNull.Value;
                        comando.Parameters.Add("@ModeloProducto", SqlDbType.NVarChar).Value = (object)producto.ModeloProducto ?? DBNull.Value;
                        comando.Parameters.Add("@LineaProducto", SqlDbType.NVarChar).Value = (object)producto.LineaProducto ?? DBNull.Value;
                        comando.Parameters.Add("@GarantiaProducto", SqlDbType.NVarChar).Value = (object)producto.GarantiaProducto ?? DBNull.Value;
                        comando.Parameters.Add("@Precio", SqlDbType.Decimal).Value = (object)producto.Precio ?? DBNull.Value;
                        comando.Parameters.Add("@Imagen", SqlDbType.Image).Value = (object)producto.Imagen ?? DBNull.Value;
                        comando.Parameters.Add("@DescripcionTecnica", SqlDbType.NVarChar).Value = (object)producto.DescripcionTecnica ?? DBNull.Value;

                        // Agregar parámetro de retorno para obtener el resultado del SP
                        SqlParameter returnParameter = comando.Parameters.Add("@ReturnValue", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        SqlParameter idProductoParameter = new SqlParameter("@IdProducto", SqlDbType.Int);
                        idProductoParameter.Direction = ParameterDirection.Output;
                        comando.Parameters.Add(idProductoParameter);

                        conexion.Open();
                        comando.ExecuteNonQuery();

                        // Verificar el valor de retorno del SP
                        int returnValue = (int)returnParameter.Value;

                        if (returnValue >= 0)
                        {
                            producto.IdProducto = (int)idProductoParameter.Value;
                            log.Info($"Producto insertado exitosamente. ID: {producto.IdProducto}");
                            return true;
                        }

                        // Log del error según el código de retorno
                        string errorMessage;
                        switch (returnValue)
                        {
                            case -1:
                                errorMessage = "Error: Categoría no existe";
                                break;
                            case -2:
                                errorMessage = "Error: Nombre de producto duplicado";
                                break;
                            default:
                                errorMessage = $"Error desconocido al insertar producto. Código: {returnValue}";
                                break;
                        }

                        log.Error(errorMessage);
                        return false;
                    }
                }
            }
            catch (SqlException ex)
            {
                log.Error($"Error SQL al insertar producto: {ex.Message}. Número de error: {ex.Number}", ex);
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"Error general al insertar producto: {ex.Message}", ex);
                throw;
            }
        }

        public bool ActualizarProducto(Producto producto)
        {
            try
            {
                // Validaciones básicas
                if (producto == null)
                    throw new ArgumentNullException(nameof(producto), "El producto no puede ser null");

                if (producto.IdProducto <= 0)
                    throw new ArgumentException("El ID del producto debe ser mayor que 0");

                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    // Primero verificamos si el producto existe
                    using (SqlCommand comandoVerificar = new SqlCommand("SELECT COUNT(1) FROM Producto WHERE IdProducto = @IdProducto", conexion))
                    {
                        comandoVerificar.Parameters.Add("@IdProducto", SqlDbType.Int).Value = producto.IdProducto;
                        conexion.Open();
                        int existe = (int)comandoVerificar.ExecuteScalar();

                        if (existe == 0)
                            throw new Exception($"No se encontró el producto con ID {producto.IdProducto}");
                    }

                    // Si existe, procedemos a actualizar
                    using (SqlCommand comando = new SqlCommand("paActualizarProducto", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros con tipos específicos
                        comando.Parameters.Add("@IdProducto", SqlDbType.Int).Value = producto.IdProducto;
                        comando.Parameters.Add("@IdCategoria", SqlDbType.Int).Value = producto.IdCategoria;
                        comando.Parameters.Add("@NomProducto", SqlDbType.NVarChar).Value = producto.NomProducto;
                        comando.Parameters.Add("@MarcaProducto", SqlDbType.NVarChar).Value = (object)producto.MarcaProducto ?? DBNull.Value;
                        comando.Parameters.Add("@ModeloProducto", SqlDbType.NVarChar).Value = (object)producto.ModeloProducto ?? DBNull.Value;
                        comando.Parameters.Add("@LineaProducto", SqlDbType.NVarChar).Value = (object)producto.LineaProducto ?? DBNull.Value;
                        comando.Parameters.Add("@GarantiaProducto", SqlDbType.NVarChar).Value = (object)producto.GarantiaProducto ?? DBNull.Value;
                        comando.Parameters.Add("@Precio", SqlDbType.Decimal).Value = (object)producto.Precio ?? DBNull.Value;
                        comando.Parameters.Add("@Imagen", SqlDbType.Image).Value = (object)producto.Imagen ?? DBNull.Value;
                        comando.Parameters.Add("@DescripcionTecnica", SqlDbType.NVarChar).Value = (object)producto.DescripcionTecnica ?? DBNull.Value;

                        // Agregar parámetro de retorno para capturar el resultado
                        SqlParameter returnParameter = comando.Parameters.Add("@ReturnValue", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        int result = comando.ExecuteNonQuery();
                        int returnValue = (int)returnParameter.Value;

                        if (returnValue >= 0)
                        {
                            log.Info($"Producto actualizado exitosamente. ID: {producto.IdProducto}");
                            return true;
                        }

                        // Log del error según el código de retorno
                        string errorMessage;
                        switch (returnValue)
                        {
                            case -1:
                                errorMessage = "Error: Categoría no existe";
                                break;
                            case -2:
                                errorMessage = "Error: Nombre de producto duplicado";
                                break;
                            default:
                                errorMessage = $"Error desconocido al actualizar producto. Código: {returnValue}";
                                break;
                        }

                        log.Error(errorMessage);
                        return false;
                    }
                }
            }
            catch (SqlException ex)
            {
                log.Error($"Error SQL al actualizar producto: {ex.Message}. Número de error: {ex.Number}", ex);
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"Error general al actualizar producto: {ex.Message}", ex);
                throw;
            }
        }

        public bool EliminarProducto(int id)
        {
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                using (SqlCommand comando = new SqlCommand("paEliminarProducto", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdProducto", id);

                    SqlParameter returnParameter = comando.Parameters.Add("@ReturnVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    conexion.Open();
                    comando.ExecuteNonQuery();

                    return (int)returnParameter.Value == 1;
                }
            }
        }
    }
}
