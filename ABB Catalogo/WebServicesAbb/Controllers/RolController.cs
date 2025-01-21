using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.LogicaNegocio.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebServicesAbb.Controllers
{
    public class RolController : ApiController
    {
        // GET: api/Rol
        public IEnumerable<Rol> Get()
        {
            try
            {
                return new RolLN().ListarRoles();
            }
            catch (Exception ex)
            {
                // Log error
                throw;
            }
        }

        // GET: api/Rol/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Rol
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Rol/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Rol/5
        public void Delete(int id)
        {
        }
    }
}
