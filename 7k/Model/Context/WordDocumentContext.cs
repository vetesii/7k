using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7k.Model.Context
{
    class WordDocumentContext
    {
        public Document Doc { get; set; }

        public FileInfo FullName { get; set; }

        public Boolean ReopenIfDisconnect { get; set; }

        public WordDocumentContext(Document doc)
        {
            if (doc == null) throw new ArgumentNullException();

            Doc = doc;

            FullName = new FileInfo(doc.FullName);
        }

        public Boolean ReConnect()
        {
            // TODO 1

            return false;
        }
    }
}
