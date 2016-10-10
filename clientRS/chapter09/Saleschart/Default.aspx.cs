using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // get reference of typed DataSet
            dsSalesChart dsChart = new dsSalesChart();

            // get row information from typed DataSet
            dsSalesChart.dtSalesChartRow dtChart;

            // add four rows to DataSet
            dtChart = dsChart.dtSalesChart.NewdtSalesChartRow();
            dtChart.Branch = "Toronto";
            dtChart.YearEndSalesTotal = 239.44;

            dsChart.dtSalesChart.AdddtSalesChartRow(dtChart);

            dtChart = dsChart.dtSalesChart.NewdtSalesChartRow();
            dtChart.Branch = "New York";
            dtChart.YearEndSalesTotal = 100.54;

            dsChart.dtSalesChart.AdddtSalesChartRow(dtChart);

            dtChart = dsChart.dtSalesChart.NewdtSalesChartRow();
            dtChart.Branch = "Victoria";
            dtChart.YearEndSalesTotal = 230.54;

            dsChart.dtSalesChart.AdddtSalesChartRow(dtChart);

            dtChart = dsChart.dtSalesChart.NewdtSalesChartRow();
            dtChart.Branch = "Hamilton";
            dtChart.YearEndSalesTotal = 41.11;

            dsChart.dtSalesChart.AdddtSalesChartRow(dtChart);

            // Specify report path
            ReportViewer1.LocalReport.ReportPath = "rpvSaleschart.rdlc";
            ReportDataSource rds = new ReportDataSource();

            rds.Name = "dsSalesChart_dtSalesChart";
            rds.Value = dsChart.Tables[0];
            ReportViewer1.LocalReport.DataSources.Add(rds);
        }
        catch (Exception ex)
        {
            // write error output on page
            Response.Write(ex.Message);
        }
    }
}
