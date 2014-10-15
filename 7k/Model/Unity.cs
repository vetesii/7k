using _7k.Model.Context;
using _7k.Model.ContextElement;
using _7k.Model.ContextElement.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7k.Model
{
    /// <summary>
    /// Megadja a Task-hoz tartozó 
    /// </summary>
    class Unity
    {
        public AbstractTask task;

        public AbstractContext context;

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}
