﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Procx
{
    internal class ProcessTermination
    {
        public ProcessTermination(int pid, bool expanded)
        {
            Pid = pid;
            ChildPidExpanded = expanded;
        }

        public int Pid { get; }
        public bool ChildPidExpanded { get; }
    }
}
