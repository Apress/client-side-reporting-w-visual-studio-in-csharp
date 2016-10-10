using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Reporting.WinForms;

namespace MyDashboard
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            System.Uri uri = new Uri(@"http://rss.news.yahoo.com/rss/topstories");

            XmlDocument doc = new XmlDocument();
            using (XmlReader reader = XmlReader.Create(uri.ToString()))
            {
                doc.Load(reader);
                reader.Close();
            }

            XmlNodeList title = doc.GetElementsByTagName("title");
            XmlNodeList link = doc.GetElementsByTagName("link");

            // get reference of typed DataSet
            dsDashboard dsRSS = new dsDashboard();

            // get row information from typed DataSet
            dsDashboard.dtDashboardRow dtRSS;

            // add four rows to DataSet
            for (int i = 1; i < 6; ++i)
            {
                dtRSS = dsRSS.dtDashboard.NewdtDashboardRow();
                dtRSS.NewsTitle = title[i].InnerText;
                dtRSS.NewsLink = link[i].InnerText;
                dsRSS.dtDashboard.AdddtDashboardRow(dtRSS);
            }

            // Specify report path
            reportViewer1.LocalReport.ReportEmbeddedResource = "MyDashboard.rptDashboard.rdlc";
            ReportDataSource rds = new ReportDataSource();

            reportViewer1.LocalReport.EnableExternalImages = true;
            reportViewer1.LocalReport.EnableHyperlinks = true;

            rds.Name = "dsDashboard_dtDashboard";
            rds.Value = dsRSS.Tables[0];
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
        }
    }
}