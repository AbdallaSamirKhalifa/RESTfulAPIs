using System.Runtime.InteropServices;

namespace Win32APIs
{
    public class ScreenResolution
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nIndex">0 for screen width and 1 for the screen hight</param>
        /// <returns>screen resolution</returns>
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);
        


    }
}
