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
    public class CategoriasController : ApiController
    {
        // GET: api/Categorias
        public IEnumerable<Categoria> Get()
        {
            List<Categoria> categorias = new List<Categoria>();
            categorias = new CategoriaLN().ListarCategorias();
            return categorias;
        }

        // GET: api/Categorias/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Categorias
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Categorias/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Categorias/5
        public void Delete(int id)
        {
        }
    }
}
