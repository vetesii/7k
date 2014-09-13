using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vBook.Model.Message
{
    abstract class AbstractMessage
    {
        private Guid id;

        public Guid ID
        {
            get { return id; }
            set { id = value; }
        }
        
        public abstract string getFullMessage();


    }
}
