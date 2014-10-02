using _7k.Model.Task;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace _7k.Model
{
    class Scheduler
    {
        private Object schedulerLock = true;

        protected Scheduler() { }
        public static Scheduler Instance { get; protected set; }
        static Scheduler()
        {
            Instance = new Scheduler();            
        }

        // TODO 1 külön szálon futtatni
        public void Start()
        {
            try
            {
                while (true)
                {
                    AbstractTask tsk;

                    lock (schedulerLock)
                    {
                        tsk = CentralStorage.GetNextTask();
                    }

                    // TODO 1 megnezni
                    //if (_tasks.Count() > 0 && _tasks[0].State != CleanerTaskState.Work && _tasks[0].AutoStart == true)

                    // TODO 2 Kell ide is lock, az egész köré? Vagy két lock kell?
                    // Lehet hogy a tsk-t kellene lock-olni?
                    if(tsk != null)
                    {
                        tsk.Run();
                    }
                    else
                    {
                        lock (schedulerLock)
                        {
                            // TODO 1 megnezni
                            //state = TaskManagerState.Stand;
                            Monitor.Wait(schedulerLock);
                            //state = TaskManagerState.Work;
                        }
                    }
                }
            }
            catch
            {
                // TODO 2 restart hivas
            }
        }
    }


    // TODO 4 - Eltuntetni, ha mar nem kell

    //class TaskManager : Notifier
    //{
    //    protected int tmCounter = 0;

    //    private Boolean automaticStartToTask;
    //    public Boolean AutomaticStartToTask
    //    {
    //        get
    //        {
    //            return automaticStartToTask;
    //        }
    //        set
    //        {
    //            automaticStartToTask = value;
    //            OnPropertyChanged("AutomaticStartToTask");
    //        }
    //    }


    //    protected ObservableCollection<AbstractCleanerTask> _tasks;
    //    public ObservableCollection<AbstractCleanerTask> Tasks
    //    {
    //        get { return _tasks; }
    //        set
    //        {
    //            _tasks = value;
    //            OnPropertyChanged("Tasks");
    //        }
    //    }

    //    protected TaskManagerState state;
    //    private Object restartLock = true;

    //    protected Thread WorkerThread;
    //    delegate void workInUIThread();

    //    private void startTaskManager()
    //    {
    //        WorkerThread = new Thread(work);
    //        WorkerThread.IsBackground = true;
    //        WorkerThread.Name = "TM" + tmCounter++;
    //        WorkerThread.Start();

    //        state = TaskManagerState.Work;
    //    }
    //    //public void restartTaskManager()
    //    //{
    //    //    lock (restartLock)
    //    //    {
    //    //        if (state.Equals(TaskManagerState.Restart)) return;
    //    //        state = TaskManagerState.Restart;
    //    //    }

    //    //    startTaskManager();
    //    //}
    //    public void stopTaskManager()
    //    {
    //        lock (restartLock)
    //        {
    //            if (state.Equals(TaskManagerState.Restart) || state.Equals(TaskManagerState.UserRestart)) return;
    //            state = TaskManagerState.UserRestart;
    //        }       

    //        try { WorkerThread.Abort(); }
    //        catch (Exception) { }
            
    //        if (_tasks.Count() != 0)
    //        {
    //            _tasks[0].AutoStart = false;
    //        }
    //    }

    //    public void wakeUp()
    //    {
    //        lock (_tasks)
    //        {
    //            Monitor.Pulse(_tasks);
    //        }
    //    }
    //    protected void wakeUpWithOtherThread()
    //    {
    //        Thread nT = new Thread(wakeUp);
    //        nT.Start();
    //    }

    //    void work()
    //    {
    //        try
    //        {
    //            while (true)
    //            {
    //                if (_tasks.Count() > 0 && _tasks[0].State != CleanerTaskState.Work && _tasks[0].AutoStart == true)
    //                {
    //                    Guid id = Guid.NewGuid();

    //                    _tasks[0].Start(id);
    //                }
    //                else
    //                {
    //                    lock (_tasks)
    //                    {
    //                        state = TaskManagerState.Stand;
    //                        Monitor.Wait(_tasks);
    //                        state = TaskManagerState.Work;
    //                    }
    //                }
    //            }
    //        }
    //        catch (Exception)
    //        {
    //            lock (restartLock)
    //            {
    //                if (state.Equals(TaskManagerState.Restart)) return;
    //                workInUIThread st = delegate { state = TaskManagerState.Restart; };
    //                try
    //                {
    //                    System.Windows.Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, st);
    //                }
    //                catch (NullReferenceException) { /* if application has already closed*/ }
    //            }

    //            //TODO megvizsgálni, hogy van-e még főszál és akkor nem kellene ide try (+ a fenti)

    //            // start a new task manager thread in UI thread [async]
    //            workInUIThread stm = delegate { startTaskManager(); };
    //            try
    //            {
    //                System.Windows.Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, stm);
    //            }
    //            catch (NullReferenceException) { /* if application has already closed*/ }
    //        }
    //    }


    //    static private TaskManager _instance;
    //    static public TaskManager Instance
    //    {
    //        get { return _instance; }
    //    }
    //    static TaskManager()
    //    {
    //        _instance = new TaskManager();
    //    }
    //    private TaskManager()
    //    {
    //        _tasks = new ObservableCollection<AbstractCleanerTask>();
    //        state = TaskManagerState.UserRestart;

    //        AutomaticStartToTask = true;

    //        startTaskManager();
    //    }

        



    //    // Task list functions
    //    public void addTask(AbstractCleanerTask task)
    //    {
    //        AbstractCleanerTask newT = task.DeepCopy();
    //        //newT.AutoStart = AutomaticStartToTask;

    //        Tasks.Add(newT);

    //        wakeUpWithOtherThread();
    //    }
    //    public void addTask(CleanerTaskPackage list)
    //    {
    //        foreach (var item in list.Tasks)
    //        {
    //            AbstractCleanerTask newT = item.DeepCopy();
    //            //newT.AutoStart = AutomaticStartToTask;

    //            Tasks.Add(newT);                               
    //        }
    //        wakeUpWithOtherThread();
    //    }
    //    public void moveUpTask(AbstractCleanerTask task)
    //    {
    //        try
    //        {
    //            int place = _tasks.IndexOf(task);
    //            if (place < 1) return;
    //            if (_tasks[place].State.Equals(CleanerTaskState.Work) || _tasks[place - 1].State.Equals(CleanerTaskState.Work))
    //                return;
    //            AbstractCleanerTask ch = _tasks[place - 1];
    //            _tasks[place - 1] = _tasks[place];
    //            _tasks[place] = ch;
    //        }
    //        catch (Exception) { }
    //        OnPropertyChanged("Tasks"); // a felület is értesül az új elemről

    //        wakeUpWithOtherThread();
    //    }
    //    public void moveDownTask(AbstractCleanerTask task)
    //    {
    //        try
    //        {
    //            int place = _tasks.IndexOf(task);
    //            if (place < 0 || place > _tasks.Count - 2) return;
    //            if (_tasks[place].State.Equals(CleanerTaskState.Work) || _tasks[place + 1].State.Equals(CleanerTaskState.Work))
    //                return;
    //            AbstractCleanerTask ch = _tasks[place + 1];
    //            _tasks[place + 1] = _tasks[place];
    //            _tasks[place] = ch;
    //        }
    //        catch (Exception) { }
    //        OnPropertyChanged("Tasks"); // a felület is értesül az új elemről

    //        wakeUpWithOtherThread();
    //    }
    //    public void deleteTask(AbstractCleanerTask task)
    //    {
    //        try
    //        {
    //            if (task.State.Equals(CleanerTaskState.Work)) return;
    //            _tasks.Remove(task);
    //        }
    //        catch { }
    //        OnPropertyChanged("Tasks"); // a felület is értesül az új elemről

    //        wakeUpWithOtherThread();
    //    }
    //    public void deleteTask(int i)
    //    {
    //        try
    //        {
    //            if (_tasks[i].State.Equals(CleanerTaskState.Work)) return;
    //            _tasks.RemoveAt(i);
    //        }
    //        catch { }
    //        OnPropertyChanged("Tasks"); // a felület is értesül az új elemről

    //        wakeUpWithOtherThread();
    //    }
    //    public void emptyTaskList()
    //    {
    //        if (state.Equals(TaskManagerState.Work)) return;
    //        _tasks = new ObservableCollection<AbstractCleanerTask>();
    //        OnPropertyChanged("Tasks");
    //    }


    //    public void deleteTaskWithDispatcherSyncron(AbstractCleanerTask item)
    //    {
    //        workInUIThread state = delegate { deleteTask(item); };
    //        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, state);
    //    }
    //}
}
