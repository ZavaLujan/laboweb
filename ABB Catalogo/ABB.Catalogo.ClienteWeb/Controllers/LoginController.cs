using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.Utiles.Helpers;
using System.Web.Security;
using ABB.Catalogo.LogicaNegocio.Core;
using ABB.Catalogo.Entidades;
using ABB.Catalogo.Entidades.Base;

namespace ABB.Catalogo.ClienteWeb.Controllers
{
    public class LoginController : BaseLN
    {
        // GET: Login
        public ActionResult Index()
        {
            Usuario u = new Usuario();
            return View(u);
        }

        [HttpPost]
        public ActionResult Index(Usuario usuario)
        {
            Log.Info($"Intento de inicio de sesión - Usuario: {usuario.CodUsuario}");

            if (string.IsNullOrEmpty(usuario.CodUsuario) || string.IsNullOrEmpty(usuario.ClaveTxt))
            {
                Log.Warn("Intento de inicio de sesión con usuario o clave vacíos");
                ModelState.AddModelError("*", "Debe llenar el usuario o clave");
                return View(usuario);
            }

            try
            {
                // Encriptar la contraseña
                usuario.Clave = EncriptacionHelper.EncriptarByte(usuario.ClaveTxt);

                Usuario res = new UsuariosLN().BuscarUsuario(usuario);

                if (res != null)
                {
                    if (res.IdRol > 0)  // Verificar que tenga un rol asignado
                    {
                        Log.Info($"Inicio de sesión exitoso para usuario: {res.CodUsuario}");

                        // Autenticación de Forms
                        FormsAuthentication.SetAuthCookie(res.CodUsuario, true);

                        // Establecer variables de sesión si es necesario
                        VariablesWeb.gUsuario = res;

                        // Cargar opciones para el usuario
                        List<Opcion> lista = new OpcionLN().ListaOpciones();
                        ParsearAcciones(lista);
                        VariablesWeb.gOpciones = lista;

                        // Redireccionamiento explícito
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        Log.Warn($"Usuario {res.CodUsuario} no tiene rol asignado");
                        ModelState.AddModelError("*", "El usuario no tiene un rol asignado");
                    }
                }
                else
                {
                    Log.Warn($"Credenciales inválidas para usuario: {usuario.CodUsuario}");
                    ModelState.AddModelError("*", "Usuario / Clave no válidos");
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Error durante el inicio de sesión para usuario {usuario.CodUsuario}", ex);
                ModelState.AddModelError("*", "Ocurrió un error durante el inicio de sesión");
            }

            // Si no se redirige, vuelve a mostrar la vista de login
            return View(usuario);
        }

        [NonAction]
        private void ParsearAcciones(List<Opcion> lista)
        {

            int cantidad = 0;
            foreach (Opcion item in lista)
            {
                if (!string.IsNullOrEmpty(item.UrlOpcion))
                {
                    cantidad = item.UrlOpcion.Split('/').Count();
                    switch (cantidad)
                    {
                        case 3:
                            item.Area = item.UrlOpcion.Split('/')[0];
                            item.Controladora = item.UrlOpcion.Split('/')[1];
                            item.Accion = item.UrlOpcion.Split('/')[2];
                            break;
                        case 2:
                            item.Controladora = item.UrlOpcion.Split('/')[0];
                            item.Accion = item.UrlOpcion.Split('/')[1];
                            break;
                        case 1:
                            item.Controladora = item.UrlOpcion.Split('/')[0];
                            item.Accion = "Index";
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
