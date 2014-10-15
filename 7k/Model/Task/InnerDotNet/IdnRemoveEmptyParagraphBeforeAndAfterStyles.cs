using _7k.Model.Context;
using _7k.Model.ContextElement.Task.Option;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7k.Model.ContextElement.Task.InnerDotNet
{
    class IdnRemoveEmptyParagraphBeforeAndAfterStyles : AbstractWordCleanerTask
    {
        public override List<AbstractContext> GetDefaultOptions()
        {
            List<AbstractContext> lst = base.GetDefaultOptions();
            if (lst == null) lst = new List<AbstractContext>();

            BooleanContext bo = new BooleanContext(BooleanContext.BCType.AlsoBefore) { Value = true };
            lst.Add(bo);

            bo = new BooleanContext(BooleanContext.BCType.ThenAlso) { Value = true };
            lst.Add(bo);

            List<String> value = new List<string>() 
            { 
                "cim0", "cim1", "cim2", "cim2eo", "cim3", "cim3eo", "cim4", "cim4eo", "felsorolas", "jegyzet", "cim", "szerzo" 
            };
            StringListContext sto = new StringListContext(StringListContext.SLType.Styles) { Value = value };
            lst.Add(sto);

            return lst;
        }

        protected override void EmbemedStart()
        {
            Boolean removeBefore = getBooleanContextValue(BooleanContext.BCType.AlsoBefore);
            Boolean removeAfter = getBooleanContextValue(BooleanContext.BCType.ThenAlso);
            List<String> styles = getStringListOptionValue(StringListContext.SLType.Styles);

            int documentSize = WordProxy.Instance.ActualDocument.Paragraphs.Count;
            uint loopCount = 1;
            uint counter = 0;

            Paragraph p = WordProxy.Instance.ActualDocument.Paragraphs[1];
            while (p != null)
            {
                if (styles.Contains(p.get_Style().NameLocal()))
                {
                    if (removeBefore)
                    {
                        Paragraph p_elo = p.Previous();
                        while (p_elo != null && p_elo.Range.Characters.Count == 1)
                        {
                            p_elo.Range.Delete();
                            p_elo = p.Previous();
                            counter++;
                        }
                    }

                    if (removeAfter)
                    {
                        Paragraph p_kov = p.Next();
                        while (p_kov != null && p_kov.Range.Characters.Count == 1)
                        {
                            p_kov.Range.Delete();
                            p_kov = p.Next();
                            counter++;
                        }
                    }
                }

                this.Percent = (double)loopCount / documentSize;
                this.Counter = counter.ToString();

                loopCount += 1;
                p = p.Next();
            }

            Percent = 1;
        }
    }
}
