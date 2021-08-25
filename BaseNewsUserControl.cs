using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Tabs;

namespace CMI_News
{
    /// <summary>
    /// base class for all News view user controls
    /// </summary>
    public class BaseNewsUserControl : PortalModuleBase
    {

        /// <summary>
        /// factory method returns the user control
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="isSchoolPortal">true if this is a school portal</param>
        ///  <param name="isFirst">true if this is the first module in the tab</param>
        /// <returns></returns>
        public static BaseNewsUserControl GetInstance(DotNetNuke.Entities.Modules.PortalModuleBase module)
        {
            BaseNewsUserControl control = null;

            // check for Tab Module setting
            ModuleController objModules = new ModuleController();
            Hashtable htSettings = objModules.GetTabModuleSettings(module.TabModuleId);

            if (htSettings.ContainsKey(NewsCommon.SETTING_DATATYPE))
            {
                htSettings = objModules.GetTabModuleSettings(module.TabModuleId);

                // use the setting to get the control
                string dataType = Convert.ToString(htSettings[NewsCommon.SETTING_DATATYPE]);
                control = getControl(module, dataType);
            }
            return control;
        }


        /// <summary>
        /// choose the control based on the data type setting
        /// </summary>
        /// <param name="module"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        private static BaseNewsUserControl getControl(DotNetNuke.Entities.Modules.PortalModuleBase module, string dataType)
        {
            BaseNewsUserControl control = null;
            if (dataType == NewsCommon.DATATYPE_Home_Top3)
            {
                control = (BaseNewsUserControl)module.Page.LoadControl(module.TemplateSourceDirectory + "/HomeTop3.ascx");
            }
            else if (dataType == NewsCommon.DATATYPE_Home_Table)
            {
                control = (BaseNewsUserControl)module.Page.LoadControl(module.TemplateSourceDirectory + "/TableView.ascx");
            }
            else if (dataType == NewsCommon.DATATYPE_User_Profile)
            {
                control = (BaseNewsUserControl)module.Page.LoadControl(module.TemplateSourceDirectory + "/UserProfile.ascx");
            }
            else if (dataType == NewsCommon.DATATYPE_User_Followed)
            {
                control = (BaseNewsUserControl)module.Page.LoadControl(module.TemplateSourceDirectory + "/UserFollowed.ascx");
            }

            return control;
        }

    }
}
