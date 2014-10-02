using _7k.Model.Task;
using _7k.Model.Task.InnerDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7k.Model
{
    /// TODO 1 - Egyesével indítás: itt kell egy elkülönített részt csinálni, amit kiemelten lehet
    /// valami olyan lista, amit mindenképp lehet 
    /// felületen meg valami soron kívűli jelöléssel ellátni, amit ebbe a listába tesz
    /// számozás, szinezés

    class CentralStorage
    {
        public static List<AbstractTask> DefaultTaskList { get; protected set; }
        public static List<AbstractTask> CustomTaskList { get; protected set; }

        public static List<AbstractTask> CustomCodeBaseTaskList { get; protected set; } 

        public static List<AbstractTask> PriorityTaskList { get; set; }
        public static List<AbstractTask> NormalTaskList { get; set; }
        public static List<AbstractTask> ClosedTaskList { get; set; } 

        public static AbstractTask GetNextTask()
        {
            // TODO 1 make return algoritm

            if (PriorityTaskList.Count > 0)
            {
                return PriorityTaskList[0];
            }
            else if(NormalTaskList.Count > 0)
            {
                return NormalTaskList[0];
            }
            else return null;

             
        }

        static CentralStorage()
        {
            DefaultTaskList = new List<AbstractTask>();
        }

        protected void createBaseTaskList()
        {
            DefaultTaskList = new List<AbstractTask>() 
            {
                new IdnDashChange(),
                new IdnRemoveEmptyParagraphBeforeAndAfterStyles()
            };
        }
    }
}
