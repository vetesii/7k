using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7k.Model.Context
{
    class BooleanContext : AbstractContext
    {
        public enum BCType
        {
            CleanLineBreakToo, DeleteManualCharacterFormatting, DeleteManualParagraphFormatting,
            MFSItalic, MFSBold, MFSSuper, MFSSub, MFSSmallCaps, MFSAllCaps,
            OnlyInExternalStyle, OnlyAfterTitle, NotFormatParagraphWithDashStart,
            AlsoBefore, ThenAlso,
            IEBASWithStyle, IEBASJumpEmpty, IEBASOnlyOneEmpty,
            IEIOPSOnlyExternalStyle,
            WordReplaceWithWildcards
        }

        public Boolean Value { get; set; }

        public BCType ContextType { get; set; }

        public BooleanContext(BCType ctype)
        {
            this.ContextType = ctype;
        }

        public override AbstractContext DeepCopy()
        {
            return new BooleanContext(this.ContextType) { Name = this.Name, Description = this.Description, Value = this.Value };
        }
    }
}
