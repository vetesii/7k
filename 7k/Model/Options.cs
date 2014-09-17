using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _7k.Model.Task.Option
{
    // TODO: lehet be kellene állítani, hogy az option-ok null-ok ne lehessenek, maximum üresek?

    abstract class AbstractOption
    {
        protected static String MissingLanguageText = "???";

        public Guid ID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }

        public AbstractOption()
        {
            this.ID = Guid.NewGuid();
        }
    }

    class BooleanOption : AbstractOption
    {
        public enum BooleanOptionType
        {
            CleanLineBreakToo, DeleteManualCharacterFormatting, DeleteManualParagraphFormatting,
            MFSItalic, MFSBold, MFSSuper, MFSSub, MFSSmallCaps, MFSAllCaps,
            OnlyInExternalStyle, OnlyAfterTitle, NotFormatParagraphWithDashStart,
            DEBASBefore, DEBASAfter,
            IEBASWithStyle, IEBASJumpEmpty, IEBASOnlyOneEmpty,
            IEIOPSOnlyExternalStyle,
            WordReplaceWithWildcards
        }

        public BooleanOptionType Type { get; private set; }
        public Boolean Value { get; set; }

        public BooleanOption()
        {
            this.Name = MultiLanguageTextProxy.getTextOrDefaultText(Type.ToString(), AbstractOption.MissingLanguageText);
            this.Description = MultiLanguageTextProxy.getTextOrDefaultText(Type.ToString(), AbstractOption.MissingLanguageText);
        }

        public BooleanOption deepCopy()
        {
            return new BooleanOption() { Name = this.Name, Description = this.Description, Value = this.Value };
        }
    }
    
    class StringOption : AbstractOption
    {
        public enum StringOptionType
        {
            BaseStyle, EmptyParagraphStyle, NoIndentStyle, NoIndentStyleWithSmallCaps, PoemStyle, MaximumRangeAfterTitle,
            FindWhatOfWordReplace, ReplaceWithOfWordReplace
        }

        public StringOptionType Type { get; private set; }
        public String Value { get; set; }

        private StringOption() 
        {
            this.Name = MultiLanguageTextProxy.getTextOrDefaultText(Type.ToString(), AbstractOption.MissingLanguageText);
            this.Description = MultiLanguageTextProxy.getTextOrDefaultText(Type.ToString(), AbstractOption.MissingLanguageText);
        }

        public StringOption deepCopy()
        {
            return new StringOption() { Name = this.Name, Description = this.Description, Value = this.Value };
        }
    }

    class StringListOption : AbstractOption
    {
        public enum StringListOptionType
        {
            StylesForNoIndentStyle, StyleForDropcap, TitleStyles,
            DEBASStyles,
            AllStyle, AllPoemStyle, LineBreakDeleteExcept,
            SeparatorTexts,
            IEBASInsertBefore, IEBASInsertAfter, IEBSDeleteBeforeAfter
        }
        
        public StringListOptionType Type { get; private set; }
        public List<String> Value { get; set; }

        private StringListOption() 
        {
            this.Name = MultiLanguageTextProxy.getTextOrDefaultText(Type.ToString(), AbstractOption.MissingLanguageText);
            this.Description = MultiLanguageTextProxy.getTextOrDefaultText(Type.ToString(), AbstractOption.MissingLanguageText);
        }

        public StringListOption deepCopy()
        {
            StringListOption newCopy = new StringListOption() { Name = this.Name, Description = this.Description, Value = this.Value };

            foreach (String item in this.Value)
                newCopy.Value.Add(item);

            return newCopy;
        }
    }


}
