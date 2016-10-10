namespace ReportWizard
{
    partial class ReportsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.RealWorldDataSet = new ReportWizard.RealWorldDataSet();
            this.tblProductProfitBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblProductProfitTableAdapter = new ReportWizard.RealWorldDataSetTableAdapters.tblProductProfitTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.RealWorldDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblProductProfitBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "RealWorldDataSet_tblProductProfit";
            reportDataSource1.Value = this.tblProductProfitBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "ReportWizard.Sample Reprt by Wizard.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(682, 386);
            this.reportViewer1.TabIndex = 0;
            // 
            // RealWorldDataSet
            // 
            this.RealWorldDataSet.DataSetName = "RealWorldDataSet";
            this.RealWorldDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tblProductProfitBindingSource
            // 
            this.tblProductProfitBindingSource.DataMember = "tblProductProfit";
            this.tblProductProfitBindingSource.DataSource = this.RealWorldDataSet;
            // 
            // tblProductProfitTableAdapter
            // 
            this.tblProductProfitTableAdapter.ClearBeforeFill = true;
            // 
            // ReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 386);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ReportsForm";
            this.Text = "ReportsForm";
            this.Load += new System.EventHandler(this.ReportsForm1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RealWorldDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblProductProfitBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource tblProductProfitBindingSource;
        private RealWorldDataSet RealWorldDataSet;
        private ReportWizard.RealWorldDataSetTableAdapters.tblProductProfitTableAdapter tblProductProfitTableAdapter;
    }
}

