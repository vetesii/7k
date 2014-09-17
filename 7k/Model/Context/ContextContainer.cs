using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _7k.Model.Task;

namespace _7k.Model.Context
{
    class ContextToTaskBound
    {
        public AbstractContext Context { get; set; }
        public AbstractTask Task { get; set; }
    }

    class ContextToManyTasksBound
    {
        public AbstractContext Context { get; set; }
        public List<AbstractTask> Task { get; set; }
    }

    static class ContextContainer
    {
        static List<ContextToTaskBound> ContextToTaskList { get; set; }

        static List<ContextToManyTasksBound> ContextToManyTasksList { get; set; }

        public static AbstractContext BaseContext { get; set; }

        public static void addNewOneToOneBound(AbstractContext cont, AbstractTask task)
        {
            if (cont == null || task == null) return;

            Boolean go = true;

            foreach (ContextToTaskBound item in ContextToTaskList)
                if(item.Task.Equals(task) && item.Context.Equals(cont))
                {
                    go = false;
                    break;
                }

            if(go) ContextToTaskList.Add(new ContextToTaskBound() { Context = cont, Task = task });
        }

        public static void addNewOneToManyBound(AbstractContext cont, List<AbstractTask> taskList)
        {
            if (cont == null || taskList == null) return;


        }
    }
}
