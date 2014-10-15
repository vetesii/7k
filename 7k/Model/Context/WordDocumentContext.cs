using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7k.Model.Context
{
    class WordDocumentContext : AbstractContext
    {
        public Document Doc { get; set; }

        public FileInfo FullName { get; set; }

        public WordDocumentContext(Document doc)
        {
            if (doc == null) throw new ArgumentNullException();

            Doc = doc;

            FullName = new FileInfo(doc.FullName);
        }
        public override AbstractContext DeepCopy()
        {
            throw new NotImplementedException();
        }

        public Boolean ReConnect()
        {
            // TODO 1

            return false;
        }

        public override Boolean EqualContexts(object obj)
        {
            if (obj.GetType() != this.GetType()) return false;

            return true;
        }
    }
}
