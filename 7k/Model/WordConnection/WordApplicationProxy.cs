using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;

namespace FoxTextCleaner.Models
{
    enum WordApplicationProxyState { NotRunning, Running}
    class WordApplicationProxy
    {
        private WordApplicationProxyState _State;
        /// <summary>
        /// Running or not
        /// </summary>
        public WordApplicationProxyState State
        {
            get { return _State; }
            set { _State = value; }
        }

        private ObservableCollection<WordDocumentProxy> _Documents;
        /// <summary>
        /// Azok a dokumentumok amelyek a program elindítása óta be voltak nyitva, nemcsak az aktuálisan megnyitottak
        /// </summary>
        public ObservableCollection<WordDocumentProxy> Documents
        {
            get { return _Documents; }
            set { _Documents = value; }
        }

        private Application _WApplication;
        public Application WApplication
        {
            get { return _WApplication; }
            set { _WApplication = value; }
        }

        private static WordApplicationProxy _Instance = new WordApplicationProxy();
        public static WordApplicationProxy Instance { get { return _Instance; } }
        private WordApplicationProxy() 
        {
            _Documents = new ObservableCollection<WordDocumentProxy>();
            _State = WordApplicationProxyState.NotRunning;
            _WApplication = null;
            RefreshWordDocuments();
        }


        public void ConnectToApplication()
        {
            try
            {
                _WApplication = (Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
                this.State = WordApplicationProxyState.NotRunning;
            }
            catch(Exception)
            {
                this.State = WordApplicationProxyState.NotRunning;
            }
        }

        public void RefreshApplication()
        {
            if (_WApplication == null)
            {
                ConnectToApplication();
            }
            else
            {
                try
                {
                    //if (_WApplication.CapsLock) { }; //check _WApplication is available (no have better solution)
                    WdWindowState x = _WApplication.WindowState;
                }
                catch (Exception)
                {
                    ConnectToApplication();
                }
            }
        }
        
        public void RefreshWordDocuments()
        {
            RefreshApplication();

            foreach (WordDocumentProxy docProxy in this.Documents)
                docProxy.State = WordDocumentProxyState.Refresh;

            try
            {
                foreach (Document doc in _WApplication.Documents)
                {
                    Boolean mapped = false;
                    foreach (WordDocumentProxy docProxy in this.Documents)
                    {
                        if (doc.FullName.Equals(docProxy.Fullname))
                        {
                            docProxy.WDocument = doc;
                            docProxy.State = WordDocumentProxyState.Connected;
                            mapped = true;
                            break;
                        }
                    }

                    if(!mapped)
                        Documents.Add(new WordDocumentProxy(doc));
                }
            }
            catch (Exception) { }
            finally
            {
                foreach (WordDocumentProxy docProxy in this.Documents)
                    if (docProxy.State.Equals(WordDocumentProxyState.Refresh));
                        //TODO: Be kellene állítani valamire
                        //docProxy.State = WordDocumentProxyState.;
                        
            }
        }


        // Plans
        protected void OpenWordApplication()
        {           
            ////Type type = Type.GetTypeFromProgID("Word.Application");
            ////wApplication = (Word.Application)System.Activator.CreateInstance(type);
            ////wApplication.Visible = true;
        }
    }
}
