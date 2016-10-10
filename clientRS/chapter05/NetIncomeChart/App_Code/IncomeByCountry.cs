using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

/// <summary>
/// Summary description for IncomeByCountry
/// </summary>
public class IncomeByCountry
{
    private List<NetIncome> _netIncome;

    public IncomeByCountry()
    {
        _netIncome = new List<NetIncome>();
        _netIncome.Add(new NetIncome("Canada", 305001, 230050));
        _netIncome.Add(new NetIncome("America", 118900, 98002));
        _netIncome.Add(new NetIncome("France", 54000, 67900));
        _netIncome.Add(new NetIncome("Germany", 20013, 7803));
    }

    public List<NetIncome> GetNetIncomeData()
    {
        return _netIncome;
    }
}
