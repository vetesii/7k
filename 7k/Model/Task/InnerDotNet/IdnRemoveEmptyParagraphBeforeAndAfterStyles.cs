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
        public override List<AbstractOption> GetDefaultOptions()
        {
            List<AbstractOption> retList = new List<AbstractOption>();

            BooleanOption bo = new BooleanOption(AbstractOption.OptionType.AlsoBefore) { Value = true };
            retList.Add(bo);

            bo = new BooleanOption(AbstractOption.OptionType.ThenAlso) { Value = true };
            retList.Add(bo);

            List<String> value = new List<string>() 
            { 
                "cim0", "cim1", "cim2", "cim2eo", "cim3", "cim3eo", "cim4", "cim4eo", "felsorolas", "jegyzet", "cim", "szerzo" 
            };
            StringListOption sto = new StringListOption(AbstractOption.OptionType.Styles) { Value = value };
            retList.Add(sto);

            return retList;
        }

        protected override void EmbemedStart()
        {
            Boolean removeBefore = getBooleanOptionValue(AbstractOption.OptionType.AlsoBefore);
            Boolean removeAfter = getBooleanOptionValue(AbstractOption.OptionType.ThenAlso);
            List<String> styles = getStringListOptionValue(AbstractOption.OptionType.Styles);

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
