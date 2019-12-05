using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApp1
{
    class PProcess
    {
        private Process[] processes;

        public void Initialize()
        {
            processes = Process.GetProcesses();
        }

        public string GetProcessNameFromID(int _pid)
        {
            string result = "";
            foreach (Process process in processes)
            {
                if (_pid == process.Id)
                {
                    result = process.ProcessName;
                    break;
                }
            }
            return result;
        }
       public static void KillProcess(int _pid)
        {
            try
            {
                ProcessStartInfo proInfo = new System.Diagnostics.ProcessStartInfo();
                Process pro = new System.Diagnostics.Process();

                proInfo.FileName = @"cmd";
                proInfo.CreateNoWindow = true;
                proInfo.UseShellExecute = false;
                proInfo.RedirectStandardOutput = false;
                proInfo.RedirectStandardInput = true;
                proInfo.RedirectStandardError = false;

                pro.StartInfo = proInfo;
                pro.Start();

                pro.StandardInput.Write(Application.StartupPath + "\\KillProcess " + _pid.ToString() + Environment.NewLine);
                pro.StandardInput.Close();

                pro.WaitForExit();
                pro.Close();
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }

}
