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
    public partial class IndustryGroup : PortalModuleBase, IActionable
    {
        DataTable colMajorIndustryGroup;
        #region "Event Handlers"


        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Page_Load runs when the control is loaded 
        /// </summary> 
        /// ----------------------------------------------------------------------------- 
        protected void Page_Load(object sender, System.EventArgs e)
        {

            CMI_NewsController objController = new CMI_NewsController();

            colMajorIndustryGroup = objController.GetCMI_MajorIndustryGroups();

            /*string path = Server.MapPath(CMI_NewsController.MAJORINDUSTRYGROUPBANNERPATH);

            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }*/
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// rgrList_NeedDataSource runs when the Rad Grid is rendered 
        /// </summary> 
        /// ----------------------------------------------------------------------------- 

        protected void rgrList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            rgrList.DataSource = colMajorIndustryGroup;
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// rgrList_ItemDataBound runs when the rad grid item data bound
        /// </summary> 
        /// ----------------------------------------------------------------------------- 
        protected void rgrList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //try
            //{
            //    if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            //    {
            //        GridEditFormItem editItem = e.Item as GridEditFormItem;
            //        UserControl yourUC = ((UserControl)((GridEditFormItem)e.Item).FindControl(GridEditFormItem.EditFormUserControlID));
            //        if (yourUC != null)
            //        {
            //            CMI_NewsController objController = new CMI_NewsController();

            //            if (editItem.RowIndex != -1)
            //            {
            //                DataRowView m = (DataRowView)editItem.DataItem;

            //                Image imgBanner = yourUC.FindControl("imgBanner") as Image;
            //                imgBanner.ImageUrl = m["BannerImage"].ToString();

            //            }
            //        }
            //    }
            //}
            //catch (Exception exc)
            //{
            //    lblErr.Text = exc.Message;
            //}
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
                //GridEditableItem editedItem = e.Item as GridEditableItem;
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);

                if (e.CommandName.Equals("Update"))
                {
                    CMI_News.CMI_NewsController objController = new CMI_News.CMI_NewsController();

                    GridEditableItem dataItem = (GridEditableItem)e.Item;
                    int MajorIndustryGroupId = Convert.ToInt32(dataItem.GetDataKeyValue("MajorIndustryGroupID").ToString());
                    string MajorIndustryGroup = (userControl.FindControl("txtMajorIndustryGroup") as TextBox).Text;

                    //string BannerImage = (userControl.FindControl("imgBanner") as Image).ImageUrl;

                    objController.UpdateCMI_MajorIndustryGroup(MajorIndustryGroupId, MajorIndustryGroup, "");

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
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                if (e.CommandName.Equals("PerformInsert"))
                {
                    CMI_News.CMI_NewsController objController = new CMI_News.CMI_NewsController();

                    GridEditableItem dataItem = (GridEditableItem)e.Item;
                    int MajorIndustryGroupId = Convert.ToInt32(dataItem.GetDataKeyValue("MajorIndustryGroupID").ToString());
                    string MajorIndustryGroup = (userControl.FindControl("txtMajorIndustryGroup") as TextBox).Text;

                    //string BannerImage = (userControl.FindControl("imgBanner") as Image).ImageUrl;

                    objController.AddCMI_MajorIndustryGroup(MajorIndustryGroup, "");

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

                CMI_NewsController objController = new CMI_NewsController();

                GridEditableItem dataItem = (GridEditableItem)e.Item;

                int MajorIndustryGroupID = Convert.ToInt32(dataItem.GetDataKeyValue("MajorIndustryGroupID").ToString());

                objController.DeleteCMI_News(MajorIndustryGroupID);

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
            else if (e.CommandName == "D2IndustryList")
            {
                GridDataItem dataItem = (GridDataItem)e.Item;

                string MajorIndustryGroupID = dataItem.GetDataKeyValue("MajorIndustryGroupID").ToString();
                Response.Redirect(EditUrl("MajorIndustryGroupID", MajorIndustryGroupID, "D2Industry"));

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