using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7k.Model.Context
{
    class RegexFileSystemContext : FileSystemContext
    {
        /// <summary>
        /// <para>If true  -> only refresh when a field is change</para>
        /// <para>If false -> also change before return value</para>
        /// </summary>
        public Boolean StaticState { get; set; }

        public String RegexString { get; set; }

        public RegexFileSystemContext()
        {
            StaticState = true;
        }
    }
}
