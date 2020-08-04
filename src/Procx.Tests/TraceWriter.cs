using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Procx.Tests
{
    public class TraceWriter : ITraceWriter
    {
        public void Info(string output)
        {
            Debug.WriteLine($"output ========= {output}");
        }
    }
}
