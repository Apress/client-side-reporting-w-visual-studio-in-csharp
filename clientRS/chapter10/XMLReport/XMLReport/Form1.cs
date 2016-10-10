using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace XMLReport
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataSet dsReport = new dsXML();

            // create temp dataset to read xml information
            DataSet dsTempReport = new DataSet();

            try
            {
                // using ReadXml method of DataSet read XML data from books.xml file
                dsTempReport.ReadXml(@"C:\Apress\Chapter10\XMLReport\books.xml");

                // copy XML data from temp dataset to our typed data set
                dsReport.Tables[0].Merge(dsTempReport.Tables[0]);
                
                // prepare report for view
                reportViewer1.LocalReport.ReportEmbeddedResource = "XMLReport.rptXML.rdlc";

                // setting this is important if you are using external image source
                reportViewer1.LocalReport.EnableExternalImages = true;

                ReportDataSource rds = new ReportDataSource();
                rds.Name = "dsXML_dtXML";
                rds.Value = dsReport.Tables[0];
                reportViewer1.LocalReport.DataSources.Add(rds);
                
                // preview the report
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}