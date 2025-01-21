using ABB.Catalogo.AccesoDatos.Core;
using ABB.Catalogo.Entidades.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABB.Catalogo.LogicaNegocio.Core
{
    public class CategoriaLN
    {
        public List<Categoria> ListarCategorias()
        {
            List<Categoria> lista = new List<Categoria>();
            try
            {
                CategoriaDA categorias = new CategoriaDA();
                return categorias.ListarCategorias();
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                //Logger.paginaNombre = this.GetType().Name;
                //Logger.Escribir(&quot;Error en Logica de Negocio: &quot; + ex.Message + &quot;. &quot; + ex.StackTrace + &quot;. &quot; + innerException);
                return lista;

            }
        }
    }
}
