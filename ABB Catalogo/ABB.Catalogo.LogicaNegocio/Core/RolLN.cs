using ABB.Catalogo.AccesoDatos.Core;
using ABB.Catalogo.Entidades.Core;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABB.Catalogo.LogicaNegocio.Core
{
    public class RolLN
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(RolLN));

        public List<Rol> ListarRoles()
        {
            try
            {
                return new RolDA().ListarRoles();
            }
            catch (Exception ex)
            {
                Log.Error("Error al listar roles", ex);
                throw;
            }
        }
    }
}
