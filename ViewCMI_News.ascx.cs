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
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using DotNetNuke;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Entities.Users;
using System.IO;
using System.Linq;
using Telerik.Web.UI;
using DotNetNuke.Entities.Controllers;

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
    partial class ViewCMI_News : PortalModuleBase, IActionable
    {

        bool bolError;
        #region "Event Handlers"

        /// <summary>
        /// 
        /// </summary>
        protected BaseNewsUserControl control = null;

        /// <summary>
        /// dynamically create the user control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {

            // get the data to display (from the tabmodule setting)
            control = BaseNewsUserControl.GetInstance(this);

            if (control != null)
            {
                phControls.Controls.Add(control);
            }
        }
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Page_Load runs when the control is loaded 
        /// </summary> 
        /// ----------------------------------------------------------------------------- 
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (control == null)
            {
                pnlNewsSearch.Visible = true;
                pnlControls.Visible = false;

                try
                {
                    CMI_NewsController objNews = new CMI_NewsController();

                    if ((Request.QueryString["NewsId"] != null))
                    {
                        int NewsId = Int32.Parse(Request.QueryString["NewsId"]);
                        PnlSearch.Visible = false;
                        pnlDetail.Visible = true;
                        lblNewsID.Text = NewsId.ToString();
                        CMI_NewsInfo news = objNews.GetCMI_News(NewsId);

                        lblAcceptanceDate.Text = news.AcceptanceDate.ToShortDateString();
                        lblFilingDate.Text = news.FilingDate.ToShortDateString();

                        lblCIKCode.Text = news.CIKCode.ToString();
                        lblCompanyName.Text = news.CompanyName;

                        lblSICCode.Text = news.SICCode.ToString();
                        lblIndustry.Text = news.Industry;
                        lblIsLoan.Text = (news.IsLoan) ? "Yes" : "No";
                        lblIsBond.Text = (news.IsBond) ? "Yes" : "No";
                        lblIsStock.Text = (news.IsStock) ? "Yes" : "No";
                        lblNewMoneyLoan.Text = news.NewMoneyLoan.ToString();
                        lblNewMoneyBondNotes.Text = news.NewMoneyBondNotes.ToString();
                        //lblProposedPricing.Text = news.ProposedPricing.ToString();
                        lblClosed.Text = (news.Closed) ? "Yes" : "No";
                        lblActualCurrency.Text = news.ActualCurrency;
                        lblk8FormDescription.Text = news.k8FormDescription;
                        lblDocumentIncluded.Text = (news.DocumentIncluded) ? "Yes" : "No";
                        hplFilingHyperlink.Text = news.FilingHyperlink;
                        hplFilingHyperlink.NavigateUrl = news.FilingHyperlink;
                        lblHeadlineDescription.Text = news.HeadlineDescription;
                        hplK8FormHyperlink.Text = news.K8FormHyperlink;
                        hplK8FormHyperlink.NavigateUrl = news.K8FormHyperlink;
                        lblFeatured.Text = (news.Featured > 0) ? "Yes" : "No";
                    }
                    else
                    {
                        //setCompanyList();
                        DataTable dtGroup = objNews.GetCMI_MajorIndustryGroups();
                        if (!Page.IsPostBack)
                        {
                            Session["SearchResult"] = null;
                            cklMajorIndustryGroup.DataSource = dtGroup;
                            cklMajorIndustryGroup.DataBind();

                            DataTable dt = objNews.GetCMI_NewsPreferenceCompany(UserController.Instance.GetCurrentUserInfo().UserID);
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

                                    js2.Append("$('.dropdown-mul-1').find('.dropdown-chose-list').prepend('<span class=\"dropdown-selected\">' + company + '<i class=\"del\" data-id=\"" + dr["CikCode"] + "\"></i></span>');").AppendLine();
                                }

                                js2.Append("});").AppendLine();
                                HtmlGenericControl ctrlX = new HtmlGenericControl("script");
                                ctrlX.Attributes.Add("type", "text/javascript");
                                ctrlX.InnerHtml = js2.ToString();
                                this.Page.Header.Controls.Add(ctrlX);
                            }
                        }
                        else
                        {
                            if (hidCIKs.Value.Length > 0)
                            {
                                StringBuilder js3 = new StringBuilder("").AppendLine();
                                string[] d1 = new string[] { "," };
                                string[] ciks = hidCIKs.Value.Split(d1, StringSplitOptions.None).Reverse().ToArray();
                                js3.Append("$( document ).ready(function() {").AppendLine();

                                foreach (var cik in ciks)
                                {
                                    //js3.Append("var obj = ciks.find(o => o.id == '" + cik + "');").AppendLine();
                                    js3.Append("var obj = altFind(ciks, function(e) { return e.id == '" + cik + "';});").AppendLine();
                                    js3.Append("var company = '';").AppendLine();
                                    js3.Append("if (obj != null) ").AppendLine();
                                    js3.Append("company=obj.name; ").AppendLine();

                                    js3.Append("$('.dropdown-mul-1').find('.dropdown-chose-list').prepend('<span class=\"dropdown-selected\">' + company + '<i class=\"del\" data-id=\"" + cik + "\"></i></span>');").AppendLine();
                                }

                                js3.Append("});").AppendLine();
                                HtmlGenericControl ctrlX = new HtmlGenericControl("script");
                                ctrlX.Attributes.Add("type", "text/javascript");
                                ctrlX.InnerHtml = js3.ToString();
                                this.Page.Header.Controls.Add(ctrlX);
                            }
                        }

                    }
                }

                catch (Exception exc)
                {
                    //Module failed to load 
                    Exceptions.ProcessModuleLoadException(this, exc);
                }
            }
            else
            {
                pnlControls.Visible = true;
                pnlNewsSearch.Visible = false;
            } 
            

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            StringBuilder js = new StringBuilder("").AppendLine();
            js.Append("$( document ).ready(function() {").AppendLine();
            int i = 0;
            CMI_NewsController objNews = new CMI_NewsController();
            DataTable dtGroup = objNews.GetCMI_MajorIndustryGroups();
            foreach (DataRow dr in dtGroup.Rows)
            {
                DataTable dt2d = objNews.GetCMI_Industries2DigitByGroup(Convert.ToInt32(dr["MajorIndustryGroupID"]));
                string s2digit = "";

                foreach (DataRow dr2 in dt2d.Rows)
                {
                    s2digit += dr2["SIC2Digit"].ToString() + " - " + dr2["Industry2Digit"].ToString() + "&#013;";
                }
                js.Append("$('label[for=\"cklMajorIndustryGroup_" + i + "\"]').append('&#010;&#010;<img src=\"/images/helpI-icn-grey.png\" alt=\"" + s2digit + "\" title=\"" + s2digit + "\">');").AppendLine();

                i++;
            }
            DataTable dt2 = objNews.GetCMI_NewsPreferenceMajorIndustryGroup(UserController.Instance.GetCurrentUserInfo().UserID);

            if (dt2.Rows.Count > 0)
            {


                foreach (DataRow dr in dt2.Rows)
                {
                    js.Append("$(\"input[value='" + dr["MajorIndustryGroupId"] + "']\").prop('checked', true);").AppendLine();
                }


            }
            js.Append("});").AppendLine();
            HtmlGenericControl ctrlY = new HtmlGenericControl("script");
            ctrlY.Attributes.Add("type", "text/javascript");
            ctrlY.InnerHtml = js.ToString();
            this.Page.Header.Controls.Add(ctrlY);
        }

        protected void rgrList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            //reBindGrid();
        }
        protected void rgrList_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "InitInsert")
            {
                Response.Redirect(EditUrl());
            }
        }
        protected void rgrList_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

            if (e.CommandName.Equals("Delete"))
            {

                GridEditableItem dataItem = (GridEditableItem)e.Item;
                CMI_NewsController objNews = new CMI_NewsController();
                int NewsID = Convert.ToInt32(dataItem.GetDataKeyValue("NewsID").ToString());

                objNews.DeleteCMI_News(NewsID);

                reBindGrid();

            }
        }

        protected void rgrList_EditCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

            if (e.CommandName.Equals("Edit"))
            {
                GridEditableItem dataItem = (GridEditableItem)e.Item;

                Response.Redirect(EditUrl("NewsID",dataItem.GetDataKeyValue("NewsID").ToString()));

            }
        }

        protected void rgrList_PreRender(object sender, EventArgs e)
        {
            //if (!UserController.Instance.GetCurrentUserInfo().IsSuperUser)
            if (!UserInfo.IsInRole("Administrators"))
            //if (!IsEditable)
            {
                rgrList.MasterTableView.RenderColumns[rgrList.MasterTableView.RenderColumns.Length - 4].Display = false;
                rgrList.MasterTableView.RenderColumns[rgrList.MasterTableView.RenderColumns.Length - 1].Display = false;
                rgrList.MasterTableView.RenderColumns[rgrList.MasterTableView.RenderColumns.Length - 2].Display = false;
            }

        }
        private void reBindGrid()
        {
            List<string> groups = new List<string>();
            foreach (ListItem item in cklMajorIndustryGroup.Items)
                if (item.Selected)
                    groups.Add(item.Value);

            string groupIds = string.Join(",", groups.ToArray());

            CMI_NewsController objNews = new CMI_NewsController();
            List<CMI_NewsInfo> news;
            //if (UserController.Instance.GetCurrentUserInfo().IsSuperUser)
            //if (IsEditable)
            if (UserInfo.IsInRole("Administrators"))
            {

                news = objNews.GetCMI_NewssSearch(1, UserController.Instance.GetCurrentUserInfo().UserID, txtStartDate.Text, txtEndDate.Text, groupIds, hidCIKs.Value);
                if (news.Count > 0)
                {
                    lblNum.Text = news.Count.ToString() + " transactions found ";
                    lblNum.Visible = true;
                    btnDownload.Visible = true;
                    btnDownloadPl.Visible = true;
                    rgrList.Visible = true;
                }
                else
                {
                    lblNum.Text = "No transactions found. ";
                    lblNum.Visible = true;
                    btnDownload.Visible = false;
                    btnDownloadPl.Visible = false;
                    rgrList.Visible = false;
                }
            }
            else
            {
                int nTotal = objNews.GetCMI_NewssSearchNum(txtStartDate.Text, txtEndDate.Text, groupIds, hidCIKs.Value);

                news = objNews.GetCMI_NewssSearch(0, UserController.Instance.GetCurrentUserInfo().UserID, txtStartDate.Text, txtEndDate.Text, groupIds, hidCIKs.Value);

                if (nTotal == 0)
                {
                    lblNum.Text = "No transaction found. ";
                    lblNum.Visible = true;
                    btnDownload.Visible = false;
                    rgrList.Visible = false;
                    lblNoDownload.Visible = false;
                }
                else
                {
                    int nDownload = objNews.GetNum_NewsUserDownload(this.UserId);
                    int nMaxRows = Convert.ToInt32(HostController.Instance.GetString("CMINewsMaximumDownloadNum"));

                    rgrList.Visible = true;
                    int nRows = Convert.ToInt32(HostController.Instance.GetString("CMINewsSearchShowNum"));

                    if (nTotal > nRows)
                    {
                        lblNum.Text = nRows.ToString() + " transactions of " + nTotal + " transactions.";
                    }
                    else
                    {
                        lblNum.Text = news.Count.ToString() + " transactions found ";
                        nRows = nTotal;
                    }
                    lblNum.Visible = true;

                    if (nMaxRows - nDownload >= nRows)
                    {
                        btnDownload.Visible = true;
                        lblNoDownload.Visible = false;
                    }
                    else
                    {
                        lblNoDownload.Visible = true;
                        lblNoDownload.Text = "Daily Download limit " + nMaxRows.ToString() + ". You have downloaded " + nDownload.ToString() + " transactions today, " + nRows + " transactions of the search results cannot be downloaded.";
                    }


                }
            }

            Session["SearchResult"] = news;

            rgrList.DataSource = news;
            rgrList.DataBind();
        }
        #endregion
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            reBindGrid();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string newsDir = Path.Combine(DotNetNuke.Common.Globals.ApplicationMapPath, NewsCommon.NEWSUPLOAD);

            if (!Directory.Exists(newsDir))
                Directory.CreateDirectory(newsDir);

            string folderName = "CMI_News_Download_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString("00") + "_" + DateTime.Now.Day.ToString("00") + "_" + DateTime.Now.Hour.ToString("00") + "_" + DateTime.Now.Minute.ToString("00") + "_" + DateTime.Now.Second.ToString("00") + ".csv";
            StreamWriter swrCourse;
            string filePath = newsDir + @"\" + folderName;
            swrCourse = File.AppendText(filePath);
            List<CMI_NewsInfo> news = (List<CMI_NewsInfo>)Session["SearchResult"];

            //if (UserController.Instance.GetCurrentUserInfo().IsSuperUser)
            //if (IsEditable)
            if (UserInfo.IsInRole("Administrators"))
            {
                string[] Header2 = new[] {"Filing Form", "CIK", "Company",
                "Acceptance Date", "Filing Date", 
                "SIC Code -Industry", "Major Industry Group","Specific Industry", "Debt Transaction (Loan/Bond/Both)",   
                "New Money Loan", "New Money Bond/Notes",  "Proposed Loan (Not Completed)", 
                "Proposed Bonds/Notes (Not Completed)", "Closed (Y)",  "Actual Currency",
                "8k Form Description",
                "Document Included", "Filing Hyperlink",
                "Headline Description", "8K Form",
                "Rank", "Image ID", "NewsID"};

                //swrCourse.WriteLine(string.Join(((char)9).ToString(), Header1));

                if (clickedButton.ID == "btnDownloadPl")
                    swrCourse.WriteLine(string.Join("|", Header2));
                else
                    swrCourse.WriteLine(string.Join(",", Header2));

                foreach (CMI_NewsInfo aNews in news)
                {
                    string LB = "";
                    if (aNews.IsLoan)
                        LB += "L";

                    if (aNews.IsBond)
                        LB += "B";

                    if (aNews.IsStock)
                        LB += "P";

                    string[] Data2 = new[] { aNews.FilingForm, aNews.CIKCode.ToString(),
                        aNews.CompanyName, aNews.AcceptanceDate.ToString("yyyy-MM-dd"), aNews.FilingDate.ToString("yyyy-MM-dd"),
                        aNews.SICCode.ToString(), aNews.MajorIndustryGroup, aNews.Industry, LB,
                        aNews.NewMoneyLoan.ToString(), aNews.NewMoneyBondNotes.ToString(), aNews.ProposedLoan.ToString(),
                        aNews.ProposedBondNotes.ToString(), NewsCommon.ToYN(aNews.Closed), aNews.ActualCurrency,
                        Regex.Replace(aNews.k8FormDescription, @"\r\n?|\n", " "),
                        NewsCommon.ToYN(aNews.DocumentIncluded), aNews.FilingHyperlink, 
                        Regex.Replace(aNews.HeadlineDescription, @"\r\n?|\n", " "), aNews.K8FormHyperlink,
                        aNews.Featured.ToString(), aNews.ImageID.ToString(), aNews.NewsID.ToString() };

                    //swrCourse.WriteLine(string.Join(((char)9).ToString(), Data1));

                    if (clickedButton.ID == "btnDownloadPl")
                        swrCourse.WriteLine(string.Join("|", Data2));
                    else
                        swrCourse.WriteLine(string.Join(",", Data2.Select(x => string.Format("\"{0}\"", x)).ToList()));
                }
            }
            else
            {
                //string[] Header1 = new[] { "Filing Date", "Company", "Headline Description", "Industry", "SIC Code", "Major Industry Group", "8K Form Hyperlink", "Filing Hyperlink" };
                string[] Header1 = new[] { "Filing Date", "Company", "Headline Description", "Industry", "SIC Code", "Major Industry Group", "8K Form Hyperlink" };

                //swrCourse.WriteLine(string.Join(((char)9).ToString(), Header1));
                swrCourse.WriteLine(string.Join(",", Header1));

                foreach (CMI_NewsInfo aNews in news)
                {
                    //string[] Data1 = new[] { aNews.FilingDate.ToString("yyyy-MM-dd"), aNews.CompanyName, Regex.Replace(aNews.HeadlineDescription, @"\r\n?|\n", " "), aNews.Industry, aNews.SICCode.ToString(), aNews.MajorIndustryGroup, aNews.K8FormHyperlink, aNews.FilingHyperlink };

                    string[] Data1 = new[] { aNews.FilingDate.ToString("yyyy-MM-dd"), aNews.CompanyName, Regex.Replace(aNews.HeadlineDescription, @"\r\n?|\n", " "), aNews.Industry, aNews.SICCode.ToString(), aNews.MajorIndustryGroup, aNews.K8FormHyperlink };
                    //swrCourse.WriteLine(string.Join(((char)9).ToString(), Data1));

                    //swrCourse.WriteLine(string.Join(",", Data1));

                    swrCourse.WriteLine(string.Join(",", Data1.Select(x => string.Format("\"{0}\"", x)).ToList()));
                }


                CMI_NewsController objNews = new CMI_NewsController();
                objNews.Add_NewsUserDownload(this.UserId, news.Count);

            }


            swrCourse.Close();

            swrCourse.Dispose();

            FileInfo file = new FileInfo(filePath);
            if (file.Exists)
            {
                Response.Clear();
                Response.ClearHeaders();
                Response.ClearContent();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "text/plain";
                //"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //"application/vnd.xls"
                Response.Flush();
                Response.TransmitFile(file.FullName);
                Response.End();
            }
        }

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
                Actions.Add(GetNextActionID(), "Upload News Items",
                   ModuleActionType.AddContent, "", "add.gif", EditUrl("UploadNews"), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
                    true, false);

                Actions.Add(GetNextActionID(), "Add a News Item",
                   ModuleActionType.AddContent, "", "add.gif", EditUrl(), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
                    true, false);

                Actions.Add(GetNextActionID(), "Latest News List",
                   ModuleActionType.AddContent, "", "edit.gif", EditUrl("LatestNewsAdmin"), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
                    true, false);
/*
                Actions.Add(GetNextActionID(), "All News List",
                   ModuleActionType.AddContent, "", "edit.gif", EditUrl("NewsAdmin"), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
                   true, false);
*/ 
                Actions.Add(GetNextActionID(), "Company",
                   ModuleActionType.AddContent, "", "edit.gif", EditUrl("Company"), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
                    true, false);

                Actions.Add(GetNextActionID(), "Industry",
                   ModuleActionType.AddContent, "", "edit.gif", EditUrl("IndustryGroup"), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
                    true, false);

                Actions.Add(GetNextActionID(), "Upload Images for Top 3",
                   ModuleActionType.AddContent, "", "add.gif", EditUrl("UploadImages"), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
                    true, false);

                return Actions;
            }
        }

        #endregion

    }

}