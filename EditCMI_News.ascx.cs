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
using DotNetNuke;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Entities.Modules;
using System.IO;
using System.Text.RegularExpressions;

namespace CMI_News
{

    /// ----------------------------------------------------------------------------- 
    /// <summary> 
    /// The EditCMI_News class is used to manage content 
    /// </summary> 
    /// <remarks> 
    /// </remarks> 
    /// <history> 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    partial class EditCMI_News : PortalModuleBase
    {

        #region "Private Members" 

        private int NewsId = Null.NullInteger;

        #endregion

        #region "Event Handlers" 

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Page_Load runs when the control is loaded 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        protected void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                CMI_NewsController objNews = new CMI_NewsController();

                // Determine NewsId of CMI_News to Update 
                if ((Request.QueryString["NewsId"] != null))
                {
                    NewsId = Int32.Parse(Request.QueryString["NewsId"]);
                }

                if (!Page.IsPostBack)
                {
                    setCompanyList();
                    setImageList();

                    rblMajorIndustryGroup.DataSource = objNews.GetCMI_MajorIndustryGroups();
                    rblMajorIndustryGroup.DataBind();

                    cmdDelete.Attributes.Add("onClick", "javascript:return confirm('Are you sure you want to delete the news item?');");

                    if (!Null.IsNull(NewsId))
                    {
                        // get content 
                        CMI_NewsController objCMI_Newss = new CMI_NewsController();
                        CMI_NewsInfo objCMI_News = objCMI_Newss.GetCMI_News(NewsId);
                        if ((objCMI_News != null))
                        {
                            txtAcceptanceDate.Text = objCMI_News.AcceptanceDate.ToString("MM-dd-yyyy");
                            txtFilingDate.Text = objCMI_News.FilingDate.ToString("MM-dd-yyyy");
                            txtCIKCode.Text = objCMI_News.CIKCode.ToString();
                            txtCompanyName.Text = objCMI_News.CompanyName;
                            rblMajorIndustryGroup.SelectedValue = objCMI_News.MajorIndustryGroupId.ToString();

                            ddlIndustry.DataSource = objNews.GetCMI_IndustriesByGroup(objCMI_News.MajorIndustryGroupId);
                            ddlIndustry.DataBind();

                            ddlIndustry.SelectedValue = objCMI_News.SICCode.ToString();
                            ckbLoan.Checked = objCMI_News.IsLoan;
                            ckbBond.Checked = objCMI_News.IsBond;
                            ckbStock.Checked = objCMI_News.IsStock;
                            if (objCMI_News.NewMoneyLoan >0)
                                txtMoneyLoan.Text = objCMI_News.NewMoneyLoan.ToString("#.##");

                            if (objCMI_News.NewMoneyBondNotes>0)
                                txtMoneyBond.Text = objCMI_News.NewMoneyBondNotes.ToString("#.##");

                            chkClosed.Checked = objCMI_News.Closed;
                            chkDocumentIncluded.Checked = objCMI_News.DocumentIncluded;
                            if (objCMI_News.ProposedLoan>0)
                                txtProposedLoan.Text = objCMI_News.ProposedLoan.ToString("#.##");

                            if (objCMI_News.ProposedBondNotes>0)
                                txtProposedBondNotes.Text = objCMI_News.ProposedBondNotes.ToString("#.##");

                            txtActualCurrency.Text = objCMI_News.ActualCurrency;
                            txt8kFormDescription.Text = objCMI_News.k8FormDescription;
                            txtFilingHyperlink.Text = objCMI_News.FilingHyperlink;
                            txtHeadlineDescription.Text = objCMI_News.HeadlineDescription;
                            txt8KFormHyperlink.Text = objCMI_News.K8FormHyperlink;
                            rblFeatured.SelectedValue = objCMI_News.Featured.ToString();
                            if (objCMI_News.ImageID>0)
                                txtImageID.Text = objCMI_News.ImageID.ToString();
                        }
                    }

                    else
                    {
                        cmdDelete.Visible = false;

                    }
                }
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

            DataTable dtCom = objNews.GetCMI_Companies();

            //txtCIKCode.Attributes.Add("onkeyup", "CIKChanged(this.value)");
            StringBuilder js = new StringBuilder("").AppendLine();

            js.Append("var ciks = [").AppendLine();
            foreach (DataRow drCom in dtCom.Rows)
            {
                js.Append("{ value: \"" + drCom["CIKCode"].ToString() + "\", company: \"" + drCom["CompanyName"].ToString().Replace("'", "").Replace(@"\", " ") + "\"}").AppendLine();

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
            js.Append("        return false;").AppendLine();
            js.Append("      }").AppendLine();

            js.Append("    })").AppendLine();
            js.Append(" } );").AppendLine();


            HtmlGenericControl ctrlX = new HtmlGenericControl("script");
            ctrlX.Attributes.Add("type", "text/javascript");
            ctrlX.InnerHtml = js.ToString();
            this.Page.Header.Controls.Add(ctrlX);
        }

        private void setImageList()
        {
            string newsDir = Path.Combine(DotNetNuke.Common.Globals.ApplicationMapPath, NewsCommon.IMAGEBANK);
            DirectoryInfo dir = new DirectoryInfo(newsDir);

            StringBuilder js = new StringBuilder("").AppendLine();
            int i = 0;
            js.Append("var imgs = [").AppendLine();
            foreach (FileInfo f in dir.GetFiles("*.jpg"))
            { 
                js.Append("\"" + Regex.Match(f.Name.Trim(), @"\d+") + "\"").AppendLine();
                i++;
                if (i < dir.GetFiles("*.jpg").Length)
                    js.Append(", ").AppendLine();

            }
            js.Append(" ];").AppendLine();


            js.Append("$(document).ready(function () {").AppendLine();

            js.Append("      $( \"#txtImageID\" ).autocomplete({").AppendLine();
            js.Append("      minLength: 0,").AppendLine();
            js.Append("      source: imgs").AppendLine();

            js.Append("    })").AppendLine();
            js.Append(" } );").AppendLine();


            HtmlGenericControl ctrlX = new HtmlGenericControl("script");
            ctrlX.Attributes.Add("type", "text/javascript");
            ctrlX.InnerHtml = js.ToString();
            this.Page.Header.Controls.Add(ctrlX);
        }
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

                //objCMI_Newss.UpdateCMI_Company(txtCIKCode.Text, txtCompanyName.Text);
                
                CMI_NewsInfo objNews = new CMI_NewsInfo();
                objNews.AcceptanceDate = Convert.ToDateTime(txtAcceptanceDate.Text);
                objNews.FilingDate = Convert.ToDateTime(txtFilingDate.Text);
                objNews.CIKCode = Convert.ToInt64(txtCIKCode.Text);
                objNews.CompanyName = txtCompanyName.Text;
                objNews.MajorIndustryGroupId = -1;
                objNews.SICCode = Convert.ToInt32(ddlIndustry.SelectedValue);

                objNews.IsLoan = ckbLoan.Checked;

                objNews.IsBond = ckbBond.Checked;

                objNews.IsStock = ckbStock.Checked;
                if (txtMoneyLoan.Text.Length > 0)
                    objNews.NewMoneyLoan = Convert.ToInt32(txtMoneyLoan.Text);
                else
                    objNews.NewMoneyLoan = 0;

                if (txtMoneyBond.Text.Length > 0)
                    objNews.NewMoneyBondNotes = Convert.ToInt32(txtMoneyBond.Text);
                else
                    objNews.NewMoneyBondNotes = 0;

                if (txtProposedLoan.Text.Length > 0)
                    objNews.ProposedLoan = Convert.ToInt32(txtProposedLoan.Text);
                else
                    objNews.ProposedLoan = 0;

                if (txtProposedBondNotes.Text.Length > 0)
                    objNews.ProposedBondNotes = Convert.ToInt32(txtProposedBondNotes.Text);
                else
                    objNews.ProposedBondNotes = 0;

                if (txtImageID.Text.Length > 0)
                    objNews.ImageID = Convert.ToInt64(txtImageID.Text);
                else
                    objNews.ImageID = 0;

                objNews.Closed = chkClosed.Checked;
                objNews.DocumentIncluded = chkDocumentIncluded.Checked;

                objNews.ActualCurrency = txtActualCurrency.Text;
                objNews.k8FormDescription = txt8kFormDescription.Text;
                objNews.FilingHyperlink = txtFilingHyperlink.Text;
                objNews.HeadlineDescription = txtHeadlineDescription.Text;
                objNews.K8FormHyperlink = txt8KFormHyperlink.Text;

                objNews.Featured = Convert.ToInt32(rblFeatured.SelectedValue);
                objNews.FilingForm = txtFilingForm.Text;

                if (Null.IsNull(NewsId))
                {

                    objNews.MajorIndustryGroupId = -1;
                    objCMI_Newss.AddCMI_News(objNews);
                }
                else
                {
                    objNews.NewsID = NewsId;
                    objCMI_Newss.UpdateCMI_News(objNews);
                }

                // Redirect back to the portal home page 
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
                if (!Null.IsNull(NewsId))
                {

                    CMI_NewsController objCMI_Newss = new CMI_NewsController();
                    objCMI_Newss.DeleteCMI_News(NewsId);

                }

                // Redirect back to the portal home page 
                Response.Redirect(Globals.NavigateURL(), true);
            }
            catch (Exception exc)
            {
                //Module failed to load 
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion

        protected void rblMajorIndustryGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            CMI_NewsController objNews = new CMI_NewsController();
            
            ddlIndustry.DataSource = objNews.GetCMI_IndustriesByGroup(Convert.ToInt32(rblMajorIndustryGroup.SelectedValue));
            ddlIndustry.DataBind();
        }
    }
}
