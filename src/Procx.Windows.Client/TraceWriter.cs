using System;
using System.Collections.Generic;
using System.Text;

namespace Procx.Windows.Client
{
    public class TraceWriter : ITraceWriter
    {
        public void Info(string output)
        {
            Console.WriteLine(output);
        }
    }
}
