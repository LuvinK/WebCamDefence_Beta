using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.Management;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApp1
{
    abstract class PThread
    {
        protected Thread thread;
        public void Run() { thread = new Thread(new ThreadStart(ThreadProc)); thread.Start(); }

        public void Abort() { thread.Abort(); }
        public void Interrupt() { thread.Interrupt(); }
        protected abstract void ThreadProc();
    }

    class MonitorThraed:PThread
    {
        private ListView listview;
        private string DevicePDO;
        public MonitorThraed(ListView _listview) {
            listview = _listview;
        }

        [DllImport("Project1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Run(Byte[] HandleName);

        [DllImport("Project1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RunA();

        public void SetPDO(string _devicepdo) {
            DevicePDO = _devicepdo;
        }

        protected override void ThreadProc()
        {
            try
            {
                listview.Invoke(
                    new Form1.Delegate_ListViewUpdate(
                        Update_ListView),
                    new object[] { PROGRAM_STRING.RDON }
                   );
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

            while (true)
            {
                try
                {
                    PProcess pProcess = new PProcess();
                    pProcess.Initialize();
                    int pid = MonitorThraed.Run(Encoding.Default.GetBytes(DevicePDO));

                    if (pid > 0)
                    {
                        string pName = pProcess.GetProcessNameFromID(pid);
                        string sDetect = pName + "에서 탐지되었습니다.";

                        listview.Invoke(
                             new Form1.Delegate_ListViewUpdate(
                                 Update_ListView),
                             new object[] { sDetect }
                             );

                        if (MessageBox.Show(sDetect + "\n종료하시겠습니까?", "경고", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            PProcess.KillProcess(pid);
                        }
                        else
                        {
                            ;
                        }

                    }
                }
                catch (ManagementException)
                {
                    return;
                }
                catch(ThreadInterruptedException)
                {
                    return;
                }
                catch(ThreadAbortException)
                {
                    return; 
                }
            }
        }

        public void Update_ListView(string text)
        {
            listview.Items.Add(text);
        }
    }
}
