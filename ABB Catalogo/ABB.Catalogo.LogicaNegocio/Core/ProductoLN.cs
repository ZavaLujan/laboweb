using ABB.Catalogo.AccesoDatos.Core;
using ABB.Catalogo.Entidades.Core;
using System;
using System.Collections.Generic;

namespace ABB.Catalogo.LogicaNegocio.Core
{
    public class ProductoLN
    {
        private readonly ProductoDA _productoDA;

        public ProductoLN()
        {
            _productoDA = new ProductoDA();
        }

        public List<Producto> ListarProductos()
        {
            try
            {
                return _productoDA.ListarProductos();
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                //Logger.Escribir("Error en Logica de Negocio: " + ex.Message + ". " + ex.StackTrace + ". " + innerException);
                return new List<Producto>();
            }
        }

        public Producto ObtenerProducto(int id)
        {
            try
            {
                return _productoDA.ObtenerProducto(id);
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                //Logger.Escribir("Error en Logica de Negocio: " + ex.Message + ". " + ex.StackTrace + ". " + innerException);
                return null;
            }
        }

        public (bool success, string message) InsertarProducto(Producto producto)
        {
            try
            {
                bool resultado = _productoDA.InsertarProducto(producto);
                if (!resultado)
                {
                    return (false, "No se pudo insertar el producto. Verifica la categoría y que el nombre no esté duplicado.");
                }
                return (true, "Producto insertado correctamente");
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                string errorMessage = $"Error al insertar producto: {ex.Message}";
                //Logger.Escribir(errorMessage + ". " + ex.StackTrace + ". " + innerException);
                return (false, errorMessage);
            }
        }

        public bool ActualizarProducto(Producto producto)
        {
            try
            {
                return _productoDA.ActualizarProducto(producto);
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                //Logger.Escribir("Error en Logica de Negocio: " + ex.Message + ". " + ex.StackTrace + ". " + innerException);
                return false;
            }
        }

        public bool EliminarProducto(int id)
        {
            try
            {
                return _productoDA.EliminarProducto(id);
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                //Logger.Escribir("Error en Logica de Negocio: " + ex.Message + ". " + ex.StackTrace + ". " + innerException);
                return false;
            }
        }
    }
}