using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Data;
using System.Text;
using DotNetNuke;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.UI.UserControls;

using Telerik.Web.UI;

namespace CMI_News
{
    public partial class Company : PortalModuleBase, IActionable
    {
        #region "Private Members"

        #endregion
        
        #region "Event Handlers"

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Page_Load runs when the control is loaded 
        /// </summary> 
        /// ----------------------------------------------------------------------------- 
        protected void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                //if (!Page.IsPostBack)
                //{            
                    setCompanyList();
                //}
            }

            catch (Exception exc)
            {
                //Module failed to load 
                Exceptions.ProcessModuleLoadException(this, exc);
            }

        }
        private void setCompanyList()
        {
            CMI_NewsController objNews = new CMI_NewsController();

            DataTable dtCom = objNews.GetCMI_CompaniesWitNum();

            txtCIKCode.Attributes.Add("onkeyup", "CIKChanged(this.value)");
            StringBuilder js = new StringBuilder("").AppendLine();

            js.Append("var ciks = [").AppendLine();
            foreach (DataRow drCom in dtCom.Rows)
            {
                js.Append("{ value: \"" + drCom["CIKCode"].ToString() + "\", company: \"" + drCom["CompanyName"].ToString().Replace("'", "").Replace("\\", " ") + "\", num: \"" + drCom["num"].ToString() + "\"}").AppendLine();

                if (drCom != dtCom.Rows[Convert.ToInt32(dtCom.Rows.Count.ToString()) - 1])
                    js.Append(", ").AppendLine();

            }
            js.Append(" ];").AppendLine();


            js.Append("$(document).ready(function () {").AppendLine();

            js.Append("      $( \"#txtCIKCode\" ).autocomplete({").AppendLine();
            js.Append("      minLength: 0,").AppendLine();
            js.Append("      source: ciks,").AppendLine();
            js.Append("      focus: function( event, ui ) {").AppendLine();
            js.Append("        $( \"#txtCIKCode\" ).val( ui.item.value );").AppendLine();
            js.Append("        return false;").AppendLine();
            js.Append("      },").AppendLine();

            js.Append("      select: function( event, ui ) {").AppendLine();
            js.Append("        $( \"#txtCompanyName\" ).val(ui.item.company );").AppendLine();
            js.Append("        $( \"#txtCIKCode\" ).val( ui.item.value );").AppendLine();

            js.Append("        if(ui.item.num=='0')").AppendLine();
            js.Append("        $( \"#cmdDelete\" ).show();").AppendLine();
            js.Append("        else").AppendLine();
            js.Append("        $( \"#cmdDelete\" ).hide();").AppendLine();

            js.Append("        return false;").AppendLine();
            js.Append("      }").AppendLine();

            js.Append("    })").AppendLine();
            js.Append(" } );").AppendLine();


            HtmlGenericControl ctrlX = new HtmlGenericControl("script");
            ctrlX.Attributes.Add("type", "text/javascript");
            ctrlX.InnerHtml = js.ToString();
            this.Page.Header.Controls.Add(ctrlX);
        }

        #endregion

        #region "Optional Interfaces"

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Registers the module actions required for interfacing with the portal framework 
        /// </summary> 
        /// <value></value> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public ModuleActionCollection ModuleActions
        {
            get
            {
                ModuleActionCollection Actions = new ModuleActionCollection();

                Actions.Add(GetNextActionID(), "View",
                   ModuleActionType.AddContent, "", "view.gif", DotNetNuke.Common.Globals.NavigateURL(), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
                    true, false);

                return Actions;
            }
        }

        #endregion

        #region "Event Handlers"

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// cmdCancel_Click runs when the cancel button is clicked 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Globals.NavigateURL(), true);
            }
            catch (Exception exc)
            {
                //Module failed to load 
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// cmdUpdate_Click runs when the update button is clicked 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                CMI_NewsController objCMI_Newss = new CMI_NewsController();

                objCMI_Newss.UpdateCMI_Company(txtCIKCode.Text, txtCompanyName.Text);

                // Redirect back to the portal home page 
                //Response.Redirect(Globals.NavigateURL(), true);
            }
            catch (Exception exc)
            {
                //Module failed to load 
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// cmdDelete_Click runs when the delete button is clicked 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Only attempt to delete the item if it exists already 
                if (txtCIKCode.Text!="")
                {

                    CMI_NewsController objCMI_Newss = new CMI_NewsController();
                    objCMI_Newss.DeleteCMICompany(txtCIKCode.Text);
                    txtCIKCode.Text="";
                    txtCompanyName.Text = "";


                }

                // Redirect back to the portal home page 
                //Response.Redirect(Globals.NavigateURL(), true);
            }
            catch (Exception exc)
            {
                //Module failed to load 
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }



        #endregion

    }
}