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
    public partial class NewsAdmin : PortalModuleBase, IActionable
    {

        CMI_NewsController objNews = new CMI_NewsController();
        List<CMI_NewsInfo> colNews;
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
                colNews = objNews.GetCMI_Newss();
            }

            catch (Exception exc)
            {
                //Module failed to load 
                Exceptions.ProcessModuleLoadException(this, exc);
            }

        }
        protected void rgrList_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "InitInsert")
            {
                Response.Redirect(EditUrl());
            }
        }
        protected void rgrList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgrList.DataSource = colNews;
        }

        protected void rgrList_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

            if (e.CommandName.Equals("Delete"))
            {

                GridEditableItem dataItem = (GridEditableItem)e.Item;

                int NewsID = Convert.ToInt32(dataItem.GetDataKeyValue("NewsID").ToString());

                objNews.DeleteCMI_News(NewsID);

                reBindGrid();

            }
        }

        private void reBindGrid()
        {
            colNews = objNews.GetCMI_Newss();
            rgrList.DataSource = colNews;
            rgrList.DataBind();
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

    }
}