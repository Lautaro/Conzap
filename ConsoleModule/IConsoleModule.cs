﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleModule
{
    public interface IConsoleModule
    {
        string Title { get; }
        void Execute();
    }

 
}
