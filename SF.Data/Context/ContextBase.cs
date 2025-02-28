using SF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.Data.Context
{
    public abstract class ContextBase
    {
        public abstract string ConnectionString { get; set; }

        public abstract DapperContext CreateConnection();

        public abstract DapperContext SetConnectionString();

    }
}
