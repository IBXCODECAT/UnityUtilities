using System;
using System.Runtime.InteropServices;

// A native windows alert/Messagebox
/// <see>https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-messagebox</see>

public static class NativeWinAlert
{
    public struct interupt
    {
        public static long app = 0x00000000L;
        public static long sys = 0x00001000L;
        public static long task = 0x00002000L;
        public static long help = 0x00004000L;
    }

    public struct Icons
    {
        public static long error = 0x00000010L;
        public static long querry = 0x00000020L;
        public static long warn = 0x00000030L;
        public static long info = 0x00000040L;
    }

    public struct Options
    {
        public static long ok = 0x00000000L;
        public static long okCancel = 0x00000001L;
        public static long abortRetryIgnore = 0x00000002L;
        public static long yesNoCancel = 0x00000003L;
        public static long yesNo = 0x00000004L;
        public static long retryCancel = 0x00000005L;
        public static long cancelRetryContinue = 0x00000006L;
    }

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();
    public static IntPtr GetWindowHandle() { return GetActiveWindow(); }
    [DllImport("user32.dll", SetLastError = true)]
    static extern int MessageBox(IntPtr hwnd, String lpText, String lpCaption, uint uType);

    public static int Alert(string title, string text, long options, long icon)
    {
        try
        {
            return MessageBox(GetWindowHandle(), text, title, (uint)(options | icon));
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogException(ex);
            return -1;
        }
    }
}
