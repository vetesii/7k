using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7k.Model.Context
{
    class StringListContext : AbstractContext
    {
        public enum SLType
        {
            StylesForNoIndentStyle, StyleForDropcap, TitleStyles,
            Styles,
            AllStyle, AllPoemStyle, LineBreakDeleteExcept,
            SeparatorTexts,
            IEBASInsertBefore, IEBASInsertAfter, IEBSDeleteBeforeAfter
        }

        public List<String> Value { get; set; }

        public SLType ContextType { get; set; }

        public StringListContext(SLType contextType)
        {
            this.ContextType = contextType;
        }

        public override AbstractContext DeepCopy()
        {
            StringListContext newCopy = new StringListContext(this.ContextType) { Name = this.Name, Description = this.Description, Value = this.Value };

            foreach (String item in this.Value)
                newCopy.Value.Add(item);

            return newCopy;
        }
    }
}
