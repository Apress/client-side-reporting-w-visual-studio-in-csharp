using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;

namespace Barcode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //making Neodynamic assembly trusted
            this.reportViewer1.LocalReport.AddTrustedCodeModuleInCurrentAppDomain("Neodynamic.ReportingServices.Barcode, Version=4.0.2000.0, Culture=neutral, PublicKeyToken=c6b33c3093a0d4cd");

            // connection string
            string cnString = @"Data Source=(local);Initial Catalog=RealWorld;Integrated Security=SSPI;";

            SqlConnection  conReport = new SqlConnection(cnString);
            SqlCommand cmdReport = new SqlCommand();
            SqlDataReader drReport;

            DataSet dsReport = new dsBarcode();

            try
            {
                // open connection
                conReport.Open();

                cmdReport.CommandType = CommandType.Text;
                cmdReport.Connection = conReport;
                
                // get query string from string builder
                cmdReport.CommandText = "SELECT * FROM tblBarcode";

                // execute query and load result to dataset
                drReport = cmdReport.ExecuteReader();
                dsReport.Tables[0].Load(drReport);

                // close connection
                drReport.Close();
                conReport.Close();
                
                // prepare report for view
                reportViewer1.LocalReport.ReportEmbeddedResource = "Barcode.rptBarcode.rdlc";
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "dsBarcode_dtBarcode";
                rds.Value = dsReport.Tables[0];
                reportViewer1.LocalReport.DataSources.Add(rds);

                // preivew the report
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (conReport.State == ConnectionState.Open)
                {
                    conReport.Close();
                }
            }
        }
    }
}