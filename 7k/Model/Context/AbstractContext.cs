﻿using _7k.Model.ContextElement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7k.Model.Context
{
    abstract class AbstractContext
    {
        public Type Tp { get; private set; }
        public String Name { get; set; }
        public String Description { get; set; }

        public AbstractContext()
        {
            Tp = this.GetType();

            // TODO 1
            //Name = MultiLanguageTextProxy.GetText("OptionType_" + key.ToString() + "_Name", key.ToString());
            //Description = MultiLanguageTextProxy.GetText("OptionType_" + key.ToString() + "_Description", key.ToString());                
        }

        public abstract AbstractContext DeepCopy();

        public abstract Boolean EqualContexts(object obj);
    }
}
