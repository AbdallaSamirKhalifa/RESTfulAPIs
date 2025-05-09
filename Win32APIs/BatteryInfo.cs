using System.Runtime.InteropServices;


public class BatteryInfo
{
    //define the SYSTEM_POWER_STATUS structure with fields as per the windows API documentation
    [StructLayout(LayoutKind.Sequential)]

    public struct SYSETEM_POWER_STATUS
    {
        public byte ACLineStatus;
        public byte BatteryFlag;
        public byte BatteryLifePercent;
        public byte Reserved1;
        public int BatteryLifeTime;
        public int BatteryFullLifeTime;
    }


    //Import the GetSystemPowerStatus API from Kernel32.dll
    [DllImport("Kernel32.dll",SetLastError =true)]
    public static extern bool GetSystemPowerStatus(out  SYSETEM_POWER_STATUS SPS);

    public static string GetBatteryStatus(byte flag)
    {
        switch(flag)
        {
            case 1:
                return "High";
            case 2:
                return "Low";
            case 4:
                return "Critical";
            case 8:
            return "Charging";
            case 128:
                return "No Battery";
            case 255:
            return "Uknown Status";
            default:
                return "Battery status not detected";
                
        }
    }

    /// <summary>
    /// Calls the GetSystemPowerStatus and outputs the values
    /// </summary>
    /// <param name="LineStatus">string output weather online (connected to the power) or offline (not connected)</param>
    /// <param name="BatteryStatus">string output 1- (High -> more than 66%) 2- (Low -> less than 33%)
    /// 3- (Critical -> less than 5%) 4- (Charging) 5- (No battery) 6- (Uknown status) 7- (couldn't dectec the status)</param>
    /// <param name="BatteryLifePercent">int output wheather -1 which means (Uknown) or the battery life percentage.</param>
    /// <param name="BatteryLifeTime">int output wheather -1 (which means Uknown) or the battery life time in seconds</param>
    /// <param name="BatteryFullLifeTime">int output wheather -1 (which means Uknown) or the battery full life time in seconds</param>
    /// <returns></returns>
    public static bool BatteryStatus(out string LineStatus, out string BatteryStatus, 
        out int BatteryLifePercent, out int BatteryLifeTime, 
        out int BatteryFullLifeTime)
    {
        if (GetSystemPowerStatus(out SYSETEM_POWER_STATUS sps))
        {
            LineStatus = sps.ACLineStatus == 0 ? "Offline" : "Online";
            BatteryStatus = GetBatteryStatus(sps.BatteryFlag);
            BatteryLifePercent = sps.BatteryLifePercent ==255? -1: sps.BatteryLifePercent;
            BatteryLifeTime = sps.BatteryLifeTime;
            BatteryFullLifeTime = sps.BatteryFullLifeTime;
            return true;
        }
        LineStatus = null;
        BatteryStatus = null;
        BatteryLifePercent = 0;
        BatteryLifeTime=0;
        BatteryFullLifeTime= 0;
        return false;
    }

}

