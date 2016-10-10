using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // connection string
        string cnString = @"Data Source=(local);Initial Catalog=RealWorld;Integrated Security=SSPI;";

        SqlConnection conReport = new SqlConnection(cnString);
        SqlCommand cmdReport = new SqlCommand();
        SqlDataReader drReport;
        DataSet dsReport = new dsChartOfAccounts();

        try
        {
            // open connection
            conReport.Open();

            cmdReport.CommandType = CommandType.Text;
            cmdReport.Connection = conReport;

            // get query string from string builder
            cmdReport.CommandText = "SELECT * FROM ChartOfAccounts ORDER BY AccountCode";

            // execute query and load result to dataset
            drReport = cmdReport.ExecuteReader();

            dsReport.Tables[0].Load(drReport);

            // close connection
            drReport.Close();
            conReport.Close();

            // prepare report for view
            ReportViewer1.LocalReport.ReportPath = "rptChartOfAccountsArb.rdlc";

            ReportDataSource rds = new ReportDataSource();
            rds.Name = "dsChartOfAccounts_dtChartOfAccounts";

            rds.Value = dsReport.Tables[0];
            ReportViewer1.LocalReport.DataSources.Add(rds);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
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
