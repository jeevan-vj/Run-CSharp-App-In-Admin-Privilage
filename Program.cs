using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace AdminPermission
{
    internal class Program
    {
        [DllImport("libc")]
        public static extern uint getuid();

        private static bool IsRunningAsAdmin()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                using var identity = WindowsIdentity.GetCurrent();
                var principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }

            return getuid() == 0;
        }

        private static void Main(string[] args)
        {
            Console.WriteLine((IsRunningAsAdmin()) ? "Running As Admin" : "Not Running As Admin");
            Console.ReadLine();
        }
    }
}