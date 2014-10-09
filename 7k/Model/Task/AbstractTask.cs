using _7k.Model.ContextElement;
using _7k.Model.ContextElement.Task.Option;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _7k.Model.ContextElement.Task
{
    // TODO 3 vannak meg kerdeses pontok a Task-okkal kapcsolatban
    abstract class AbstractTask : Notifier
    {
        public enum TaskState
        {
            Initialized, Running, Completed, Error
        }

        // delegate void workInUIThread();

        public Type Tp { get; private set; }

        public Guid ID { get; set; }

        public Boolean AutoStart { get; set; }
        //public Boolean AutoStart
        //{
        //    get { return _autoStart; }
        //    set
        //    {
        //        _autoStart = value;
        //        OnPropertyChanged("AutoStart");
        //        TaskManager.Instance.wakeUp();
        //    }
        //}

        //protected Boolean AutoStartWithDispatcher
        //{
        //    set
        //    {
        //        workInUIThread st = delegate
        //        {
        //            AutoStart = value;
        //        };
        //        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, st);

        //    }
        //}

        ///// <summary>
        ///// Már lehet indítani belsőleg feladatot, csak készíteni kell egy példányt, annak a silertRun kapcsolóját bekapcsolni
        ///// majd a feladat kódjában ha kell ehhez kötni a kiírásokat és egyebeket
        ///// + a validateandcorrectoption, getrequiredoptionlist készítésekor ez is figyelembe lett véve
        ///// </summary>
        //public Boolean silentRun = false;

        public String Name { get; set; }
        public String Decription { get; set; }

        public List<AbstractOption> Options { get; set; }

        public Double Percent { get; set; }
        public String PercentText
        {
            get { return Percent * 100 + "%"; }
        }
        public String Counter { get; set; }
        public String Error { get; set; }

        protected TaskState _state;
        public TaskState State
        {
            get { return _state; }
            set
            {
                _state = value;
                OnPropertyChanged("State");
            }
        }
        //protected CleanerTaskState StateWithDispatcher
        //{
        //    set
        //    {
        //        workInUIThread st = delegate
        //        {
        //            State = value;
        //        };
        //        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, st);

        //    }
        //}

        public AbstractTask()
        {
            Tp = this.GetType();

            Name = MultiLanguageTextProxy.GetText(Tp.Name + "_Name", Tp.Name);
            Name = MultiLanguageTextProxy.GetText(Tp.Name + "_Description", Tp.Name);

            Percent = -1;
            Counter = String.Empty;
            Error = String.Empty;

            Options = new List<AbstractOption>();
            ValidateAndAmendOptions();

            State = TaskState.Initialized;
        }

        // TODO 2 - Deserializaton?
        //public void OnDeserialization(Object sender)
        //{
        //    _state = CleanerTaskState.Wait;
        //    ValidateAndCorrectOptions();

        //    // optional fields
        //}

        public virtual List<AbstractOption> GetDefaultOptions() 
        {
            return new List<AbstractOption>();
        }
                
        public void duplicate()
        {
            AbstractTask task = (AbstractTask)Activator.CreateInstance(this.GetType());

            task.Name = this.Name;
            task.Decription = this.Decription;
            task.AutoStart = this.AutoStart;

            // TODO copy observer?

            task.ID = Guid.NewGuid();

            foreach (AbstractOption item in Options)
                task.Options.Add(item.DeepCopy());
        }

        // TODO 5 egyszerusiteni
        //   1) a viszatérési értéket ref paraméterként kapná? Object-ként?

        //   2)
        //protected R getOptionValue<T,R>(AbstractOption id) where T : AbstractOption
        //{
        //    foreach (AbstractOption item in this.options)
        //        if (item is T && item.Key.Equals(id)) return ((T)item).Value;

        //    throw new OptionNotFoundException();
        //}
        //

        protected Boolean getBooleanOptionValue(AbstractOption.OptionType id)
        {
            foreach (var item in this.Options)
                if (item is BooleanOption && item.Key.Equals(id)) return ((BooleanOption)item).Value;

            throw new OptionNotFoundException();
        }
        protected string getStringOptionValue(AbstractOption.OptionType id)
        {
            foreach (var item in this.Options)
                if (item is StringOption && item.Key.Equals(id)) return ((StringOption)item).Value;

            throw new OptionNotFoundException();
        }

        protected List<string> getStringListOptionValue(AbstractOption.OptionType id)
        {
            foreach (var item in this.Options)
                if (item is StringListOption && item.Key.Equals(id)) return ((StringListOption)item).Value;

            throw new OptionNotFoundException();
        }

        // TODO 4 - Lesz verziózás? Akkor kicsit bonyolultabb lesz.
        public void ValidateAndAmendOptions()
        {
            List<AbstractOption> defOptList = GetDefaultOptions();

            foreach (AbstractOption item in defOptList)
            {
                Boolean contain = false;
                foreach (AbstractOption opt in Options)
                    if(item.Key.Equals(opt.Key))
                    {
                        contain = true;
                        break;
                    }

                if (!contain) Options.Add(item);
            }
        }

        abstract protected void EmbemedStart();
        
        // TODO 2 - Felülvizsgálni, kiegészíteni a hibákat
        public void Run()
        {        
            try
            {
                this.State = TaskState.Running;
                EmbemedStart();
                this.AutoStart = false;
                this.State = TaskState.Completed;
                // TODO - 2 - Jo kerdes ez a torles itt, valoszineleg teljesen el kellene vetni a torles dolgot
                // TaskManager.Instance.deleteTaskWithDispatcherSyncron(this);
            }
            // TODO 2 - hibauzenetek nyelvesitese
            catch (StopTaskManagerException)
            {
                terminateTask("(A folyamat a felhasználó által megállítva)");
            }
            catch (NullReferenceException)
            {
                terminateTask("(Valószínűleg nincs kiválasztva word dokumentum)");
            }
            catch (InvalidSelectedDocumentException)
            {
                terminateTask("(Válassz ki egy Word dokumentumot, amin dolgozol)");
            }
            catch (ClosedDocumentOrApplicationException)
            {
                terminateTask("(A kiválasztott dokumentum vagy maga a Word bezárult)");
            }
            catch (InvalidCastException)
            {
                terminateTask("(A kiválasztott dokumentum vagy maga a Word bezárult)");
            }
            catch (UnidentifiedStyleException e)
            {
                terminateTask("(A dokumentumból hiányzik a " + e.Message + " stílus.)");
            }
            catch (ThreadAbortException)
            {
                terminateTask("(Megszakítva)");
            }
            catch (Exception e)
            {
                terminateTask("(Ismeretlen hiba történt: " + e.Message + ")");
            }
        }
        protected void terminateTask(string message)
        {
            Error = message;
            this.AutoStart = false;
            this.State = TaskState.Error;
        }
    }        
}
