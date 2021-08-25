// 
// DotNetNuke?- http://www.dotnetnuke.com 
// Copyright (c) 2002-2018 
// by DotNetNuke Corporation 
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions: 
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software. 
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE. 
// 

using System;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Data;
using DotNetNuke;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Entities.Users;
using DotNetNuke.Common;
using DotNetNuke.Entities.Profile;

namespace CMI_News
{

    /// ----------------------------------------------------------------------------- 
    /// <summary> 
    /// The ViewCMI_News class displays the content 
    /// </summary> 
    /// <remarks> 
    /// </remarks> 
    /// <history> 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    partial class UserProfile : BaseNewsUserControl
    {

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
                CMI_NewsController objNews = new CMI_NewsController();

                hplName.Text=this.UserInfo.DisplayName;
                hplName.NavigateUrl = Globals.UserProfileURL(this.UserId);

                imgPicture.ImageUrl = UserController.Instance.GetUserProfilePictureUrl(this.UserId,135,100);


                lblCompany.Text = GetProfilePropertyValue("CompanyName");

                lblPosition.Text = GetProfilePropertyValue("PositionFunction");

                lblLastViewDate.Text = this.UserInfo.Membership.LastLoginDate.ToLongDateString();

                setCompanyList();
                setMajorIndustryGroupList();

                DataTable dt = objNews.GetFollowedCompanys(UserController.Instance.GetCurrentUserInfo().UserID);
                if (dt.Rows.Count > 0)
                {
                    StringBuilder js2 = new StringBuilder("").AppendLine();
                    js2.Append("$( document ).ready(function() {").AppendLine();

                    foreach (DataRow dr in dt.Rows)
                    {
                        //js2.Append("var obj = ciks.find(o => o.id == '" + dr["CikCode"] + "');").AppendLine();
                        js2.Append("var obj = altFind(ciks, function(e) { return e.id == '" + dr["CikCode"] + "';});").AppendLine();
                        js2.Append("var company = '';").AppendLine();
                        js2.Append("if (obj != null) ").AppendLine();
                        js2.Append("company=obj.name; ").AppendLine();

                        js2.Append("$('.dropdown-mul-2').find('.dropdown-chose-list').prepend('<span class=\"dropdown-selected\">' + company + '<i class=\"del\" data-id=\"" + dr["CikCode"] + "\"></i></span>');").AppendLine();
                    }

                    js2.Append("});").AppendLine();
                    HtmlGenericControl ctrlX = new HtmlGenericControl("script");
                    ctrlX.Attributes.Add("type", "text/javascript");
                    ctrlX.InnerHtml = js2.ToString();
                    this.Page.Header.Controls.Add(ctrlX);
                }

                DataTable dt3 = objNews.GetFollowedMajorIndustryGroups(UserController.Instance.GetCurrentUserInfo().UserID);
                if (dt3.Rows.Count > 0)
                {
                    StringBuilder js2 = new StringBuilder("").AppendLine();
                    js2.Append("$( document ).ready(function() {").AppendLine();

                    foreach (DataRow dr in dt3.Rows)
                    {
                        js2.Append("var obj = altFind(majorIndustryGroupIDs, function(e) { return e.id == '" + dr["MajorIndustryGroupID"] + "';});").AppendLine();
                        js2.Append("var company = '';").AppendLine();
                        js2.Append("if (obj != null) ").AppendLine();
                        js2.Append("company=obj.name; ").AppendLine();

                        js2.Append("$('.dropdown-mul-3').find('.dropdown-chose-list').prepend('<span class=\"dropdown-selected\">' + company + '<i class=\"del\" data-id=\"" + dr["MajorIndustryGroupID"] + "\"></i></span>');").AppendLine();
                    }

                    js2.Append("});").AppendLine();
                    HtmlGenericControl ctrlX = new HtmlGenericControl("script");
                    ctrlX.Attributes.Add("type", "text/javascript");
                    ctrlX.InnerHtml = js2.ToString();
                    this.Page.Header.Controls.Add(ctrlX);
                }
            }

            catch (Exception exc)
            {
                //Module failed to load 
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        protected string GetProfilePropertyValue(string propertyName)
        {
            string value;

            ProfilePropertyDefinition ppd = UserInfo.Profile.GetProperty(propertyName);

            if (ppd == null)
            {
                value = "";
            }
            else
            {
                value = ppd.PropertyValue;
                if (value == null)
                {
                    if (String.IsNullOrEmpty(ppd.DefaultValue))
                        value = String.Empty;
                    else
                        value = ppd.DefaultValue;
                }
            }
            return value;
        }

        private void setCompanyList()
        {
            CMI_NewsController objNews = new CMI_NewsController();

            DataTable dtCom = objNews.GetCMI_CompaniesSortByName();

            StringBuilder js = new StringBuilder("").AppendLine();

            js.Append("var ciks = [").AppendLine();
            foreach (DataRow drCom in dtCom.Rows)
            {
                js.Append("{ id: \"" + drCom["CIKCode"].ToString() + "\", name: \"" + drCom["CompanyName"].ToString().Replace("'", "").Replace("\\", " ") + "\"}").AppendLine();

                if (drCom != dtCom.Rows[Convert.ToInt32(dtCom.Rows.Count.ToString()) - 1])
                    js.Append(", ").AppendLine();

            }
            js.Append(" ];").AppendLine();


            HtmlGenericControl ctrlX = new HtmlGenericControl("script");
            ctrlX.Attributes.Add("type", "text/javascript");
            ctrlX.InnerHtml = js.ToString();
            this.Page.Header.Controls.Add(ctrlX);
        }

        private void setMajorIndustryGroupList()
        {
            CMI_NewsController objNews = new CMI_NewsController();

            DataTable dtCom = objNews.GetCMI_MajorIndustryGroups();

            StringBuilder js = new StringBuilder("").AppendLine();

            js.Append("var majorIndustryGroupIDs = [").AppendLine();
            foreach (DataRow drCom in dtCom.Rows)
            {
                js.Append("{ id: \"" + drCom["MajorIndustryGroupID"].ToString() + "\", name: \"" + drCom["MajorIndustryGroup"].ToString().Replace("'", "").Replace("\\", " ") + "\"}").AppendLine();

                if (drCom != dtCom.Rows[Convert.ToInt32(dtCom.Rows.Count.ToString()) - 1])
                    js.Append(", ").AppendLine();

            }
            js.Append(" ];").AppendLine();


            HtmlGenericControl ctrlX = new HtmlGenericControl("script");
            ctrlX.Attributes.Add("type", "text/javascript");
            ctrlX.InnerHtml = js.ToString();
            this.Page.Header.Controls.Add(ctrlX);
        }
        #endregion

        #region "Optional Interfaces"

        #endregion

    }

}