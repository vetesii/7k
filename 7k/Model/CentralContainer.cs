using _7k.Model.Context;
using _7k.Model.ContextElement.Task;
using _7k.Model.ContextElement.Task.InnerDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _7k.Model.ContextElement
{
    static class CentralContainer
    {
        static public readonly Object TaskListLock = null;
        static public readonly Object RunningTaskLock = null;

        // Base lists
        public static List<AbstractTask> DefaultTaskList { get; private set; }
        public static List<AbstractTask> CustomTaskList { get; private set; }
        public static List<AbstractTask> CustomCodeBaseTaskList { get; private set; }

        // Scheduler tasks
        public static List<Unity> ClosedTaskList { get; set; }
        public static Unity RunningTask { get; set; }
        public static List<Unity> PriorityTaskList { get; set; }
        public static List<Unity> NormalTaskList { get; set; }

        static CentralContainer()
        {
            DefaultTaskList = new List<AbstractTask>();
            CustomTaskList = new List<AbstractTask>();
            CustomCodeBaseTaskList = new List<AbstractTask>();

            ClosedTaskList = new List<Unity>();
            PriorityTaskList = new List<Unity>();
            NormalTaskList = new List<Unity>();

            RunningTask = null;
        }

        public static void PrepareNextRunnningTask()
        {
            // TODO 1 - Meg kell nézni, hogy a GUI dispatcherrel indítva ez nem lesz-e gond: kell-e tasklistlock is

            lock (RunningTask)
            {
                lock (TaskListLock)
                {
                    if (RunningTask == null)
                    {
                        if (PriorityTaskList.Count > 0) RunningTask = PriorityTaskList[0];
                        else if (NormalTaskList.Count > 0) RunningTask = NormalTaskList[0];
                    }

                    Monitor.Pulse(TaskListLock);
                }
            }
        }

        private static void createBaseTaskList()
        {
            DefaultTaskList = new List<AbstractTask>() 
            {
                new IdnDashChange(),
                new IdnRemoveEmptyParagraphBeforeAndAfterStyles()
            };
        }

        public static void AddTaskToPriorityList(Unity uni)
        {
            if (uni == null) return;
            if (uni.task == null) return;

            Unity last = null;
            try
            {
                last = PriorityTaskList[PriorityTaskList.Count];
            }
            catch (Exception)
            {
                try
                {
                    last = ClosedTaskList[ClosedTaskList.Count];
                }
                catch (Exception) { }
            }

            PriorityTaskList.Add(uni);

            NormalTaskList.Remove(uni);

            lock (CentralContainer.RunningTaskLock)
            {
                Monitor.Wait(CentralContainer.RunningTaskLock);
            }
        }

        public static void RemoveTaskFromPriorityList(Unity uni)
        {
            if (uni != null) PriorityTaskList.Remove(uni);
        }

        public static void RemoveTaskFromNormalList(Unity uni)
        {
            if (uni != null) NormalTaskList.Remove(uni);
        }

        public static List<AbstractContext> GetAbstractContextList(AbstractTask task)
        {
            List<AbstractContext> lst = new List<AbstractContext>();
            


            return lst;
        }
    }
}
