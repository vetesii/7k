using _7k.Model.Context;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace _7k.Model.ContextElement.Task
{
    /// <summary>
    /// Queue base working
    /// </summary>
    abstract class AbstractWordCleanerTask : AbstractTask
    {
        static protected object oEmpty = System.Reflection.Missing.Value;
        static protected object oMissing = Type.Missing;
        static protected object oTrue = true;

        protected List<AbstractContext> wordContextList;

        protected WordDocumentContext actual;

        public AbstractWordCleanerTask()
        {
            wordContextList = new List<AbstractContext>();
            actual = null;
        }

        protected override void SubAbstractTaskRun()
        {
            List<AbstractContext> wList = new List<AbstractContext>();

            getAllContextsForRun();

            foreach (AbstractContext item in extContextList)
                if (item is WordDocumentContext)
                {
                    WordDocumentContext wdc = item as WordDocumentContext;
                    if (!testConnection(wdc) && getBooleanContextValue(BooleanContext.BCType.TryToReopenWordDoc)) wdc.ReConnect();

                    if (testConnection(wdc))
                    {
                        actual = wdc;
                        SubAbstractWordCleanerTaskRun();
                    }
                }
        }

        abstract protected void SubAbstractWordCleanerTaskRun();

        public override List<AbstractContext> GetDefaultOptions()
        {
            List<AbstractContext> lst = base.GetDefaultOptions();
            if (lst == null) lst = new List<AbstractContext>();

            lst.Add(new BooleanContext(BooleanContext.BCType.SelectRangeInWord) { Value = false });
            lst.Add(new BooleanContext(BooleanContext.BCType.OpenFilePathToWord) { Value = true });
            lst.Add(new BooleanContext(BooleanContext.BCType.OpenSilentInWord) { Value = false });
            lst.Add(new BooleanContext(BooleanContext.BCType.TryToReopenWordDoc) { Value = true });

            return lst;
        }

        protected void sortWordContext()
        {
            wordContextList = new List<AbstractContext>();

            if (extContextList != null)
                foreach (AbstractContext item in extContextList)
                    if (item is WordDocumentContext) wordContextList.Add(item);

        }

        protected Boolean testConnection(Document doc)
        {
            if (doc == null) return false;
            try
            {
                Paragraph test = doc.Paragraphs[1];
            }
            catch
            {
                return false;
            }

            return true;
        }
        protected Boolean testConnection(List<Document> docList)
        {
            foreach (Document item in docList)
                if (false == testConnection(item)) return false;

            return true;
        }

        protected Boolean testConnection(AbstractContext cont)
        {


            return true;
        }
        protected Boolean testConnection(List<AbstractContext> cont)
        {


            return true;
        }


        protected bool wordFindAndReplace(object mit, object mire)
        {
            object oReplaceAll = WdReplace.wdReplaceAll;
            Range rng = actual.Doc.Content;

            rng.Find.ClearFormatting();
            rng.Find.Text = (String)mit;
            rng.Find.Replacement.ClearFormatting();
            rng.Find.Replacement.Text = (String)mire;

            return rng.Find.Execute(
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                 ref oReplaceAll, // replace all
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing
                 );
        }
        protected bool wordFindAndReplaceWithRegex(object mit, object mire)
        {
            object oReplaceAll = WdReplace.wdReplaceAll;
            Range rng = actual.Doc.Content;

            rng.Find.ClearFormatting();
            rng.Find.Text = (string)mit;
            rng.Find.Replacement.ClearFormatting();
            rng.Find.Replacement.Text = (string)mire;

            return rng.Find.Execute(
                 ref oMissing, ref oMissing, ref oMissing,
                 ref oTrue, // regexreplace => true
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                 ref oReplaceAll,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing
                 );
        }

        //protected bool betuVagySzamVagyHarmasponte(char mi)
        protected bool characterOrNumberOrTriplePoint(char ch)
        {
            if (('a' <= ch && ch <= 'z') || ('A' <= ch && ch <= 'Z') || ('0' <= ch && ch <= '9')) return true;

            if (ch == '\u2026') return true;

            return false;
        }

        protected String capitalizeRomanNumbers(char[] tomb)
        {
            String ret = new string(tomb);
            String pattern = @"(\s|^)([ivxlcdm]*)([.!?\s])";
            //String replacePattern = "$0$1$2";
            try
            {
                //ret = Regex.Replace(ret, pattern, replacePattern);
                ret = Regex.Replace(ret, pattern, m => m.ToString().ToUpper(), RegexOptions.IgnoreCase);
            }
            catch (Exception) { }

            return ret;
        }

        protected void isStyleExist(string neve)
        {
            bool exist = false;
            foreach (Word.Style item in actual.Doc.Styles)
            {
                if (item.NameLocal.Equals(neve))
                {
                    exist = true;
                    break;
                }
            }
            // TODO 3 make new exception type
            if (!exist) throw new Exception(String.Format(MultiLanguageTextProxy.GetText("Error_Not_Imported_Style"), neve));
        }
    }
}
