using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7k.Model.Context
{
    // TODO a UI-n kell frissít gomb rá
    class FileSystemContext : AbstractContext
    {
        public FileSystemInfo Path { get; set; }

        public override Boolean EqualContexts(object obj)
        {
            if (obj.GetType() != this.GetType()) return false;

            return true;
        }

        public override AbstractContext DeepCopy()
        {
            throw new NotImplementedException();
        }
    }
}
