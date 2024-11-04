using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmUsuarios.Data
{
    public class LogradouroDbSettings
    {
        public string ConnectionString { get; set; } = null;
        public string DatabaseName { get; set; } = null;
        public string LogradouroCollectionName { get; set; } = null;
    }
}