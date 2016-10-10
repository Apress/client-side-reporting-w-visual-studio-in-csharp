using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ReportWizard
{
    public partial class ReportsForm : Form
    {
        public ReportsForm()
        {
            InitializeComponent();
        }

        private void ReportsForm1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'RealWorldDataSet.tblProductProfit' table. You can move, or remove it, as needed.
            this.tblProductProfitTableAdapter.Fill(this.RealWorldDataSet.tblProductProfit);

            this.reportViewer1.RefreshReport();
        }
    }
}