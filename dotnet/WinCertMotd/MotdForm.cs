using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Json;


namespace WinCertMotd
{
    public partial class MotdForm : Form
    {
        public MotdForm()
        {
            InitializeComponent();
        }

        private void DisplayMotd(string error)
        {
            linkLabelRule.Text = String.Empty;
            labelTitle.Text = error;
        }

        private void DisplayMotd(string title, string message, string url)
        {
            labelTitle.Text = title;           
            linkLabelRule.Text = message;

            linkLabelRule.Links.Add(0, url.Length, url); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();

            // By default use the database in the same directory as the executable
            string path = "db.json";
            if (args.Length == 2)
            {
                path = args[1]; // override it if supplied though
            }
            
            try
            { 
                using (StreamReader sr = new StreamReader(path))
                {
                    DataContractJsonSerializer jsonSer = new DataContractJsonSerializer(typeof(RuleSet));

                    object objdb = jsonSer.ReadObject(sr.BaseStream);
                    RuleSet db = objdb as RuleSet;

                    if (db.rules != null)
                    {
                        Random r = new Random();
                        Rule show = db.rules[r.Next(db.rules.Length)];

                        DisplayMotd(String.Format("CERT {0} {1} {2}", show.Type, show.Section, show.Number), show.Name, show.Url);
                    }
                    else
                    {
                        DisplayMotd("Did not decode any rules");
                    }
                }
            }
            catch(FileNotFoundException err)
            {
                DisplayMotd("Could not find database file '" + path + "'");
            }
            catch (Exception err)
            {
                DisplayMotd("Exception thrown: " + err.ToString());
            }
        }

        private void linkLabelRule_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            System.Diagnostics.Process.Start(e.Link.LinkData as string);
        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
