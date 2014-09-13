using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vBook.Model.Task
{
    abstract class AbstractTask
    {
        public Guid ID { get; set; }
        protected List<ITaskObserver> observers = new List<ITaskObserver>();

        public abstract void Run();

        public void addObserver(ITaskObserver e)
        {
            observers.Add(e);
        }

        public void removeObserver(ITaskObserver e)
        {
            observers.Remove(e);
        }

    }
}
