using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using Microsoft.Vbe.Interop;
using Microsoft.Win32;

namespace WindowsApp2
{
    public partial class Form1 : Form
    {

        public bool extensions;
        public bool search;
        public bool signin;
        public bool incognito;
        public bool sync;

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
        } // Constructor



        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex == 0 && checkedListBox1.SelectedIndex > -1)
            {
                extensions = !extensions;
            } 

            /* if (checkedListBox1.SelectedIndex == 1 && checkedListBox1.SelectedIndex > -1)
             {
                 search = !search;
             } */

            if (checkedListBox1.SelectedIndex == 1 && checkedListBox1.SelectedIndex > -1)
            {
                signin = !signin;
            }

            if (checkedListBox1.SelectedIndex == 2 && checkedListBox1.SelectedIndex > -1)
            {
                sync = !sync;
            }

            if (checkedListBox1.SelectedIndex == 3 && checkedListBox1.SelectedIndex > -1)
            {
                incognito = !incognito;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        } // CANCEL BUTTON

        private void button1_Click(object sender, EventArgs e)
        {
            if (extensions == true)
            {
                Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Google\\Chrome\\ExtensionInstallBlacklist", "1", "");               
            }

            /*if(search == true)
            {
                Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Google\\Chrome", "ForceGoogleSafeSearch", "0", RegistryValueKind.DWord);
            }*/

            if (signin == true)
            {
                Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Google\\Chrome", "RestrictSigninToPattern", "");
            }

            if (incognito == true)
            {
                Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Google\\Chrome", "IncognitoModeAvailability", "0", RegistryValueKind.DWord);
            }

            if (sync == true)
            {
                Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Google\\Chrome", "SyncDisabled", "0", RegistryValueKind.DWord);
            }

            if ((extensions == false) && (signin == false) && (incognito == false) && (sync == false))
            {
                MessageBox.Show("No selection has been made!", "Error");
            }

            else
            {
                MessageBox.Show("Changes have been made! Chrome will now RESTART for the changes to take effect!");
                RestartChrome();
                System.Windows.Forms.Application.Exit();
            }
        } // OK BUTTON

        private void button2_Click(object sender, EventArgs e)
        {
                //extensions
                Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Google\\Chrome\\ExtensionInstallBlacklist", "1", "*");

                //search
                //Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Google\\Chrome", "ForceGoogleSafeSearch", "1", RegistryValueKind.DWord);

                //signin
                Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Google\\Chrome", "RestrictSigninToPattern", "*@schools.nyc.gov");

                //Incognito Mode
                Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Google\\Chrome", "IncognitoModeAvailability", "1", RegistryValueKind.DWord);

                //Sync
                Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Google\\Chrome", "SyncDisabled", "1", RegistryValueKind.DWord);

                UpdateGroupPolicy();
                MessageBox.Show("Settings have been restored to default! Chrome will now RESTART for the changes to take effect!");
                RestartChrome();

            System.Windows.Forms.Application.Exit();
            

        } // RESET BUTTON

        private void label1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("This application was created by SD&E Members: Rahul, James, Moaad, Antonio and Keith.", "How did you find this?");
        }

        private void UpdateGroupPolicy()
        {
            FileInfo execfile = new FileInfo("gpupdate.exe");
            Process proc = new Process();
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo.FileName = execfile.Name;
            proc.StartInfo.Arguments = "/force";
            proc.Start();

            MessageBox.Show("This may take a moment.");

            while (!proc.HasExited)
            {
                System.Windows.Forms.Application.DoEvents();
                Thread.Sleep(100);
            }

        }

        private void RestartChrome()
        {
            Process[] AllProcesses = Process.GetProcesses();

            foreach (var process in AllProcesses)
            {
                if (process.MainWindowTitle != "")
                {
                    string Process = process.ProcessName.ToUpper();
                    if (Process == "CHROME")
                    {
                        process.Kill();
                    }
                }
            }

            Process startChrome = new Process();
            startChrome.StartInfo.FileName = @"C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe";
            startChrome.StartInfo.Arguments = "google.com";
            startChrome.Start();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Process process = new Process();

            try
            {
                Process.Start(@"E:\Chrome Policy Editor V2\WindowsApp2\Resources\Chrome Policy Editor User Instruction Manual.docx");
            }
            catch
            {
                MessageBox.Show("Could not find help documentation");
            }
        }
    }
}
