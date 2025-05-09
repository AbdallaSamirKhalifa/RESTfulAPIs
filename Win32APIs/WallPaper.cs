using System.Runtime.InteropServices;

public class Wallpaper
{
    //Import the SystemParametersInfo function from User32.dll
    [DllImport("user32.dll", CharSet = CharSet.Auto)]

    public static extern int SystemnParametersaInfo(uint action, uint uParam, string vParam, uint winIni);

    //constants for the function
    public static readonly uint SPI_SETDESKWALLPAPER = 0x14;
    public static readonly uint SPIF_UPDATEINFILE = 0x01;
    public static readonly uint SPIF_SENDCHANGE = 0x02;

    public static void SetWallPaper(string ImagePath)
    {
        SystemnParametersaInfo(SPI_SETDESKWALLPAPER, 0, ImagePath, SPIF_UPDATEINFILE | SPIF_SENDCHANGE);
    }
}

