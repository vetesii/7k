using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;

namespace _7k.Model
{
    public class WordProxy : Notifier
    {
        Word.Application wApplication;
        public Word.Application WApplication
        {
            get { return wApplication; }
        }

        ObservableCollection<String> selectableDocument;
        public ObservableCollection<String> SelectableDocument
        {
            get { return selectableDocument; }
        }
        public String reNewDocumentList()
        {
            selectableDocument.Clear();
            try
            {
                wApplication = (Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
                Documents docs = wApplication.Documents;

                foreach (Document item in wApplication.Documents)
                {
                    // TODO megcsinálni
                    // MessageWall.Instance.sendMessage(Guid.NewGuid(), item.FullName, System.Windows.Media.Brushes.White);
                }
            }
            catch (COMException)
            {
                //Type type = Type.GetTypeFromProgID("Word.Application");
                //wApplication = (Word.Application)System.Activator.CreateInstance(type);
                //wApplication.Visible = true;

                selectableDocument.Add("Nincs elérhető dokumentum!");
                OnPropertyChanged("SelectableDocument");
                return "";
            }

            //alkalmazas = (Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");

            //alkalmazas = (Application)Microsoft.VisualBasic.Interaction.GetObject(null, "Word.Application");
            // egy másik módszer, amit régen használtam, de ahhoz + referenci keresgélés és betöltés is tartozik
            // add reference / misrosoft word interrop vagy valami más dll kell hozzá
            // visual basic interop referenciát kell hozzáadni

            string r = String.Empty;
            foreach (Document item in wApplication.Documents)
            {
                try
                {
                    if (item.Equals(_actualDocument))
                        r = item.Name;
                }
                catch { }
                selectableDocument.Add(item.Name);
            }
            return r;
        }

        Document _actualDocument;
        public Document ActualDocument
        {
            get { return _actualDocument; }
            set { _actualDocument = value; }
        }
        public bool changeActualDocument(string name)
        {
            try
            {
                foreach (Document item in wApplication.Documents)
                    if (item.Name == name)
                    {
                        ActualDocument = item;
                        return true;
                    }
            }
            catch (Exception) { }

            return false;
        }
        public string getFirstDocumentName()
        {
            try
            {
                reNewDocumentList();
                foreach (Document item in wApplication.Documents)
                    return item.Name;
            }
            catch (Exception) { }

            return null;
        }

        static WordProxy _instance;
        static public WordProxy Instance
        {
            get { return _instance; }
        }
        static WordProxy()
        {
            _instance = new WordProxy();
            _instance.selectableDocument = new ObservableCollection<string>();
        }
        protected WordProxy() { }
    }
}
