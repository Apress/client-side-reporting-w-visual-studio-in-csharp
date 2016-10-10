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
        //Declare connection string
        string cnString = "Data Source=(local);Initial Catalog=RealWorld;Integrated Security=SSPI;";

        //Declare Connection, command and other related objects
        SqlConnection conReport = new SqlConnection(cnString);
        SqlCommand cmdReport = new SqlCommand();
        SqlDataReader drReport;
        DataSet dsReport = new dsProductDrilldown();

        try
        {
            conReport.Open();

            cmdReport.CommandType = CommandType.Text;
            cmdReport.Connection = conReport;
            cmdReport.CommandText = "Select * FROM dbo.ProductDrilldown order by CategoryName,SubCategoryName,ProductNumber";

            drReport = cmdReport.ExecuteReader();
            dsReport.Tables[0].Load(drReport);

            drReport.Close();
            conReport.Close();

            //provide local report information to viewer
            ReportViewer1.LocalReport.ReportPath = "rptProductDrilldown.rdlc";

            //prepare report data source
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "dsProductDrilldown_dtProductDrilldown";
            rds.Value = dsReport.Tables[0];
            ReportViewer1.LocalReport.DataSources.Add(rds);
        }
        catch (Exception ex)
        {
            //display generic error message back to user
            Response.Write(ex.Message);
        }
        finally
        {
            //check if connection is still open then attempt to close it
            if (conReport.State == ConnectionState.Open)
            {
                conReport.Close();
            }
        }
    }
}
