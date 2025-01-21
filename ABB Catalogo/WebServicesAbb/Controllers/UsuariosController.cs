using System;
using ABB.Catalogo.Entidades.Core;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ABB.Catalogo.LogicaNegocio.Core;

namespace WebServicesAbb.Controllers
{
    public class UsuariosController : ApiController
    {
        private readonly UsuariosLN _usuariosLN;

        public UsuariosController()
        {
            _usuariosLN = new UsuariosLN();
        }

        // GET: api/Usuarios
        public IEnumerable<Usuario> Get()
        {
            return _usuariosLN.ListarUsuarios();
        }

        // GET: api/Usuarios/GetById/5
        [HttpGet]
        [Route("api/Usuarios/GetById/{id}")]
        public Usuario GetById(int id)
        {
            try
            {
                return _usuariosLN.BuscaUsuarioId(id);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        // GET: api/Usuarios/ValidateUser
        [HttpGet]
        [Route("api/Usuarios/ValidateUser")]
        public int ValidateUser([FromUri] string usuario, [FromUri] string password)
        {
            return _usuariosLN.GetUsuarioId(usuario, password);
        }

        // POST: api/Usuarios
        public Usuario Post([FromBody] Usuario usuario)
        {
            try
            {
                return _usuariosLN.InsertarUsuario(usuario);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        // PUT: api/Usuarios/5
        public Usuario Put(int id, [FromBody] Usuario usuario)
        {
            try
            {
                return _usuariosLN.ModificarUsuario(id, usuario);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        // DELETE: api/Usuarios/5
        public void Delete(int id)
        {
            try
            {
                _usuariosLN.EliminarUsuario(id);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
    }
}
