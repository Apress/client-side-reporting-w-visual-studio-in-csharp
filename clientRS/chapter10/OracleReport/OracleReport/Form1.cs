using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Oracle.DataAccess.Client;

namespace OracleReport
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // connection string
            string cnString = "User Id=hr;Password=hr;Data Source=XE";

            OracleConnection conReport = new OracleConnection(cnString);
            OracleCommand cmdReport = new OracleCommand();
            OracleDataReader drReport;

            DataSet dsReport = new dsOracle();

            try
            {
                // open connection
                conReport.Open();

                cmdReport.CommandType = CommandType.Text;
                cmdReport.Connection = conReport;
                cmdReport.CommandText = "SELECT first_name, last_name, salary FROM EMP_DETAILS_VIEW";

                // execute query and load result to dataset
                drReport = cmdReport.ExecuteReader();
                dsReport.Tables[0].Load(drReport);

                // close connection
                drReport.Close();
                conReport.Close();

                // prepare report for view
                reportViewer1.LocalReport.ReportEmbeddedResource = "OracleReport.rptOracle.rdlc";
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "dsOracle_dtOracle";
                rds.Value = dsReport.Tables[0];
                reportViewer1.LocalReport.DataSources.Add(rds);

                // preivew the report
                reportViewer1.RefreshReport();
            }
            catch (OracleException ex)
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