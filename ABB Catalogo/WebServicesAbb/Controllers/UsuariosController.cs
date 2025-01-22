using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.LogicaNegocio.Core;

namespace WebServicesAbb.Controllers
{
    [Authorize]
    public class UsuariosController : ApiController
    {
        private readonly UsuariosLN _usuariosLN;

        public UsuariosController()
        {
            _usuariosLN = new UsuariosLN();
        }

        // GET: api/Usuarios
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                var usuarios = _usuariosLN.ListarUsuarios();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Usuarios/GetById/5
        [HttpGet]
        [Route("api/Usuarios/GetById/{id}")]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                var usuario = _usuariosLN.BuscaUsuarioId(id);
                if (usuario == null)
                {
                    return NotFound();
                }
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Usuarios/ValidateUser
        [HttpGet]
        [Route("api/Usuarios/ValidateUser")]
        public IHttpActionResult ValidateUser([FromUri] string usuario, [FromUri] string password)
        {
            try
            {
                var usuarioId = _usuariosLN.GetUsuarioId(usuario, password);
                return Ok(usuarioId);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Usuarios
        [HttpPost]
        public IHttpActionResult Post([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest("El objeto usuario no puede ser nulo.");
            }

            try
            {
                var usuarioCreado = _usuariosLN.InsertarUsuario(usuario);
                return Created($"api/Usuarios/{usuarioCreado.IdUsuario}", usuarioCreado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Usuarios/5
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest("El objeto usuario no puede ser nulo.");
            }

            try
            {
                var usuarioActualizado = _usuariosLN.ModificarUsuario(id, usuario);
                return Ok(usuarioActualizado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Usuarios/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _usuariosLN.EliminarUsuario(id);
                return StatusCode(HttpStatusCode.NoContent); // 204 No Content
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
