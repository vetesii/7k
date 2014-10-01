using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7k.Model
{
    class MessageContainer : ITaskObserver
    {
        public enum MessageType
        {
            TaskState, TaskError, Task
        }

        protected List<AbstractMessage> allMessages;

        //public void addMessage(Guid id, )

        public void sendMessage(AbstractMessage e)
        {
            throw new NotImplementedException();
        }
    }
}
