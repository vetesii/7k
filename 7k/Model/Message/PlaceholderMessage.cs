using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vBook.Model.Message
{
    /// <summary>
    /// @ivetesi: this message type is needless, because the message sender can use String.Format("Hello {0}!", place); 
    /// and other String.Format()
    /// </summary>
    class PlaceholderMessage : AbstractMessage
    {
        private string text;
        public string Text {
            get { return text; }
            set {
                text = value;
                generateFullMessage();
            }
        }

        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }
        
        public Dictionary<string,string> Placeholders { get; set; }

        private string fullText = String.Empty;

        public override string getFullMessage()
        {
            throw new NotImplementedException();
        }

        public PlaceholderMessage()
        {
            this.Text = String.Empty;
            this.fullText = String.Empty;
        }

        public PlaceholderMessage(string text)
        {
            if (text == null) this.Text = String.Empty;
            else this.Text = text;

            this.fullText = String.Empty;
        }

        public PlaceholderMessage(string text, Dictionary<string, string> placeholders)
        {
            if (text == null) this.Text = String.Empty;
            else this.Text = text;

            if (placeholders == null) this.Placeholders = new Dictionary<string,string>();
            else this.Placeholders = placeholders;
        }

        protected string generateFullMessage() 
        {
            if (Text == null || Text.Equals(String.Empty)) return String.Empty;
            if ( Placeholders == null || Placeholders.Count < 1) return Text;

            StringBuilder sb = new StringBuilder(Text);

            foreach (KeyValuePair<string,string> item in Placeholders)
            {
                sb.Replace(item.Key, item.Value);
            }

            return sb.ToString();

        }
    }
}
