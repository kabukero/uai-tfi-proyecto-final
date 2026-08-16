using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string language = "en-US";

                if (Session["Language"] != null)
                {
                    language = Session["Language"].ToString();
                }

                ddlLanguage.SelectedValue = language;
            }
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Language"] = ddlLanguage.SelectedValue;
            Response.Redirect(Request.RawUrl);
        }
    }
}
