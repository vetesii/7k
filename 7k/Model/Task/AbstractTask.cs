using _7k.Model;
using _7k.Model.Task.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7k.Model.Task
{
    abstract class AbstractTask : Notifier
    {
        public Type Tp { get; private set; }

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

        public List<AbstractOption> options = new List<AbstractOption>();

        public AbstractTask()
        {
            Tp = this.GetType();

            Name = MultiLanguageTextProxy.getTextOrDefaultText(Tp.Name + ".Name", Tp.Name);
            Name = MultiLanguageTextProxy.getTextOrDefaultText(Tp.Name + ".Description", Tp.Name);
        }

        public abstract List<AbstractOption> GetDefaultOptions();

        public abstract void Run();

        public void duplicate()
        {
            AbstractTask task = (AbstractTask)Activator.CreateInstance(this.GetType());

            task.Name = this.Name;
            task.Decription = this.Decription;
            task.AutoStartState = this.AutoStartState;

            // TODO copy observer?

            task.ID = Guid.NewGuid();

            foreach (AbstractOption item in options)
                task.options.Add(item.DeepCopy());
        }     
    }
}
