using _7k.Model.ContextElement;
using _7k.Model.ContextElement.ContextElement;
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
        public List<AbstractContextElement> Context { get; set; }
        public AbstractTask Task { get; set; }

        // TODO 3 - megtervezni a kontextus állípotot
        // például hordozhat olyan információt ami a megjelenítést befolyásolja:
        // szalagként leképzelve ezeket, bizonyos állapotoknál kevésbé feltünően jelennek meg stb...
        public ContextState CState { get; set; }
    }
}
