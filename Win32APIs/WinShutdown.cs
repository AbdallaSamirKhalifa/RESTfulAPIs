using System;
using System.Runtime.InteropServices;
using System.ComponentModel;


public static class WinShutdown
{

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool ExitWindowsEx(uint uFlag, uint dwReason);

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool OpenProcessToken(IntPtr ProcessHandle, uint DesiredAccess, out IntPtr TokenHandle);

    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern bool LookupPrivilageValue(string lpSystemName, string lpName, out LUID lpLuid);

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool AdjustTokenPrivilages(IntPtr TokenHandle, bool DisableAllPrivileges, ref TOKEN_PRIVILEGES NewState
        , int BufferLength, IntPtr PreviousState, IntPtr ReturnLength);

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct LUID
    {
        public uint LowPart;
        public int HighPart;
    }

    private struct TOKEN_PRIVILEGES
    {
        public int PrivilegeCount;
        public LUID Luid;
        public int Attributes;
    }

    const int SE_PRIVILEGE_ENABLED = 0x00000002;
    const int TOKEN_QUERY = 0x00000008;
    const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
    const uint EWX_LOGOFF = 0x00000000;
    const uint EWX_SHUTDOWN = 0x00000001;
    const uint EWX_REBOOT = 0x00000002;
    const uint EWX_FORCE = 0x00000004;

    private static void EnableShutdownPrivelege()
    {
        if( !OpenProcessToken(System.Diagnostics.Process.GetCurrentProcess().Handle,
            TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, out IntPtr tokenHandle))
        {
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }
        if(!LookupPrivilageValue(null, "SeShutdownPrivilege", out LUID luid))
        {
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }
        TOKEN_PRIVILEGES tp= new TOKEN_PRIVILEGES
        {
            PrivilegeCount = 1,
            Luid = luid,
            Attributes = SE_PRIVILEGE_ENABLED
        };

        if(!AdjustTokenPrivilages(tokenHandle,false, ref tp, 0, IntPtr.Zero, IntPtr.Zero))
        {
            throw new Win32Exception(Marshal.GetLastWin32Error());

        }
        int ERROR_NOT_ALL_ASSIGNED = 1300;
        int error= Marshal.GetLastWin32Error();
        if (error== ERROR_NOT_ALL_ASSIGNED)
        {
            throw new Win32Exception(error, "The token does not have one or more of the requested privileges.");
        }

    }

    public static void Shutdown()
    {
        EnableShutdownPrivelege();

        if(!ExitWindowsEx(EWX_SHUTDOWN | EWX_FORCE, 0))
        {
            throw new Win32Exception(Marshal.GetLastWin32Error(), "Failed to initiate system shutdown.");
        }
    }
}

