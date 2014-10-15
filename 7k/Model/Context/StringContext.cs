using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7k.Model.Context
{
    class StringContext : AbstractContext
    {
        public enum SCType
        {
            BaseStyle, EmptyParagraphStyle, NoIndentStyle, NoIndentStyleWithSmallCaps, PoemStyle, MaximumRangeAfterTitle,
            FindWhatOfWordReplace, ReplaceWithOfWordReplace
        }

        public String Value { get; set; }

        public SCType ContextType { get; set; }

        public StringContext(SCType contextType) 
        {
            this.ContextType = contextType;
        }

        public override AbstractContext DeepCopy()
        {
            return new StringContext(this.ContextType) { Name = this.Name, Description = this.Description, Value = this.Value };
        }

        public override Boolean EqualContexts(object obj)
        {
            if (obj.GetType() != this.GetType()) return false;
            if ((obj as StringContext).ContextType.Equals(this.ContextType)) return false;

            return true;
        }
    }
}
