using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7k.Model.ContextElement
{
    // TODO make this file to default, but add resource base overriding 

    static class SpecialCharacters
    {
        //TODO sortörés karaktert és a space-t is ide belerakni és azt hasznáni

        static public string italicOpeningTag = "ѾitalicѾ";
        static public string italicClosingTag = "Ѿ/italicѾ";
        static public string boldOpeningTag = "ѾboldѾ";
        static public string boldClosingTag = "Ѿ/boldѾ";
        static public string superOpeningTag = "ѾsuperѾ";
        static public string superClosingTag = "Ѿ/superѾ";
        static public string subOpeningTag = "ѾsubѾ";
        static public string subClosingTag = "Ѿ/subѾ";
        static public string smallcapsOpeningTag = "ѾsmallcapsѾ";
        static public string smallcapsClosingTag = "Ѿ/smallcapsѾ";
        static public string upperOpeningTag = "ѾupperѾ";
        static public string upperClosingTag = "Ѿ/upperѾ";

        static public Char enDashChar = '\u2013';
        static public String enDash = enDashChar.ToString();                   //jelenlegi favorit

        // Dash unicode characters
        static public Char minusChar = '-';
        static public String minus = minusChar.ToString();                     //jelenlegi favorit
        static public Char emDashChar = '\u2014';
        static public String emDash = emDashChar.ToString();
        static public Char hyphenChar = '\u2010';
        static public String hyphen = hyphenChar.ToString();
        static public Char hyphenNonBreakingChar = '\u2011';
        static public String hyphenNonBreaking = hyphenNonBreakingChar.ToString();
        static public Char figureDashChar = '\u2012';
        static public String figureDash = figureDashChar.ToString();
        static public Char horizontalBarChar = '\u2015';
        static public String horizontalBar = horizontalBarChar.ToString();

        // TODO megnézni mi ez
        static public Char UFODashChar = '−';
        static public String UFODash = UFODashChar.ToString();

        // Space unicode characters
        static public Char enQuadChar = '\u2000';
        static public String enQuad = enQuadChar.ToString();                     //jelenlegi favorit
        static public String notBreakableSpace = '\u00A0'.ToString();
        static public String narrowNonBreakSpace = '\u202F'.ToString();
        static public String thinSpace = '\u2009'.ToString();
        
        static public String threeDot = '\u2026'.ToString();
        
        static public String quotionMarkStart = "„";
        static public String quotionMarkEnd = "”";
        

        static public String wordHandPageBreak = @"^m";
        static public String wordLineBreak     = @"^l";
        static public String wordSzakaszBreak  = @"^b";
        static public String wordHasabTores    = @"^n";

        static public String wordParagraphMarkNormal = @"^p";
        static public String wordParagraphMarkRegex = @"^13";
    }
}
