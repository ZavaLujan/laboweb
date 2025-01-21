using ABB.Catalogo.AccesoDatos.Core;
using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.LogicaNegocio.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    public class UsuariosController : Controller
    {
        string RutaApi = "https://localhost:44380/api/"; //define la ruta del web api
        string jsonMediaType = "application/json"; // define el tipo de dat

        // GET: Usuarios
        public ActionResult Index()
        {
            string controladora = "Usuarios";
            string metodo = "Get";
            List<Usuario> listausuarios = new List<Usuario>();
            using (WebClient usuario = new WebClient())
            {
                usuario.Headers.Clear();//borra datos anteriores
                                        //establece el tipo de dato de tranferencia
                usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                //typo de decodificador reconocimiento carecteres especiales
                usuario.Encoding = UTF8Encoding.UTF8;
                string rutacompleta = RutaApi + controladora;
                //ejecuta la busqueda en la web api usando metodo GET
                var data = usuario.DownloadString(new Uri(rutacompleta));
                // convierte los datos traidos por la api a tipo lista de usuarios
                listausuarios = JsonConvert.DeserializeObject<List<Usuario>>(data);
            }
            return View(listausuarios);
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            Usuario usuario = new Usuario();// se crea una instancia de la clase usuario
            List<Rol> listarol = new List<Rol>();
            listarol = new RolLN().ListarRoles();
            listarol.Add(new Rol() { IdRol = 0, DesRol = "[Seleccione Rol...]" });
            ViewBag.listaRoles = listarol;
            return View(usuario);
        }

        // POST: Usuarios/Create
        [System.Web.Mvc.HttpPost]
        public ActionResult Create(Usuario collection)
        {
            string controladora = "Usuarios";
            try
            {
                // TODO: Add insert logic here
                using (WebClient usuario = new WebClient())
                {
                    usuario.Headers.Clear();//borra datos anteriores
                                            //establece el tipo de dato de tranferencia
                    usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                    //typo de decodificador reconocimiento carecteres especiales
                    usuario.Encoding = UTF8Encoding.UTF8;
                    //convierte el objeto de tipo Usuarios a una trama Json
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
            using (WebClient usuario = new WebClient())
            {
                usuario.Headers.Clear();
                usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                usuario.Encoding = UTF8Encoding.UTF8;
                // Modificamos la ruta para usar el endpoint correcto según la API
                string rutacompleta = RutaApi + controladora + "/getbyid/" + id;
                //ejecuta la busqueda en la web api usando metodo GET
                var data = usuario.DownloadString(new Uri(rutacompleta));
                // Deserializamos a un único objeto Usuario
                users = JsonConvert.DeserializeObject<Usuario>(data);
            }

            // Cargar lista de roles para el dropdown
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
            string jsonMediaType = "application/json";
    
            using (WebClient client = new WebClient())
            {
                try
                {
                    // Asegurarse que los campos requeridos no son null
                    if (string.IsNullOrEmpty(usuario.ClaveTxt))
                    {
                        usuario.ClaveTxt = ""; // O asignar un valor por defecto
                    }

                    client.Headers.Clear();
                    client.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                    client.Encoding = UTF8Encoding.UTF8;

                    string rutacompleta = RutaApi + controladora + "/" + id;
                    string jsonUsuario = JsonConvert.SerializeObject(usuario);
            
                    // Para debug: ver qué estamos enviando
                    System.Diagnostics.Debug.WriteLine("JSON enviado: " + jsonUsuario);
            
                    client.UploadString(new Uri(rutacompleta), "PUT", jsonUsuario);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Para debug: ver el error completo
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
            using (WebClient client = new WebClient())
            {
                client.Headers.Clear();
                client.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
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
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Headers.Clear();
                    client.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                    client.Encoding = UTF8Encoding.UTF8;
                    string rutacompleta = RutaApi + controladora + "/" + id;
                    // Usando el método DELETE de HTTP
                    client.UploadString(new Uri(rutacompleta), "DELETE", "");
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Para debug: ver el error completo
                System.Diagnostics.Debug.WriteLine("Error: " + ex.ToString());
                return View();
            }
        }
    }
}
