using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for NetIncome
/// </summary>
public class NetIncome
{
    private string _country;
    private double _currentYear;
    private double _lastYear;

    public string Country
    {
        get
        {
            return _country;
        }
        set
        {
            _country = value;
        }
    }

    public double CurrentYear
    {
        get
        {
            return _currentYear;
        }
        set
        {
            _currentYear = value;
        }
    }

    public double LastYear
    {
        get
        {
            return _lastYear;
        }
        set
        {
            _lastYear = value;
        }
    }

    public NetIncome(string CountryName, double CurrentYear, double LastYear)
    {
        this._country = CountryName;
        this._currentYear = CurrentYear;
        this._lastYear = LastYear;
    }
}
