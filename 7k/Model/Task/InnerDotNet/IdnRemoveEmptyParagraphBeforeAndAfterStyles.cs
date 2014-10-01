using _7k.Model.Task.Option;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7k.Model.Task.InnerDotNet
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

        public override void Run()
        {
            Boolean removeBefore = getBooleanOptionValue(AbstractOption.OptionType.AlsoBefore);
            Boolean removeAfter = getBooleanOptionValue(AbstractOption.OptionType.ThenAlso);
            List<String> styles = getStringListOptionValue(AbstractOption.OptionType.Styles);

            //bool before = searchCheckOptionValue(CheckOptionType.DEBASBefore);
            //bool after = searchCheckOptionValue(CheckOptionType.DEBASAfter);
            //ObservableCollection<string> styles = searchListOptionValue(ListOptionType.DEBASStyles);

            int documentSize = WordProxy.Instance.ActualDocument.Paragraphs.Count;
            uint x = 1;
            uint changeCounter = 0;

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
                            changeCounter++;
                        }
                    }

                    if (removeAfter)
                    {
                        Paragraph p_kov = p.Next();
                        while (p_kov != null && p_kov.Range.Characters.Count == 1)
                        {
                            p_kov.Range.Delete();
                            p_kov = p.Next();
                            changeCounter++;
                        }
                    }
                }

                // TODO uzenetkuldes taskbol
                //MessageWall.Instance.changeCounterWithDispatcher(id, "(" + changeCounter + ")");
                //MessageWall.Instance.changePercentWithDispatcher(id, (int)(((double)x / documentSize) * 100) + "%");
                x += 1;

                p = p.Next();
            }
        }
    }
}
