using ABB.Catalogo.Entidades.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace ABB.Catalogo.ClienteWeb.Controllers
{
    public class ProductoController : Controller
    {
        string RutaApi = "https://localhost:44380/api/";
        string jsonMediaType = "application/json";

        // GET: Producto
        public ActionResult Index()
        {
            string controladora = "productos";
            List<Producto> listaproducto = new List<Producto>();

            // Obtener el token
            var tokenResponse = Respuesta();

            using (WebClient producto = new WebClient())
            {
                producto.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                producto.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenResponse.Token;
                producto.Encoding = UTF8Encoding.UTF8;
                string rutacompleta = RutaApi + controladora;
                var data = producto.DownloadString(new Uri(rutacompleta));
                listaproducto = JsonConvert.DeserializeObject<List<Producto>>(data);
            }
            return View(listaproducto);
        }

        // GET: Producto/Details/5
        public ActionResult Details(int id)
        {
            string controladora = $"productos/{id}";
            Producto producto = null;

            // Obtener el token
            var tokenResponse = Respuesta();

            using (WebClient cliente = new WebClient())
            {
                cliente.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                cliente.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenResponse.Token;
                cliente.Encoding = UTF8Encoding.UTF8;
                string rutacompleta = RutaApi + controladora;
                var data = cliente.DownloadString(new Uri(rutacompleta));
                producto = JsonConvert.DeserializeObject<Producto>(data);
            }
            return View(producto);
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            // Obtener el token
            var tokenResponse = Respuesta();

            // Obtener las categorías de la API
            List<Categoria> categorias = null;
            using (WebClient cliente = new WebClient())
            {
                cliente.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                cliente.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenResponse.Token;
                string rutacompleta = RutaApi + "categorias";
                string respuesta = cliente.DownloadString(rutacompleta);
                categorias = JsonConvert.DeserializeObject<List<Categoria>>(respuesta);
            }

            ViewBag.IdCategoria = new SelectList(categorias, "IdCategoria", "DescCategoria");
            return View();
        }

        // POST: Producto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Producto producto, HttpPostedFileBase imagenFile)
        {
            try
            {
                // Obtener el token
                var tokenResponse = Respuesta();

                // Procesar la imagen si se subió una
                if (imagenFile != null && imagenFile.ContentLength > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        imagenFile.InputStream.CopyTo(ms);
                        producto.Imagen = ms.ToArray();
                    }
                }

                string controladora = "productos";
                using (WebClient cliente = new WebClient())
                {
                    cliente.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                    cliente.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenResponse.Token;
                    cliente.Encoding = UTF8Encoding.UTF8;
                    string rutacompleta = RutaApi + controladora;
                    string data = JsonConvert.SerializeObject(producto);
                    cliente.UploadString(new Uri(rutacompleta), "POST", data);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                // Recargar el combobox en caso de error
                var tokenResponse = Respuesta();
                List<Categoria> categorias = null;
                using (WebClient cliente = new WebClient())
                {
                    cliente.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                    cliente.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenResponse.Token;
                    string rutacompleta = RutaApi + "categorias";
                    string respuesta = cliente.DownloadString(rutacompleta);
                    categorias = JsonConvert.DeserializeObject<List<Categoria>>(respuesta);
                }
                ViewBag.IdCategoria = new SelectList(categorias, "IdCategoria", "DescCategoria");
                return View(producto);
            }
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                // Obtener el token
                var tokenResponse = Respuesta();

                // Obtener el producto de la API
                Producto producto = null;
                using (WebClient cliente = new WebClient())
                {
                    cliente.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                    cliente.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenResponse.Token;
                    string rutacompleta = RutaApi + "productos/" + id;
                    string respuesta = cliente.DownloadString(rutacompleta);
                    producto = JsonConvert.DeserializeObject<Producto>(respuesta);
                }

                // Obtener las categorías de la API
                List<Categoria> categorias = null;
                using (WebClient cliente = new WebClient())
                {
                    cliente.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                    cliente.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenResponse.Token;
                    string rutacompleta = RutaApi + "categorias";
                    string respuesta = cliente.DownloadString(rutacompleta);
                    categorias = JsonConvert.DeserializeObject<List<Categoria>>(respuesta);
                }

                ViewBag.IdCategoria = new SelectList(categorias, "IdCategoria", "DescCategoria", producto.IdCategoria);
                return View(producto);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Producto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Producto producto, HttpPostedFileBase imagenFile)
        {
            try
            {
                if (id != producto.IdProducto)
                {
                    return HttpNotFound();
                }

                // Obtener el token
                var tokenResponse = Respuesta();

                // Si se subió una nueva imagen, procesarla
                if (imagenFile != null && imagenFile.ContentLength > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        imagenFile.InputStream.CopyTo(ms);
                        producto.Imagen = ms.ToArray();
                    }
                }
                else
                {
                    // Si no se subió una nueva imagen, obtener la imagen actual
                    using (WebClient cliente = new WebClient())
                    {
                        cliente.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                        cliente.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenResponse.Token;
                        string rutacompleta = RutaApi + "productos/" + id;
                        string respuesta = cliente.DownloadString(rutacompleta);
                        var productoActual = JsonConvert.DeserializeObject<Producto>(respuesta);
                        producto.Imagen = productoActual.Imagen;
                    }
                }

                // Actualizar el producto
                string controladora = "productos/" + id;
                using (WebClient cliente = new WebClient())
                {
                    cliente.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                    cliente.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenResponse.Token;
                    cliente.Encoding = UTF8Encoding.UTF8;
                    string rutacompleta = RutaApi + controladora;
                    string data = JsonConvert.SerializeObject(producto);
                    cliente.UploadString(new Uri(rutacompleta), "PUT", data);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                // Recargar el combobox en caso de error
                var tokenResponse = Respuesta();
                List<Categoria> categorias = null;
                using (WebClient cliente = new WebClient())
                {
                    cliente.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                    cliente.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenResponse.Token;
                    string rutacompleta = RutaApi + "categorias";
                    string respuesta = cliente.DownloadString(rutacompleta);
                    categorias = JsonConvert.DeserializeObject<List<Categoria>>(respuesta);
                }
                ViewBag.IdCategoria = new SelectList(categorias, "IdCategoria", "DescCategoria", producto.IdCategoria);
                return View(producto);
            }
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int id)
        {
            string controladora = $"productos/{id}";
            Producto producto = null;

            // Obtener el token
            var tokenResponse = Respuesta();

            using (WebClient cliente = new WebClient())
            {
                cliente.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                cliente.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenResponse.Token;
                cliente.Encoding = UTF8Encoding.UTF8;
                string rutacompleta = RutaApi + controladora;
                var data = cliente.DownloadString(new Uri(rutacompleta));
                producto = JsonConvert.DeserializeObject<Producto>(data);
            }
            return View(producto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // Obtener el token
                var tokenResponse = Respuesta();

                string controladora = $"productos/{id}";
                using (WebClient cliente = new WebClient())
                {
                    cliente.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                    cliente.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenResponse.Token;
                    cliente.Encoding = UTF8Encoding.UTF8;
                    string rutacompleta = RutaApi + controladora;
                    cliente.UploadString(new Uri(rutacompleta), "DELETE", "");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private TokenResponse Respuesta()
        {
            TokenResponse respuesta = new TokenResponse();
            string controladora = "Auth";
            var resultado = "";
            UsuariosApi usuapi = new UsuariosApi();

            usuapi.Codigo = Convert.ToInt32(ConfigurationManager.AppSettings["UsuApiCodigo"]);
            usuapi.UserName = ConfigurationManager.AppSettings["UsuApiUserName"];
            usuapi.Clave = ConfigurationManager.AppSettings["UsuApiClave"];
            usuapi.Nombre = ConfigurationManager.AppSettings["UsuApiNombre"];
            usuapi.Rol = ConfigurationManager.AppSettings["UsuApiRol"];

            using (WebClient usuarioapi = new WebClient())
            {
                usuarioapi.Headers.Clear();
                usuarioapi.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                usuarioapi.Encoding = UTF8Encoding.UTF8;
                var usuarioJson = JsonConvert.SerializeObject(usuapi);
                string rutacompleta = RutaApi + controladora;
                resultado = usuarioapi.UploadString(new Uri(rutacompleta), usuarioJson);
                respuesta = JsonConvert.DeserializeObject<TokenResponse>(resultado);
            }
            return respuesta;
        }
    }
}
