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

                client.OnOutput += OnOutput;

                await client.ExcuteAsync(@"C:\", "cmd.exe", "/c git --version");

                await client.ExcuteAsync(@"C:\Repos", "cmd.exe", "/c dir");


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
