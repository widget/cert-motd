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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void DisplayHtml(string html)
        {
            certText.Navigate("about:blank");
            if (certText.Document != null)
            {
                certText.Document.Write(string.Empty);
            }
            certText.DocumentText = html;
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

                        DisplayHtml(String.Format("<html><h3>CERT {0} <abbr title={1}>{2}</abbr></h3><p><a target=_blank href={3}>{4}</a></p></html>",
                                        show.Type, show.Section, show.Number, show.Url, show.Name));

                    }
                    else
                    {
                        DisplayHtml("Did not decode any rules");
                    }
                }
            }
            catch(FileNotFoundException err)
            {
                DisplayHtml("Could not find database file <tt>" + path + "</tt>");
            }
            catch (Exception err)
            {
                DisplayHtml("Exception thrown: " + err.ToString());
            }


        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
