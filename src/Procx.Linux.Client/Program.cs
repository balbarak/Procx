using System;
using System.Threading.Tasks;

namespace Procx.Linux.Client
{
    class Program
    {
        private static TraceWriter _trace = new TraceWriter();

        static async Task Main(string[] args)
        {
            using (var client = new TerminalClient(_trace))
            {
                client.OnOutput += OnOutput;

                await client.ExcuteAsync("/etc", "ifconfig", null);
            }
        }

        private static void OnOutput(object sender, string e)
        {
            Console.WriteLine(e);
        }
    }

    public class TraceWriter : ITraceWriter
    {
        public void Info(string output)
        {
            Console.WriteLine(output);
        }
    }
}
