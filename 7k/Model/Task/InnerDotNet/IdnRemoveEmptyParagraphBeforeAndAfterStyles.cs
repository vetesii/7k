using _7k.Model.Task.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7k.Model.Task.InnerDotNet
{
    /// <summary>
    /// _name = "Üres sor törlése stílus előtt/után";
    /// _toolTip = "A megadott stílusú bekezdések előtt és után törli az üres bekezdéseket.";
    /// </summary>
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
            //bool before = searchCheckOptionValue(CheckOptionType.DEBASBefore);
            //bool after = searchCheckOptionValue(CheckOptionType.DEBASAfter);
            //ObservableCollection<string> styles = searchListOptionValue(ListOptionType.DEBASStyles);

            //int counter = WordProxy.Instance.ActualDocument.Paragraphs.Count;
            //uint x = 1;
            //uint changeCounter = 0;

            //Paragraph p = WordProxy.Instance.ActualDocument.Paragraphs[1];
            //while (letezikeAParagrafus(p))
            //{
            //    if (styles.Contains(p.get_Style().NameLocal()))
            //    {
            //        if (before)
            //        {
            //            Paragraph p_elo = p.Previous();
            //            while (letezikeAParagrafus(p_elo) && p_elo.Range.Characters.Count == 1)
            //            {
            //                p_elo.Range.Delete();
            //                p_elo = p.Previous();
            //                changeCounter++;
            //            }
            //        }

            //        if (after)
            //        {
            //            Paragraph p_kov = p.Next();
            //            while (letezikeAParagrafus(p_kov) && p_kov.Range.Characters.Count == 1)
            //            {
            //                p_kov.Range.Delete();
            //                p_kov = p.Next();
            //                changeCounter++;
            //            }
            //        }
            //    }

            //    MessageWall.Instance.changeCounterWithDispatcher(id, "(" + changeCounter + ")");
            //    MessageWall.Instance.changePercentWithDispatcher(id, (int)(((double)x / counter) * 100) + "%");
            //    x += 1;

            //    p = p.Next();
            //}
        }
    }
}
