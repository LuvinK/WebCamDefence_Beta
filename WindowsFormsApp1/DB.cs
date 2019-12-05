using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public static class DB
    {
        public static List<string> WEBCAM_DEVICE_NAME = new List<string>{
            "webcam",
            "camera",
            "image"
        };
    }

    struct PROGRAM_STRING
    {
        public static string RD_BUTTON_ON = "실시간 감지 ON";
        public static string RD_BUTTON_OFF = "실시간 감지 OFF";
        public static string RDON = "실시간 탐지를 실행합니다.";
        public static string RDOFF = "실시간 탐지를 중지합니다.";
    }

    struct DEVICE_CLASS
    {
        public static string Win32_PnPEntity = "Win32_PnPEntity";
        public static string Win32_PnPSignedDriver = "Win32_PnPSignedDriver";
    }

    struct SEARCH_SELECTOR_TYPE
    {
        public static string Availability = "Availability";
        public static string Caption = "Caption";
        public static string ClassGuid = "ClassGuid";
        public static string CompatibleID = "CompatibleID";
        public static string ConfigManagerErrorCode = "ConfigManagerErrorCode";
        public static string ConfigManagerUserConfig = "ConfigManagerUserConfig";
        public static string CreationClassName = "CreationClassName";
        public static string Description = "Description";
        public static string DeviceID = "DeviceID";
        public static string ErrorCleared = "ErrorCleared";
        public static string ErrorDescription = "ErrorDescription";
        public static string HardwareID = "HardwareID";
        public static string InstallDate = "InstallDate";
        public static string LastErrorCode = "LastErrorCode";
        public static string Manufacturer = "Manufacturer";
        public static string Name = "Name";
        public static string PNPClass = "PNPClass";
        public static string PNPDeviceID = "PNPDeviceID";
        public static string PowerManagementCapabilities = "PowerManagementCapabilities";
        public static string PowerManagementSupported = "PowerManagementSupported";
        public static string Present = "Present";
        public static string Service = "Service";
        public static string Status = "Status";
        public static string StatusInfo = "StatusInfo";
        public static string SystemCreationClassName = "SystemCreationClassName";
        public static string SystemName = "SystemName";

        public static string PDO = "PDO";
        public static string DeviceName = "DeviceName";
    }
   
}
