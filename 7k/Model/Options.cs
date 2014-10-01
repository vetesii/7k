using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _7k.Model.Task.Option
{
    abstract class AbstractOption
    {
        public enum OptionType
        {
            // Boolean option types
            CleanLineBreakToo, DeleteManualCharacterFormatting, DeleteManualParagraphFormatting,
            MFSItalic, MFSBold, MFSSuper, MFSSub, MFSSmallCaps, MFSAllCaps,
            OnlyInExternalStyle, OnlyAfterTitle, NotFormatParagraphWithDashStart,
            AlsoBefore, ThenAlso,
            IEBASWithStyle, IEBASJumpEmpty, IEBASOnlyOneEmpty,
            IEIOPSOnlyExternalStyle,
            WordReplaceWithWildcards,

            // String option types
            BaseStyle, EmptyParagraphStyle, NoIndentStyle, NoIndentStyleWithSmallCaps, PoemStyle, MaximumRangeAfterTitle,
            FindWhatOfWordReplace, ReplaceWithOfWordReplace,

            // String list option types
            StylesForNoIndentStyle, StyleForDropcap, TitleStyles,
            Styles,
            AllStyle, AllPoemStyle, LineBreakDeleteExcept,
            SeparatorTexts,
            IEBASInsertBefore, IEBASInsertAfter, IEBSDeleteBeforeAfter
        }

        public Guid ID { get; set; }
        public OptionType Key { get; private set; }
        public String Name { get; set; }
        public String Description { get; set; }

        public AbstractOption(OptionType key)
        {
            this.ID = Guid.NewGuid();
            this.Key = key;

            Name = MultiLanguageTextProxy.GetText("OptionType_" + key.GetType() + "_Name", key.ToString());
            Description = MultiLanguageTextProxy.GetText("OptionType_" + key.GetType() + "_Description", key.ToString());                
        }

        // TODO 5 constructor with key and language args

        public abstract AbstractOption DeepCopy();
    }

    class BooleanOption : AbstractOption
    {
        public Boolean Value { get; set; }

        public BooleanOption(OptionType key) : base(key) { }

        public override AbstractOption DeepCopy()
        {
            return new BooleanOption(this.Key) { Name = this.Name, Description = this.Description, Value = this.Value };
        }
    }
    
    class StringOption : AbstractOption
    {
        public String Value { get; set; }

        public StringOption(OptionType key) : base(key) { }

        public override AbstractOption DeepCopy()
        {
            return new StringOption(this.Key) { Name = this.Name, Description = this.Description, Value = this.Value };
        }
    }

    class StringListOption : AbstractOption
    {        
        public List<String> Value { get; set; }

        public StringListOption(OptionType key) : base(key) { }

        public override AbstractOption DeepCopy()
        {
            StringListOption newCopy = new StringListOption(this.Key) { Name = this.Name, Description = this.Description, Value = this.Value };

            foreach (String item in this.Value)
                newCopy.Value.Add(item);

            return newCopy;
        }
    }


}
