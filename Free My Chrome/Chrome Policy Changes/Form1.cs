using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Chrome_Policy_Changes
{
    public partial class Form1 : Form
    {
        public bool extensions;
        public bool signin;
        public bool incognito;
        public bool sync;

        public Form1()
        {
            this.KeyPreview = true;
            InitializeComponent();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex == 0 && checkedListBox1.SelectedIndex > -1)
            {
                extensions = !extensions;
              /*  if (extensions == true)
                {
                    MessageBox.Show("Checked");
                }

                if (extensions == false)
                {
                    MessageBox.Show("Unchecked");
                }*/

            }

           /* if (checkedListBox1.SelectedIndex == 1 && checkedListBox1.SelectedIndex > -1)
            {
                search = !search;
              /*  if (search == true)
                {
                    MessageBox.Show("Search");
                }

                if (search == false)
                {
                    MessageBox.Show("Unchecked");
                }

            }*/

            if (checkedListBox1.SelectedIndex == 1 && checkedListBox1.SelectedIndex > -1)
            {
                signin = !signin;
              /*  if (signin == true)
                {
                    MessageBox.Show("Sign In");
                }

                if (signin == false)
                {
                    MessageBox.Show("Unchecked");
                }*/

            }

            if (checkedListBox1.SelectedIndex == 2 && checkedListBox1.SelectedIndex > -1)
            {
                sync = !sync;
              /* if (bookmarks == true)
                {
                    MessageBox.Show("Bookmarks");
                }

                if (bookmarks == false)
                {
                    MessageBox.Show("Unchecked");
                }*/

            }

            if (checkedListBox1.SelectedIndex == 3 && checkedListBox1.SelectedIndex > -1)
            {
                incognito = !incognito;

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(extensions == true)
            {
                Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Google\\Chrome\\ExtensionInstallBlacklist", "1", "");
                /*dialog = MessageBox.Show("Do you want to make any other changes?", "Changes have been made!", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.No)
                {
                    Application.Exit();
                }*/
            }

            /*if(search == true)
            {
                Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Google\\Chrome", "ForceGoogleSafeSearch", "0", RegistryValueKind.DWord);
            }*/

            if(signin == true)
            {
                Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Google\\Chrome", "RestrictSigninToPattern", "");
            }

           if(incognito == true)
            {
                Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Google\\Chrome", "IncognitoModeAvailability", "0", RegistryValueKind.DWord);
            }

           if(sync == true)
            {
                Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Google\\Chrome", "SyncDisabled", "0", RegistryValueKind.DWord);
            }

            if (extensions == false && signin == false && incognito == false && sync == false){
                MessageBox.Show("No selection has been made!", "Error!");
            }

            else
            {
                MessageBox.Show("Changes have been made! You will need to restart Google Chrome for changes to take effect!");
                Application.Exit();
            }
           
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
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

            MessageBox.Show("Settings have been restored to default!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutBox1 = new AboutBox1();
            aboutBox1.ShowDialog();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("By Rahul & James");
        }

        private void UpdateGroupPolicy()
        {
            FileInfo execfile = new FileInfo("gpupdate.exe");
            Process proc = new Process();
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo.FileName = execfile.Name;
            proc.StartInfo.Arguments = "/force";
            proc.Start();

            while (!proc.HasExited)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }
        }

    }
}
