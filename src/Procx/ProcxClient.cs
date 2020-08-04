using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Procx
{
    public partial class ProcxClient
    {

        private delegate bool ConsoleCtrlDelegate(ConsoleCtrlEvent CtrlType);

        private readonly ConcurrentQueue<string> _outputData = new ConcurrentQueue<string>();
        private readonly ConcurrentQueue<string> _errorData = new ConcurrentQueue<string>();
        private readonly AsyncManualResetEvent _outputProcessEvent = new AsyncManualResetEvent();
        private readonly TaskCompletionSource<bool> _processExitedCompletionSource = new TaskCompletionSource<bool>();

        private readonly TimeSpan _sigintTimeout = TimeSpan.FromMilliseconds(7500);
        private readonly TimeSpan _sigtermTimeout = TimeSpan.FromMilliseconds(2500);

        private Encoding _outputEncoding;
        private bool _waitingOnStreams = false;
        private Stopwatch _stopWatch;
        private Process _proc;
        private int _streamReadCount = 0;
        private StreamWriter _inputWriter;
        private readonly ITraceWriter _trace;

        private void InitProcess(string workingDir, string fileName, string args, bool killProccessOnCancel)
        {
            _trace?.Info("Starting process:");
            _trace?.Info($"  File name: '{fileName}'");
            _trace?.Info($"  Arguments: '{args}'");
            _trace?.Info($"  Working directory: '{workingDir}'");
            _trace?.Info($"  Encoding web name: {_outputEncoding?.WebName} ; code page: '{_outputEncoding?.CodePage}'");
            _trace?.Info($"  Force kill process on cancellation: '{killProccessOnCancel}'");

            _proc = new Process();
            _proc.StartInfo.WorkingDirectory = workingDir;
            _proc.StartInfo.FileName = fileName;
            _proc.StartInfo.Arguments = args;
            _proc.StartInfo.UseShellExecute = false;
            _proc.StartInfo.RedirectStandardOutput = true;
            _proc.StartInfo.RedirectStandardError = true;
            _proc.StartInfo.RedirectStandardInput = true;

            if (_outputEncoding != null)
            {
                _proc.StartInfo.StandardOutputEncoding = _outputEncoding;
                _proc.StartInfo.StandardErrorEncoding = _outputEncoding;
            }

            _proc.EnableRaisingEvents = true;
            _proc.Exited += OnProcessExited;
        }

        private void OnProcessExited(object sender, EventArgs e)
        {
            _trace?.Info($"Exited process {_proc.Id} with exit code {_proc.ExitCode}");

            if (_streamReadCount != 0)
            {
                _waitingOnStreams = true;

                Task.Run(async () =>
                {
                    // Wait 5 seconds and then Cancel/Kill process tree
                    await Task.Delay(TimeSpan.FromSeconds(5));

                    KillProcessTree();

                    _processExitedCompletionSource.TrySetResult(true);

                });
            }
            else
            {
                _processExitedCompletionSource.TrySetResult(true);
            }
        }

        private void KillProcessTree()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WindowsKillProcessTree();
            }
            else
            {
                NixKillProcessTree();
            }
        }

    }
}
