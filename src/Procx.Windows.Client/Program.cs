using System;
using System.Threading.Tasks;

namespace Procx.Windows.Client
{
    class Program
    {
        private static TraceWriter _trace = new TraceWriter();

        static async Task Main(string[] args)
        {
            using (var client = new TerminalClient(_trace))
            {
                var workingDir = @"C:\";
                var fileName = "cmd.exe";
                var cmd = "/c dir";

                client.OnOutput += OnOutput;

                await client.ExcuteAsync(workingDir, fileName, cmd);

            }
        }

        private static void OnOutput(object sender, string e)
        {
            Console.WriteLine(e);
        }
    }
}
