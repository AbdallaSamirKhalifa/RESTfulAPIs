using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Win32APIs
{
    public class WifiScanner
    {
        [DllImport("Wlanapi.dll")]
        private static extern uint WlanOpenHandle(uint dwClientVersion, IntPtr pReserved,
            out uint pdwNegotiatedVersion, out IntPtr phClientHandle);

        [DllImport("Wlanapi.dll")]
        private static extern uint WlanEnumInterface(IntPtr hClientHandle, IntPtr pReservedm, out IntPtr ppInterfaceList);

        [DllImport("Wlanapi.dll")]
    
        private static extern uint WlanCloseHandle(IntPtr hClientHandle, IntPtr pReserved);
        [DllImport("Wlanapi.dll")]

        private static extern uint WlanFreeMemory(IntPtr pMemory);

        private IntPtr clientHandle = IntPtr.Zero;
        public WifiScanner()
        {
            uint negotiatedVersion;
            WlanOpenHandle(2, IntPtr.Zero, out negotiatedVersion, out clientHandle);
        }

        ~WifiScanner()
        {
            WlanCloseHandle(clientHandle, IntPtr.Zero);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct WlanInterfaceInfoListHeader
        {
            public uint dwNumberOfItems;
            public uint dwIndex;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct WlanInterfaceInfo
        {
            public Guid InterfaceGuid;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string strProfileName;
        }

        public List<string> GetAvailableNetworks()
        {
            IntPtr interfaceList = IntPtr.Zero;
            WlanEnumInterface(clientHandle, IntPtr.Zero, out interfaceList);
        
            var listHeader = (WlanInterfaceInfoListHeader)Marshal.PtrToStructure(interfaceList,
                typeof(WlanInterfaceInfoListHeader));
            var wlanInterfaceInfo = new WlanInterfaceInfo[listHeader.dwNumberOfItems];
            List <string> networkList = new List <string>();

            for (int i = 0; i < listHeader.dwNumberOfItems; i++)
            {
                IntPtr interfaceInfoPtr = new IntPtr(interfaceList.ToInt64() +
                    (i * Marshal.SizeOf(typeof(WlanInterfaceInfo))) + Marshal.SizeOf(typeof(int)));
                wlanInterfaceInfo[i] = (WlanInterfaceInfo)Marshal.PtrToStructure (interfaceInfoPtr, 
                    typeof(WlanInterfaceInfo));
                networkList.Add(wlanInterfaceInfo[i].strProfileName);

            }
        
            WlanFreeMemory(interfaceList);
            return networkList;
        }
    }
}
