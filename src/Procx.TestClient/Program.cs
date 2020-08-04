using System;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Procx.TestClient
{
    class Program
    {
        private static TraceWriter _trace = new TraceWriter();

        static async Task Main(string[] args)
        {
           
            using (var client = new TerminalClient())
            {
                var workingDir = @"C:\";
                var fileName = "cmd.exe";
                var cmd = "/c dir";

                var result = await client.ExcuteAndReadOutputAsync(workingDir, fileName, cmd);
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
