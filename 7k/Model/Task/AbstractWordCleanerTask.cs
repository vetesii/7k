using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _7k.Model.ContextElement.Task
{
    abstract class AbstractWordCleanerTask : AbstractTask
    {
        static protected object oEmpty = System.Reflection.Missing.Value;
        static protected object oMissing = Type.Missing;
        static protected object oTrue = true;

        static public Boolean _selectRangeInWord = true;

        public AbstractWordCleanerTask()
        {
            
        }

        protected bool wordFindAndReplace(object mit, object mire)
        {
            object oReplaceAll = WdReplace.wdReplaceAll;
            Range rng = WordProxy.Instance.ActualDocument.Content;

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
            Range rng = WordProxy.Instance.ActualDocument.Content;

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
            foreach (Microsoft.Office.Interop.Word.Style item in WordProxy.Instance.ActualDocument.Styles)
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
