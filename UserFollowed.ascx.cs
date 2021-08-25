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
    partial class UserFollowed : BaseNewsUserControl
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

                DataTable company = objNews.GetFollowedCompanys(this.UserId);
                rptCompany.DataSource = company;
                rptCompany.DataBind();

                DataTable majorIndustryGroup = objNews.GetFollowedMajorIndustryGroups(this.UserId);
                rptMajorIndustryGroup.DataSource =majorIndustryGroup;
                rptMajorIndustryGroup.DataBind();

            }

            catch (Exception exc)
            {
                //Module failed to load 
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }
        #endregion

        #region "Optional Interfaces"

        #endregion

        protected void rptCompany_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CMI_NewsController objNews = new CMI_NewsController();
                var row = e.Item.DataItem as System.Data.DataRowView;

                Repeater rptLoan = e.Item.FindControl("rptLoan") as Repeater;
                DataTable loan = objNews.GetFollowedCompanyNewss(this.UserId, Convert.ToInt64(row.Row["CIKCode"]), "L");

                if (loan.Rows.Count>0)
                {
                    rptLoan.DataSource = loan;
                    rptLoan.DataBind();
                }
                else
                {
                    StringBuilder js2 = new StringBuilder("").AppendLine();
                    //js2.Append("#CIK" + row.Row["CIKCode"] + " div:nth-child(1) { color: #ccc !important;}").AppendLine();

                    //HtmlGenericControl ctrlX = new HtmlGenericControl("style");
                    //ctrlX.Attributes.Add("type", "text/css");

                    js2.Append("$( document ).ready(function() {").AppendLine();

                    js2.Append("$('#CIK" + row.Row["CIKCode"] + " div:nth-child(1)').addClass('disabled').removeClass('divCollapses')").AppendLine();

                    js2.Append("});").AppendLine();
                    HtmlGenericControl ctrlX = new HtmlGenericControl("script");
                    ctrlX.Attributes.Add("type", "text/javascript");
                    ctrlX.InnerHtml = js2.ToString();
                    this.Page.Header.Controls.Add(ctrlX);
                }


                Repeater rptBond = e.Item.FindControl("rptBond") as Repeater;
                DataTable bond = objNews.GetFollowedCompanyNewss(this.UserId, Convert.ToInt64(row.Row["CIKCode"]), "B");

                if (bond.Rows.Count > 0)
                {
                    rptBond.DataSource = bond;
                    rptBond.DataBind();
                }
                else
                {
                    StringBuilder js2 = new StringBuilder("").AppendLine();
                    //js2.Append("#CIK" + row.Row["CIKCode"] + " div:nth-child(3) { color: #ccc !important;}").AppendLine();

                    //HtmlGenericControl ctrlX = new HtmlGenericControl("style");
                    //ctrlX.Attributes.Add("type", "text/css");

                    js2.Append("$( document ).ready(function() {").AppendLine();

                    js2.Append("$('#CIK" + row.Row["CIKCode"] + " div:nth-child(3)').addClass('disabled').removeClass('divCollapses')").AppendLine();

                    js2.Append("});").AppendLine();
                    HtmlGenericControl ctrlX = new HtmlGenericControl("script");
                    ctrlX.Attributes.Add("type", "text/javascript");
                    ctrlX.InnerHtml = js2.ToString();
                    this.Page.Header.Controls.Add(ctrlX);
                }


                Repeater rptStock = e.Item.FindControl("rptStock") as Repeater;
                DataTable stock = objNews.GetFollowedCompanyNewss(this.UserId, Convert.ToInt64(row.Row["CIKCode"]), "P");

                if (stock.Rows.Count > 0)
                {
                    rptStock.DataSource = stock;
                    rptStock.DataBind();
                }
                else
                {
                    StringBuilder js2 = new StringBuilder("").AppendLine();
                    //js2.Append("#CIK" + row.Row["CIKCode"] + " div:nth-child(5) { color: #ccc !important;}").AppendLine();

                    //HtmlGenericControl ctrlX = new HtmlGenericControl("style");
                    //ctrlX.Attributes.Add("type", "text/css");

                    js2.Append("$( document ).ready(function() {").AppendLine();

                    js2.Append("$('#CIK" + row.Row["CIKCode"] + " div:nth-child(5)').addClass('disabled').removeClass('divCollapses')").AppendLine();

                    js2.Append("});").AppendLine();
                    HtmlGenericControl ctrlX = new HtmlGenericControl("script");
                    ctrlX.Attributes.Add("type", "text/javascript");
                    ctrlX.InnerHtml = js2.ToString();
                    this.Page.Header.Controls.Add(ctrlX);
                }
            }
        }

        protected void rptMajorIndustryGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CMI_NewsController objNews = new CMI_NewsController();
                var row = e.Item.DataItem as System.Data.DataRowView;

                Repeater rptLoan = e.Item.FindControl("rptLoan") as Repeater;
                DataTable loan = objNews.GetFollowedMajorIndustryGroupNewss(this.UserId, Convert.ToInt32(row.Row["MajorIndustryGroupID"]), "L");

                if (loan.Rows.Count > 0)
                {
                    rptLoan.DataSource = loan;
                    rptLoan.DataBind();
                }
                else
                {
                    StringBuilder js2 = new StringBuilder("").AppendLine();

                    js2.Append("$( document ).ready(function() {").AppendLine();

                    js2.Append("$('#MajorIndustryGroupID" + row.Row["MajorIndustryGroupID"] + " div:nth-child(1)').addClass('disabled').removeClass('divCollapses')").AppendLine();

                    js2.Append("});").AppendLine();
                    HtmlGenericControl ctrlX = new HtmlGenericControl("script");
                    ctrlX.Attributes.Add("type", "text/javascript");
                    ctrlX.InnerHtml = js2.ToString();
                    this.Page.Header.Controls.Add(ctrlX);
                }


                Repeater rptBond = e.Item.FindControl("rptBond") as Repeater;
                DataTable bond = objNews.GetFollowedMajorIndustryGroupNewss(this.UserId, Convert.ToInt32(row.Row["MajorIndustryGroupID"]), "B");

                if (bond.Rows.Count > 0)
                {
                    rptBond.DataSource = bond;
                    rptBond.DataBind();
                }
                else
                {
                    StringBuilder js2 = new StringBuilder("").AppendLine();

                    js2.Append("$( document ).ready(function() {").AppendLine();

                    js2.Append("$('#MajorIndustryGroupID" + row.Row["MajorIndustryGroupID"] + " div:nth-child(3)').addClass('disabled').removeClass('divCollapses')").AppendLine();

                    js2.Append("});").AppendLine();
                    HtmlGenericControl ctrlX = new HtmlGenericControl("script");
                    ctrlX.Attributes.Add("type", "text/javascript");
                    ctrlX.InnerHtml = js2.ToString();
                    this.Page.Header.Controls.Add(ctrlX);
                }


                Repeater rptStock = e.Item.FindControl("rptStock") as Repeater;
                DataTable stock = objNews.GetFollowedMajorIndustryGroupNewss(this.UserId, Convert.ToInt32(row.Row["MajorIndustryGroupID"]), "P");

                if (stock.Rows.Count > 0)
                {
                    rptStock.DataSource = stock;
                    rptStock.DataBind();
                }
                else
                {
                    StringBuilder js2 = new StringBuilder("").AppendLine();

                    js2.Append("$( document ).ready(function() {").AppendLine();

                    js2.Append("$('#MajorIndustryGroupID" + row.Row["MajorIndustryGroupID"] + " div:nth-child(5)').addClass('disabled').removeClass('divCollapses')").AppendLine();

                    js2.Append("});").AppendLine();
                    HtmlGenericControl ctrlX = new HtmlGenericControl("script");
                    ctrlX.Attributes.Add("type", "text/javascript");
                    ctrlX.InnerHtml = js2.ToString();
                    this.Page.Header.Controls.Add(ctrlX);
                }
            }
        }
    }

}