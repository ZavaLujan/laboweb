using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.LogicaNegocio.Core;
using log4net;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebServicesAbb.Controllers
{
    [Authorize]
    public class ProductosController : ApiController
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ProductosController));
        private readonly ProductoLN _productoLN;

        public ProductosController()
        {
            _productoLN = new ProductoLN();
        }

        // GET: api/Productos
        public IHttpActionResult Get()
        {
            try
            {
                List<Producto> productos = _productoLN.ListarProductos();
                if (productos == null || productos.Count == 0)
                    return NotFound();

                return Ok(productos);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Productos/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                Producto producto = _productoLN.ObtenerProducto(id);
                if (producto == null)
                    return NotFound();

                return Ok(producto);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Productos
        public IHttpActionResult Post([FromBody] Producto producto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var (success, message) = _productoLN.InsertarProducto(producto);
                if (!success)
                    return BadRequest(message);

                return Created($"api/productos/{producto.IdProducto}", producto);
            }
            catch (Exception ex)
            {
                Logger.Error($"Error al procesar la solicitud POST: {ex.Message}", ex);
                return InternalServerError(ex);
            }
        }

        // PUT: api/Productos/5
        public IHttpActionResult Put(int id, [FromBody] Producto producto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (id != producto.IdProducto)
                    return BadRequest("El ID del producto no coincide");

                bool resultado = _productoLN.ActualizarProducto(producto);
                if (!resultado)
                    return BadRequest("No se pudo actualizar el producto. Verifica que exista y que los datos sean correctos.");

                return Ok(producto);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Productos/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                bool resultado = _productoLN.EliminarProducto(id);
                if (!resultado)
                    return NotFound();

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}