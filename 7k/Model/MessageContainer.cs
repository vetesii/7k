using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vBook.Model.Message;

namespace _7k.Model
{
    class MessageContainer : ITaskObserver
    {
        protected List<AbstractMessage> messages;

        public void sendMessage(AbstractMessage e)
        {
            throw new NotImplementedException();
        }
    }
}
