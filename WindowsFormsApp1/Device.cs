using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Device
    {
        private static string ACTION_ON = "Enable";
        private static string ACTION_OFF = "Disable";

        private static string scope = "root\\CIMV2";
        private ManagementObject classInstance;

        public Device(string _querystring)
        {
            // "Win32_PnPEntity.DeviceID='ACPI_HAL\PNP0C08\0'"
            try
            {
                classInstance =
                        new ManagementObject(
                            scope,
                            _querystring,
                            null);
            }catch(Exception)
            {
                ;
            }
        }

        public void On()
        {
            try
            {
                ManagementBaseObject outParams =
                       classInstance.InvokeMethod(ACTION_ON, null, null);
            }catch(Exception)
            {
                ;
            }

        }

        public void Off()
        {
            try
            {
                ManagementBaseObject outParams =
                       classInstance.InvokeMethod(ACTION_OFF, null, null);
            }
            catch(Exception)
            {
                ;
            }
        }
    }

    class FindDevice
    {
        private ManagementObjectSearcher searcher;
        private string scope = "root\\CIMV2";
        private string querystring;

        public FindDevice(string _querystring)
        {
            querystring = _querystring;
            searcher = new ManagementObjectSearcher(scope, querystring);
        }

        public List<string> GetResults(string _searchselector, List<string> _matchstring, string _findselector)
        {
            List<string> result = new List<string>();

            foreach (ManagementObject queryObj in searcher.Get())
            {
                foreach (string s_match in _matchstring)
                {
                    try
                    {
                        bool except_cast = false;
                        try
                        {
                            if (((string)queryObj[_searchselector]).ToUpper().Contains(s_match.ToUpper()))
                            {
                                result.Add((string)queryObj[_findselector]);
                            }
                        }catch(InvalidCastException)
                        {
                            except_cast = true;
                        }
                        if (except_cast)
                        {
                                var datas = queryObj[_searchselector] as Array;
                                foreach (string data in datas)
                                {
                                    if (data.ToUpper().Contains(s_match.ToUpper()) && (data.Length==s_match.Length))
                                    {
                                        if(!result.Contains((string)queryObj[_findselector]))
                                            result.Add((string)queryObj[_findselector]);
                                    }
                                }
                        }
                    }
                    catch(NullReferenceException)
                    {
                        ;
                    }
                }
            }

            return result;
        }
    }
}
