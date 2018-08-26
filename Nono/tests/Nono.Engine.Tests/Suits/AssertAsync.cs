using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nono.Engine.Tests.Suits
{
    public static class AssertAsync
    {
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool CheckRemoteDebuggerPresent(IntPtr hProcess, ref bool isDebuggerPresent);

        public static void CompletesIn(int timeout, Action<CancellationToken> action)
        {
            bool isDebuggerPresent = false;
            CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, ref isDebuggerPresent);

            if (isDebuggerPresent)
            {
                action(default);
                return;
            }

            var cts = new CancellationTokenSource();
            cts.CancelAfter(timeout * 1000);

            Task.Run(() => action(cts.Token), cts.Token).GetAwaiter().GetResult();
        }
    }
}
