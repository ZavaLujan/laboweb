using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebServicesAbb.Controllers
{
    public class ProductosController : ApiController
    {
        // GET: api/Productos
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Productos/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Productos
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Productos/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Productos/5
        public void Delete(int id)
        {
        }
    }
}
