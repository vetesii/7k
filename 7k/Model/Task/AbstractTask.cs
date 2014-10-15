using _7k.Model.Context;
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

        public List<AbstractContext> Options { get; set; }

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

            Options = new List<AbstractContext>();
            ValidateAndAmendOptions();

            State = TaskState.Initialized;

            extContextList = new List<AbstractContext>();
        }

        protected List<AbstractContext> extContextList;

        protected void getContextsForRun()
        {
            extContextList = CentralContainer.GetAbstractContextList(this);
            if (extContextList == null) extContextList = new List<AbstractContext>();
        }

        // TODO 2 - Deserializaton?
        //public void OnDeserialization(Object sender)
        //{
        //    _state = CleanerTaskState.Wait;
        //    ValidateAndCorrectOptions();

        //    // optional fields
        //}

        public virtual List<AbstractContext> GetDefaultOptions() 
        {
            return new List<AbstractContext>();
        }
                
        public void duplicate()
        {
            AbstractTask task = (AbstractTask)Activator.CreateInstance(this.GetType());

            task.Name = this.Name;
            task.Decription = this.Decription;
            task.AutoStart = this.AutoStart;

            // TODO copy observer?

            task.ID = Guid.NewGuid();

            foreach (AbstractContext item in Options)
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

        protected Boolean getBooleanContextValue(BooleanContext.BCType id)
        {
            foreach (var item in this.Options)
                if (item is BooleanContext && (item as BooleanContext).ContextType.Equals(id)) return (item as BooleanContext).Value;

            throw new OptionNotFoundException();
        }
        protected string getStringOptionValue(StringContext.SCType id)
        {
            foreach (var item in this.Options)
                if (item is StringContext && (item as StringContext).ContextType.Equals(id)) return (item as StringContext).Value;

            throw new OptionNotFoundException();
        }
        protected List<string> getStringListOptionValue(StringListContext.SLType id)
        {
            foreach (var item in this.Options)
                if (item is StringListContext && (item as StringListContext).ContextType.Equals(id)) return (item as StringListContext).Value;

            throw new OptionNotFoundException();
        }

        // TODO 4 - Lesz verziózás? Akkor kicsit bonyolultabb lesz.
        public void ValidateAndAmendOptions()
        {
            List<AbstractContext> defOptList = GetDefaultOptions();

            foreach (AbstractContext item in defOptList)
            {
                Boolean contain = false;
                foreach (AbstractContext opt in Options)
                    if( item.EqualContexts(opt))
                    {
                        contain = true;
                        break;
                    }

                if (!contain) Options.Add(item);
            }
        }

        
        // TODO 2 - Felülvizsgálni, kiegészíteni a hibákat
        public void Run()
        {        
            try
            {
                this.State = TaskState.Running;
                SubAbstractTaskRun();
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
        abstract protected void SubAbstractTaskRun();

        protected void terminateTask(string message)
        {
            Error = message;
            this.AutoStart = false;
            this.State = TaskState.Error;
        }
    }        
}
