using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7k.Model.Message
{
    class TaskState : AbstractMessage
    {
        public String TaskName { get; set; }
        public Double Percent { get; set; }
        public String PercentText 
        {
            get { return Percent * 100 + "%"; }
        }
        public long Counter { get; set; }

    }
}
