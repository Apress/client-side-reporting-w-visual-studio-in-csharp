<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:CheckBox ID="CheckBox1" Text="Hide List Price?" runat="server" />
        <asp:Button ID="Button1" runat="server" Text="Generate Report" OnClick="Button1_Click" />
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="800px">
        </rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
