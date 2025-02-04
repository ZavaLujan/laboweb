using ABB.Catalogo.AccesoDatos.Core;
using ABB.Catalogo.Entidades.Base;
using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.LogicaNegocio.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using ActionNameAttribute = System.Web.Mvc.ActionNameAttribute;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;

namespace ABB.Catalogo.ClienteWeb.Controllers
{
    public class UsuariosController : BaseLN
    {
        string RutaApi = "http://localhost/webapiabb/api/";
        string jsonMediaType = "application/json";

        // GET: Usuarios
        public ActionResult Index()
        {
            string controladora = "Usuarios";
            List<Usuario> listausuarios = new List<Usuario>();

            // Obtener el token
            var tokenResponse = Respuesta();

            using (WebClient usuario = new WebClient())
            {
                usuario.Headers.Clear();
                usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                // Agregar el token al header
                usuario.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenResponse.Token;
                usuario.Encoding = UTF8Encoding.UTF8;

                string rutacompleta = RutaApi + controladora;
                var data = usuario.DownloadString(new Uri(rutacompleta));
                listausuarios = JsonConvert.DeserializeObject<List<Usuario>>(data);
            }
            return View(listausuarios);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            // Obtener el token (aunque no se use en esta acción, podría necesitarse para cargar datos adicionales)
            var tokenResponse = Respuesta();

            Usuario usuario = new Usuario();
            List<Rol> listarol = new List<Rol>();
            listarol = new RolLN().ListarRoles();
            listarol.Add(new Rol() { IdRol = 0, DesRol = "[Seleccione Rol...]" });
            ViewBag.listaRoles = listarol;
            return View(usuario);
        }

        // POST: Usuarios/Create
        [HttpPost]
        public ActionResult Create(Usuario collection)
        {
            string controladora = "Usuarios";
            try
            {
                // Obtener el token
                var tokenResponse = Respuesta();

                using (WebClient usuario = new WebClient())
                {
                    usuario.Headers.Clear();
                    usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                    // Agregar el token al header
                    usuario.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenResponse.Token;
                    usuario.Encoding = UTF8Encoding.UTF8;

                    var usuarioJson = JsonConvert.SerializeObject(collection);
                    string rutacompleta = RutaApi + controladora;
                    var resultado = usuario.UploadString(new Uri(rutacompleta), usuarioJson);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int id)
        {
            string controladora = "Usuarios";
            Usuario users = new Usuario();

            // Obtener el token
            var tokenResponse = Respuesta();

            using (WebClient usuario = new WebClient())
            {
                usuario.Headers.Clear();
                usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                // Agregar el token al header
                usuario.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenResponse.Token;
                usuario.Encoding = UTF8Encoding.UTF8;

                string rutacompleta = RutaApi + controladora + "/getbyid/" + id;
                var data = usuario.DownloadString(new Uri(rutacompleta));
                users = JsonConvert.DeserializeObject<Usuario>(data);
            }

            List<Rol> listarol = new List<Rol>();
            listarol = new RolLN().ListarRoles();
            ViewBag.listaRoles = listarol;

            return View(users);
        }

        // POST: Usuarios/Edit/12
        [HttpPost]
        public ActionResult Edit(int id, Usuario usuario)
        {
            string controladora = "Usuarios";

            // Obtener el token
            var tokenResponse = Respuesta();

            using (WebClient client = new WebClient())
            {
                try
                {
                    if (string.IsNullOrEmpty(usuario.ClaveTxt))
                    {
                        usuario.ClaveTxt = "";
                    }

                    client.Headers.Clear();
                    client.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                    // Agregar el token al header
                    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenResponse.Token;
                    client.Encoding = UTF8Encoding.UTF8;

                    string rutacompleta = RutaApi + controladora + "/" + id;
                    string jsonUsuario = JsonConvert.SerializeObject(usuario);

                    System.Diagnostics.Debug.WriteLine("JSON enviado: " + jsonUsuario);
                    client.UploadString(new Uri(rutacompleta), "PUT", jsonUsuario);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error: " + ex.ToString());
                    return View(usuario);
                }
            }
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int id)
        {
            string controladora = "Usuarios";
            Usuario usuario = new Usuario();

            // Obtener el token
            var tokenResponse = Respuesta();

            using (WebClient client = new WebClient())
            {
                client.Headers.Clear();
                client.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                // Agregar el token al header
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenResponse.Token;
                client.Encoding = UTF8Encoding.UTF8;

                string rutacompleta = RutaApi + controladora + "/getbyid/" + id;
                var data = client.DownloadString(new Uri(rutacompleta));
                usuario = JsonConvert.DeserializeObject<Usuario>(data);
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string controladora = "Usuarios";

            // Obtener el token
            var tokenResponse = Respuesta();

            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Headers.Clear();
                    client.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                    // Agregar el token al header
                    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenResponse.Token;
                    client.Encoding = UTF8Encoding.UTF8;

                    string rutacompleta = RutaApi + controladora + "/" + id;
                    client.UploadString(new Uri(rutacompleta), "DELETE", "");
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.ToString());
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