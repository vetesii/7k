using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vBook.Model
{
    interface ITaskObserver
    {
        void sendMessage(vBook.Model.Message.AbstractMessage e);
    }
}
