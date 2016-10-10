using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace WinServerSide
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // set processing mode to remote
            reportViewer1.ProcessingMode = ProcessingMode.Remote;

            // specify report URL
            // please make sure to replace localhost with server name if needed
            reportViewer1.ServerReport.ReportServerUrl = new Uri(@"http://localhost/reportserver");

            // specify report path, Folder = Accounting Report Pack, Report = Trial Balance
            reportViewer1.ServerReport.ReportPath = @"/Accounting Report Pack/Trial Balance";

            // show the report
            this.reportViewer1.RefreshReport();
        }
    }
}