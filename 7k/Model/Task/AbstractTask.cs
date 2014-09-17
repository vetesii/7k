using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7k.Model.Task
{
    abstract class AbstractTask
    {
        public Guid ID { get; set; }
        protected List<ITaskObserver> observers = new List<ITaskObserver>();

        public void addObserver(ITaskObserver e)
        {
            observers.Add(e);
        }
        public void removeObserver(ITaskObserver e)
        {
            observers.Remove(e);
        }

        public Boolean AutoStartState { get; set; }

        public String Name { get; set; }
        public String Decription { get; set; }


        public abstract void Run();
    }
}
