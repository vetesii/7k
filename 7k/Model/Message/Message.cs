using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7k.Model
{
    /// <summary>
    /// the message sender can use String.Format("Hello {0}!", place); 
    /// and other String.Format()
    /// </summary>
    class AbstractMessage
    {
        public DateTime Date { get; set; }

        private String _text;
        public String Text
        {
            get { return _text; }
            set
            { 
                _text = value;
                Date = DateTime.Now;
            }
        }

        public AbstractMessage()
        {
            Date = DateTime.Now;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
