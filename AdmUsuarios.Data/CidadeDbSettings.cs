using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmUsuarios.Data
{
    public class CidadeDbSettings
    {
        public string ConnectionString { get; set; } = null;
        public string DatabaseName { get; set; } = null;
        public string CidadeCollectionName { get; set; } = null;
    }
}

