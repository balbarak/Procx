using System;
using System.Threading.Tasks;

namespace Procx.Linux.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var client = new TerminalClient())
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
}
