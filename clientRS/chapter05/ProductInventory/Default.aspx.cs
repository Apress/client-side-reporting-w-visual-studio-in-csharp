using System;
using System.Data;
using System.Configuration;
using System.Collections;
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
        // Add a handler for drill-through reprot
        ReportViewer1.Drillthrough += new DrillthroughEventHandler(ReportViewer1_Drillthrough);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        // connection string
        string cnString = @"Data Source=(local);Initial Catalog=RealWorld;Integrated Security=SSPI;";

        SqlConnection conReport = new SqlConnection(cnString);
        SqlCommand cmdReport = new SqlCommand();
        SqlDataReader drReport;
        DataSet dsReport = new dsProductInventory();

        try
        {
            // open connection
            conReport.Open();

            cmdReport.CommandType = CommandType.Text;
            cmdReport.Connection = conReport;

            // get query string from string builder
            cmdReport.CommandText = "SELECT * FROM ProductDrilldown ORDER BY ProductNumber; SELECT * FROM InventoryStatus ORDER BY ProductNumber";

            // execute query and load result to dataset
            drReport = cmdReport.ExecuteReader();

            dsReport.Load(drReport, LoadOption.OverwriteChanges, dsReport.Tables[1], dsReport.Tables[0]);

            // close connection
            drReport.Close();
            conReport.Close();

            Session["dsProductInventory"] = (DataSet)dsReport;

            // prepare report for view
            ReportViewer1.LocalReport.ReportPath = "rptProductInformation.rdlc";

            //pass user choice of hiding price list column to report using the report parameters
            ReportParameter parHidePriceList = new
            ReportParameter("parHidePriceList", CheckBox1.Checked ? "Y" : "N");
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { parHidePriceList });

            ReportDataSource rds = new ReportDataSource();
            rds.Name = "dsProductInventory_dtProductInformation";

            rds.Value = dsReport.Tables[1];
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

    void ReportViewer1_Drillthrough(object sender, Microsoft.Reporting.WebForms.DrillthroughEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = (DataSet)Session["dsProductInventory"];

        LocalReport InventoryReport = (LocalReport)e.Report;
        InventoryReport.DataSources.Add(new ReportDataSource("dsProductInventory_dtInventoryStatus", ds.Tables[0]));
    }
}
