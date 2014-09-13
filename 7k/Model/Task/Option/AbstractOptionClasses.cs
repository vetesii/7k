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
        public Guid ID { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public AbstractOption()
        {
            this.ID = Guid.NewGuid();
        }
    }

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

    class BooleanOption : AbstractOption
    {
        public BooleanOptionType Type { get; private set; }
        public Boolean Value { get; set; }

        public BooleanOption()
        {
            this.ID = Guid.NewGuid();

            // TODO set name
            // TODO set desc
        }

        public BooleanOption deepCopy()
        {
            return new BooleanOption() { Name = this.Name, Description = this.Description, Value = this.Value };
        }
    }

    public enum StringOptionType
    {
        BaseStyle, EmptyParagraphStyle, NoIndentStyle, NoIndentStyleWithSmallCaps, PoemStyle, MaximumRangeAfterTitle,

        FindWhatOfWordReplace, ReplaceWithOfWordReplace
    }

    class StringOption : AbstractOption
    {
        public StringOptionType Type { get; private set; }
        public String Value { get; set; }

        private StringOption() { }
        public StringOption(String value)
        {
            this.Value = value;
        }

        public StringOption deepCopy()
        {
            return new StringOption(this.Value);
        }
    }

    public enum StringListOptionType
    {
        StylesForNoIndentStyle, StyleForDropcap, TitleStyles,
        DEBASStyles,
        AllStyle, AllPoemStyle, LineBreakDeleteExcept,
        SeparatorTexts,
        IEBASInsertBefore, IEBASInsertAfter, IEBSDeleteBeforeAfter
    }

    class StringListOption : AbstractOption
    {
        public StringListOptionType Type { get; private set; }
        public List<String> Value { get; set; }

        private StringListOption() { }
        public StringListOption(List<String> value)
        {
            this.Value = value;
        }

        public StringListOption deepCopy()
        {
            StringListOption newCopy = new StringListOption();

            newCopy._identifier = this._identifier;
            newCopy.Name = this.Name;

            newCopy._value = new ObservableCollection<string>();
            foreach (String item in this._value)
                newCopy._value.Add(item);

            return newCopy;
        }
    }


}
