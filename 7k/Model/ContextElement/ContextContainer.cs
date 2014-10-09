using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _7k.Model.ContextElement.Task;

namespace _7k.Model.ContextElement.ContextElement
{
    class ContextToManyTasksBound
    {
        public AbstractContextElement Context { get; set; }
        public List<AbstractTask> Task { get; set; }
    }

    /// <summary>
    /// Only one to many bound and base Context. Private bound is needless.
    /// </summary>
    static class ContextContainer
    {
        static List<ContextToManyTasksBound> BoundList { get; set; }

        public static AbstractContextElement BaseContext { get; set; }

        public static void addBound(AbstractContextElement cont, List<AbstractTask> taskList)
        {
            if (cont == null || taskList == null) return;

            ContextToManyTasksBound ex = null;
            foreach (ContextToManyTasksBound item in BoundList)
                if (item.Context == cont)
                {
                    ex = item;
                    break;
                }

            if (ex != null)
                foreach (AbstractTask newTask in taskList)
                {
                    if (newTask == null) break;

                    Boolean go = true;
                    foreach (AbstractTask item in ex.Task)
                    {
                        if (item.Equals(newTask))
                        {
                            go = false;
                            break;
                        }
                    }

                    if (go) ex.Task.Add(newTask);
                }
            else
            {
                ex = new ContextToManyTasksBound() { Context = cont, Task = taskList };
                BoundList.Add(ex);
            }

            for (int i = 0; i < ex.Task.Count; i++)
                if (ex.Task[i] == null) ex.Task.RemoveAt(i);

            //if(ex.Task.Count) 
        }

        public static void addBound(AbstractContextElement cont, AbstractTask task)
        {
            addBound(cont, new List<AbstractTask>() { task });
        }
    }
}
