using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vBook.Model.Message
{
    class SimpleMessage : AbstractMessage
    {
        private String text;

        public String Text
        {
            get { return text; }
            set { text = value; }
        }
        
        public override string getFullMessage()
        {
            throw new NotImplementedException();
        }
    }
}
