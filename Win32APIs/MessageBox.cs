using System;
using System.Runtime.InteropServices;

public class MessageBoxAPI
{
    public enum enMessgeBoxType { }
    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern int MessageBox(IntPtr hWnd, string text, string caption, int type);

    public static void MessageBox(string message, string caption, int type)
    {
        MessageBox(IntPtr.Zero, message, caption, type);
    }
}

