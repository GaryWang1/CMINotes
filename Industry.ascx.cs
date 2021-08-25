using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Data;

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

    /// ----------------------------------------------------------------------------- 
    /// <summary> 
    /// The ViewEA class displays the content 
    /// </summary> 
    /// <remarks> 
    /// </remarks> 
    /// <history> 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public partial class Industry : PortalModuleBase, IActionable
    {
        DataTable colIndustry;
        int majorIndustryGroupId;
        #region "Event Handlers"


        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Page_Load runs when the control is loaded 
        /// </summary> 
        /// ----------------------------------------------------------------------------- 
        protected void Page_Load(object sender, System.EventArgs e)
        {
            majorIndustryGroupId = Convert.ToInt32(Request.QueryString["MajorIndustryGroupID"].ToString());
            CMI_NewsController objController = new CMI_NewsController();
            ltlIndustry.Text = objController.GetCMI_MajorIndustryGroup(majorIndustryGroupId).Rows[0]["MajorIndustryGroup"].ToString();
            
            colIndustry = objController.GetCMI_IndustriesByGroup(majorIndustryGroupId);

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// rgrList_NeedDataSource runs when the Rad Grid is rendered 
        /// </summary> 
        /// ----------------------------------------------------------------------------- 

        protected void rgrList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            rgrList.DataSource = colIndustry;
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// rgrList_ItemDataBound runs when the rad grid item data bound
        /// </summary> 
        /// ----------------------------------------------------------------------------- 
        protected void rgrList_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// OnUpdateCommand 
        /// </summary> 
        /// ----------------------------------------------------------------------------- 
        protected void rgrList_OnUpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {

                if (e.CommandName.Equals("Update"))
                {
                    CMI_News.CMI_NewsController objController = new CMI_News.CMI_NewsController();

                    GridEditableItem dataItem = (GridEditableItem)e.Item;
                    int SiCCode = Convert.ToInt32(dataItem.GetDataKeyValue("SICCode").ToString());
                    string Industry = ((TextBox)dataItem["Industry"].Controls[0]).Text;

                    objController.UpdateCMI_Industry(SiCCode, majorIndustryGroupId, Industry);

                    Response.Redirect(Request.RawUrl);

                }
            }
            catch (Exception exc)
            {
                lblErr.Text = exc.Message;
                lblErr.Visible = true;
            }
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// OnInsertCommand
        /// </summary> 
        /// ----------------------------------------------------------------------------- 
        protected void rgrList_OnInsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("PerformInsert"))
                {
                    CMI_News.CMI_NewsController objController = new CMI_News.CMI_NewsController();

                    GridEditableItem dataItem = (GridEditableItem)e.Item;
                    int SiCCode = Convert.ToInt32(((TextBox)dataItem["SiCCode"].Controls[0]).Text);
                    string Industry = ((TextBox)dataItem["Industry"].Controls[0]).Text;

                    objController.AddCMI_Industry(SiCCode, majorIndustryGroupId, Industry);

                    Response.Redirect(Request.RawUrl);
                }
            }
            catch (Exception exc)
            {
                lblErr.Text = exc.Message;
            }
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// OnDeleteCommand
        /// </summary> 
        /// ----------------------------------------------------------------------------- 
        protected void rgrList_OnDeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName.Equals("Delete"))
            {
                CMI_News.CMI_NewsController objController = new CMI_News.CMI_NewsController();

                GridEditableItem dataItem = (GridEditableItem)e.Item;
                int SiCCode = Convert.ToInt32(dataItem.GetDataKeyValue("SICCode").ToString());

                objController.DeleteCMIIndustry(SiCCode);

                Response.Redirect(Request.RawUrl);

            }
        }
		
		protected void rgrList_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e) 
		{ 
			if (e.CommandName == "IndustryList") 
			{ 
				GridDataItem dataItem = (GridDataItem)e.Item; 
				
				string MajorIndustryGroupID = dataItem.GetDataKeyValue("MajorIndustryGroupID").ToString();
                Response.Redirect(EditUrl("MajorIndustryGroupID", MajorIndustryGroupID, "Industry"));
	 
			} 
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
            {ModuleActionCollection Actions = new ModuleActionCollection();

            ////Edit Service
            //Actions.Add(GetNextActionID(), "Edit Service",
            //   ModuleActionType.AddContent, "", "edit.gif", EditUrl("Service"), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
            //    true, false);

                return Actions;
            }
        }

        #endregion

    }
}