using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _7k.Model.ContextElement.Task;
using _7k.Model.ContextElement.Task.Option;

namespace _7k.Model.ContextElement.Task.InnerDotNet
{
    class IdnDashChange : AbstractWordCleanerTask
    {
        /// <summary>
        /// Mit mire kellene használni:
        ///  - párbeszédhez: en-dash
        ///  - paragrafus kezdő párbeszéd jele után: en_quad (srf: narrow_non_break_space vagy thin space is jó)
        ///  - egyéb helyekre kötöjel, de sima minusz jel lesz, mert a kötöjel (\u2010) kevés betűtípus támogatja
        ///      (ami már ígyis gond a en-quad-nál) és könyebb is javításkor sima minusz jelet berakni
        ///  - speciális esetben (2004-2006-os költségvetés..., 1-1-es döntetlennel) em-dash kellene majd javításkor
        ///      kézileg beszúrni :)
        /// </summary>
        protected override void SubAbstractWordCleanerTaskRun()
        {
            // replace to en_dash
            wordFindAndReplace(SpecialCharacters.minus, SpecialCharacters.enDash);
            wordFindAndReplace(SpecialCharacters.emDash, SpecialCharacters.enDash);
            wordFindAndReplace(SpecialCharacters.hyphen, SpecialCharacters.enDash);
            wordFindAndReplace(SpecialCharacters.hyphenNonBreaking, SpecialCharacters.enDash);
            wordFindAndReplace(SpecialCharacters.figureDash, SpecialCharacters.enDash);
            wordFindAndReplace(SpecialCharacters.horizontalBar, SpecialCharacters.enDash);

            wordFindAndReplace(SpecialCharacters.UFODash, SpecialCharacters.enDash);


            wordFindAndReplace(@"." + SpecialCharacters.enDash, @". " + SpecialCharacters.enDash);
            wordFindAndReplace(@"!" + SpecialCharacters.enDash, @"! " + SpecialCharacters.enDash);
            wordFindAndReplace(@"?" + SpecialCharacters.enDash, @"? " + SpecialCharacters.enDash);


            // Kötöjelek visszarakása a megfelelő helyekre (ez rövidebb, mint az en_dash)
            wordFindAndReplaceWithRegex(@"([a-zA-Z0-9])" + SpecialCharacters.enDash + @"( és )", @"\1" + SpecialCharacters.minus + @"\2");
            wordFindAndReplaceWithRegex(@"([a-zA-Z])( és )" + SpecialCharacters.enDash + @"([a-zA-z])", @"\1\2" + SpecialCharacters.minus + @"\3");
            wordFindAndReplaceWithRegex(@"([a-zA-Z0-9])" + SpecialCharacters.enDash + @"( vagy )", @"\1" + SpecialCharacters.minus + @"\2");
            wordFindAndReplaceWithRegex(@"([a-zA-Z])( vagy )" + SpecialCharacters.enDash + @"([a-zA-z])", @"\1\2" + SpecialCharacters.minus + @"\3");

            wordFindAndReplaceWithRegex(@"([a-zA-Z])" + SpecialCharacters.enDash + @"(, illetve )", @"\1" + SpecialCharacters.minus + @"\2");

            wordFindAndReplaceWithRegex(@"(”)" + SpecialCharacters.enDash + @"([a-zA-Z0-9])", @"\1" + SpecialCharacters.minus + @"\2");
            wordFindAndReplaceWithRegex(@"([a-zA-Z0-9])" + SpecialCharacters.enDash + @"([a-zA-Z0-9])", @"\1" + SpecialCharacters.minus + @"\2");


            // Kezdő gondolatjel után nemtörhető szóköz
            //installEnQuad(new Guid(), false)
            wordFindAndReplace(@"^p" + SpecialCharacters.enDash + @" ", @"^p" + SpecialCharacters.enDash + SpecialCharacters.enQuad);
            wordFindAndReplaceWithRegex(@"(^13" + SpecialCharacters.enDash + @")([a-zA-Z0-9…])", @"\1" + SpecialCharacters.enQuad + @"\2");


            // Egyéb szépítés miután az en_dash-ek a helyükön vannak
            wordFindAndReplace(SpecialCharacters.enDash + SpecialCharacters.threeDot, SpecialCharacters.enDash + @" " + SpecialCharacters.threeDot);
            wordFindAndReplace(SpecialCharacters.enDash + SpecialCharacters.enQuad + SpecialCharacters.threeDot + @" ", SpecialCharacters.enDash + SpecialCharacters.enQuad + SpecialCharacters.threeDot);
            wordFindAndReplace(SpecialCharacters.enDash + @" " + SpecialCharacters.threeDot + @" ", SpecialCharacters.enDash + @" " + SpecialCharacters.threeDot);
        }
    }
}
