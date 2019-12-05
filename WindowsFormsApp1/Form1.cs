using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private MonitorThraed monitor;
        private bool RealDetection;
        private string DeviceID;
        private string DevicePDO;
        public delegate void Delegate_ListViewUpdate(string text);

        public Form1()
        {
            InitializeComponent();
            monitor = new MonitorThraed(listView1);
            RealDetection = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Initialize();
        }

        private void Initialize()
        {
            // 1. Get PDO
            string querystring = "SELECT * FROM " + DEVICE_CLASS.Win32_PnPSignedDriver;

            FindDevice findDevice = new FindDevice(querystring);

            DevicePDO = findDevice.GetResults(
                SEARCH_SELECTOR_TYPE.DeviceName,
                DB.WEBCAM_DEVICE_NAME,
                SEARCH_SELECTOR_TYPE.PDO
                )[0];

            // 2. Get DeviceID
            List<string> hardwareid = findDevice.GetResults(
                SEARCH_SELECTOR_TYPE.DeviceName,
                DB.WEBCAM_DEVICE_NAME,
                SEARCH_SELECTOR_TYPE.HardwareID
                );

            string[] s_hardwareid = hardwareid[0].Split(new char[] { '&' });
            string f_hardwareid = "";

            for(int i=0;i<s_hardwareid.Length-1;i++)
            {
                f_hardwareid += s_hardwareid[i];
                if (i < s_hardwareid.Length - 2)
                    f_hardwareid += '&';
            }

            hardwareid[0] = f_hardwareid;

            querystring = "SELECT * FROM " + DEVICE_CLASS.Win32_PnPEntity;
            findDevice = new FindDevice(querystring);

            DeviceID = findDevice.GetResults(
                SEARCH_SELECTOR_TYPE.HardwareID,
                hardwareid,
                SEARCH_SELECTOR_TYPE.DeviceID
                )[0];

            monitor.SetPDO(DevicePDO);
        }

        // on
        private void Button1_Click(object sender, EventArgs e)
        {
            // 장치의 하드웨어 ID를 얻는다.
            // USB Composite Device에서 해당 값을 갖고 있는 장치의 DeviceID를 얻는다. 그걸 끄면 됨
            string querystring = DEVICE_CLASS.Win32_PnPEntity + "." + SEARCH_SELECTOR_TYPE.DeviceID + "='" + DeviceID+"'";

            Device device = new Device(
                querystring);

            device.On();
        }

        // off
        private void Button2_Click(object sender, EventArgs e)
        {
            string querystring = DEVICE_CLASS.Win32_PnPEntity + "." + SEARCH_SELECTOR_TYPE.DeviceID + "='" + DeviceID + "'";

            Device device = new Device(
                querystring);

            device.Off();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            ;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                monitor.Abort();
            }catch(Exception)
            {
                ;
            }

        }

        private void Button3_Click_1(object sender, EventArgs e)
        {
            if(RealDetection)
            {
                try
                {
                    RealDetection = false;
                    monitor.Abort();
                    button3.Text = PROGRAM_STRING.RD_BUTTON_OFF;
                    listView1.Items.Add(PROGRAM_STRING.RDOFF);
                }catch(Exception)
                {
                    ;
                }
            }
            else
            {
                try
                {
                    RealDetection = true;
                    monitor.Run();
                    button3.Text = PROGRAM_STRING.RD_BUTTON_ON;
                }
                catch (Exception)
                {
                    ;
                }
            }
        }
    }
}
