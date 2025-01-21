using ABB.Catalogo.Entidades.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ABB.Catalogo.ClienteWeb.Controllers
{
    public class ProductoController : Controller
    {
        string RutaApi = "https://localhost:44380/api/"; // URL base de la API
        string jsonMediaType = "application/json";

        // GET: Producto
        public ActionResult Index()
        {
            string controladora = "productos";
            List<Producto> listaproducto = new List<Producto>();
            using (WebClient producto = new WebClient())
            {
                producto.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
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
            using (WebClient cliente = new WebClient())
            {
                cliente.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
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
            // Obtener las categorías de la API
            List<Categoria> categorias = null;
            using (WebClient cliente = new WebClient())
            {
                cliente.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                string rutacompleta = RutaApi + "categorias";
                string respuesta = cliente.DownloadString(rutacompleta);
                categorias = JsonConvert.DeserializeObject<List<Categoria>>(respuesta);
            }

            // Preparar los datos para el combobox
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
                List<Categoria> categorias = null;
                using (WebClient cliente = new WebClient())
                {
                    cliente.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
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
            string controladora = $"productos/{id}";
            Producto producto = null;
            using (WebClient cliente = new WebClient())
            {
                cliente.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                cliente.Encoding = UTF8Encoding.UTF8;
                string rutacompleta = RutaApi + controladora;
                var data = cliente.DownloadString(new Uri(rutacompleta));
                producto = JsonConvert.DeserializeObject<Producto>(data);
            }
            return View(producto);
        }

        // POST: Producto/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Producto producto)
        {
            try
            {
                string controladora = $"productos/{id}";
                using (WebClient cliente = new WebClient())
                {
                    cliente.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                    cliente.Encoding = UTF8Encoding.UTF8;
                    string rutacompleta = RutaApi + controladora;
                    string data = JsonConvert.SerializeObject(producto);
                    cliente.UploadString(new Uri(rutacompleta), "PUT", data);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int id)
        {
            string controladora = $"productos/{id}";
            Producto producto = null;
            using (WebClient cliente = new WebClient())
            {
                cliente.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
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
                string controladora = $"productos/{id}";
                using (WebClient cliente = new WebClient())
                {
                    cliente.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
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
    }
}
