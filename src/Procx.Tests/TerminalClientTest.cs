using System;
using System.Collections.Generic;
using System.IO;
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

        [Fact]
        public async Task Should_Excute_Cmd_And_Wait_Result()
        {
            using (var client = new TerminalClient(_trace))
            {
                var result = await client.ExcuteAndReadOutputAsync(@"C:\", "cmd.exe", "/c dir");

                Assert.NotNull(result);
            }
        }

        [Fact]
        public async Task Should_Excute_When_No_Args()
        {
            using (var client = new TerminalClient())
            {
                var result = await client.ExcuteAndReadOutputAsync(@"C:\", "cmd.exe", "/c dir");

                Assert.NotNull(result);
            }
        }

        [Fact]
        public async Task Should_Set_Env_Variable()
        {
            var value = "Yeah it is work";

            var env = new Dictionary<string, string>()
            {
                ["TestVar"] = value
            };

            using (var client = new TerminalClient(_trace, env))
            {
                var result = await client.ExcuteAndReadOutputAsync(@"C:\", "powershell.exe", "/c echo $Env:TestVar");

                result = result
                    .Replace("\n", "")
                    .Replace("\r", "");

                Assert.Equal(value, result);
            }
        }

        [Fact]
        public async Task Should_Excute_When_No_Cmd()
        {
            using (var client = new TerminalClient())
            {
                var result = await client.ExcuteAndReadOutputAsync(@"C:\", "ipconfig", "/all");

              
                Assert.NotNull(result);
            }
        }
    }
}
