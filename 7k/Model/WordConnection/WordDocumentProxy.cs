using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxTextCleaner.Models
{
    enum WordDocumentProxyState { Refresh, Connected, NotOpened, FileNotFound }

    class WordDocumentProxy
    {
        private Document _WDocument;
        public Document WDocument
        {
            get { return _WDocument; }
            set { _WDocument = value; }
        }

        private String _Fullname;
        public String Fullname
        {
            get { return _Fullname; }
            set { _Fullname = value; }
        }

        private String _FileName;
        public String FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }
        

        private WordDocumentProxyState _State;
        public WordDocumentProxyState State
        {
            get { return _State; }
            set { _State = value; }
        }

        public WordDocumentProxy(Document doc)
        {
            _WDocument = doc;
            _Fullname = doc.FullName;
            _FileName = doc.Name;
            _State = WordDocumentProxyState.Connected;
        }

        public override string ToString()
        {
            return _FileName;
        }
    }
}
