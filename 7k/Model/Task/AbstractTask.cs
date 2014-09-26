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

        public void duplicate()
        {
            Type t = GetType();

            AbstractTask task = (AbstractTask)Activator.CreateInstance(t);

            preDuplicate(task);

            copyOptions(task);

            postDuplicate(task);
        }
        
        protected abstract void preDuplicate(AbstractTask task);
        protected abstract void postDuplicate(AbstractTask task);
        
        private void copyOptions(AbstractTask task)
        {
            // TODO beállítás másolás kifejtése
            throw new NotImplementedException();
        }
    }
}
