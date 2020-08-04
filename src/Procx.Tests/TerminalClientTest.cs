using System;
using System.Threading.Tasks;
using Xunit;

namespace Procx.Tests
{
    public class TerminalClientTest
    {
        private TraceWriter _trace = new TraceWriter();

        [Fact]
        public async Task Should_Excute_Command()
        {
            using (var client = new TerminalClient(_trace))
            {
                var exitCode = await client.ExcuteAsync(@"C:\", "cmd.exe", "/c git --version");
                
                Assert.Equal(0, exitCode);
            }
        }


        [Fact]
        public async Task Should_Excute_Multiple_Command()
        {
            using (var client = new TerminalClient(_trace))
            {
                var exitCode = await client.ExcuteAsync(@"C:\", "cmd.exe", "/c git --version");
                
                exitCode = await client.ExcuteAsync(@"C:\Repos", "cmd.exe", "/c dir");

                Assert.Equal(0, exitCode);
            }
        }

        [Fact]
        public async Task Should_Excute_Long_Command()
        {
            using (var client = new TerminalClient(_trace))
            {
                var exitCode = await client.ExcuteAsync(@"C:\", "cmd.exe", null);

                Assert.Equal(0, exitCode);
            }
        }
    }
}
