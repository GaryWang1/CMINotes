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
using Microsoft.VisualBasic.FileIO;
using System.IO;
using System.Globalization;
using System.Data.SqlClient;
using System.Web;
using System.Text.RegularExpressions;

namespace CMI_News
{
    public partial class UploadImages : PortalModuleBase, IActionable
    {

        int i;
        CMI_NewsController objNews = new CMI_NewsController();

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
                if (!IsPostBack)
                {
                    string newsDir2 = Path.Combine(DotNetNuke.Common.Globals.ApplicationMapPath, NewsCommon.CMINEWS);

                    if (!Directory.Exists(newsDir2))
                    {
                        Directory.CreateDirectory(newsDir2);
                    }

                    string newsDir = Path.Combine(DotNetNuke.Common.Globals.ApplicationMapPath, NewsCommon.IMAGEBANK);


                    if (!Directory.Exists(newsDir))
                    {
                        Directory.CreateDirectory(newsDir);
                    }
                    
                    showData();
                }
            }

            catch (Exception exc)
            {
                //Module failed to load 
                Exceptions.ProcessModuleLoadException(this, exc);
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
            {
                ModuleActionCollection Actions = new ModuleActionCollection();

                Actions.Add(GetNextActionID(), "View",
                   ModuleActionType.AddContent, "", "view.gif", DotNetNuke.Common.Globals.NavigateURL(), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
                    true, false);

                return Actions;
            }
        }

        #endregion

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {

                string newsDir2 = Path.Combine(DotNetNuke.Common.Globals.ApplicationMapPath, NewsCommon.CMINEWS);

                if (!Directory.Exists(newsDir2))
                {
                    Directory.CreateDirectory(newsDir2);
                }

                string newsDir = Path.Combine(DotNetNuke.Common.Globals.ApplicationMapPath, NewsCommon.IMAGEBANK);


                if (!Directory.Exists(newsDir))
                {
                    Directory.CreateDirectory(newsDir);
                }

                if (!fupFileUpload.HasFile)
                {
                    FailureStatusDiv("Please select a proper file to upload.");
                }
                else
                {
                    string txtStatus="";
                    string txtStatusErr = "";
                    foreach (HttpPostedFile uploadedFile in fupFileUpload.PostedFiles)
                    {
                        if (checkFileName(uploadedFile.FileName))
                        {
                            uploadedFile.SaveAs(System.IO.Path.Combine(newsDir, uploadedFile.FileName));
                            txtStatus += String.Format("{0}<br />", uploadedFile.FileName);
                        }
                        else
                            txtStatusErr += String.Format("{0}<br />", uploadedFile.FileName);
                    }

                    if (txtStatusErr.Length>0)
                        FailureStatusDiv("The following image(s)<br />" + txtStatusErr + " can not be uploadedd due to incorrect file naming convention. ");
                    if (txtStatus.Length > 0)
                        SuccessStatusDiv("The following image(s)<br />" + txtStatus + " has/have been uploaded.");
                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        private bool checkFileName(string fName)
        {
            //iStock-nnnnnnn.jpg
            String fRegex = @"^istock-\d+\.jpg$";
           if(Regex.IsMatch(fName.ToLower(), fRegex))
           {
                return true;
           }
           else
           {
                return false;
           }
        }

        private void SuccessStatusDiv(string StatusMessage)
        {
            lblStatus.Text = StatusMessage;
            status.Visible = true;
            //status.Attributes["Class"] = "statusSuccess";
        }

        private void FailureStatusDiv(string StatusMessage)
        {
            lblStatusErr.Text = StatusMessage;
            statusErr.Visible = true;
            //statusErr.Attributes["Class"] = "statusFailure";
        }
        protected void btnDeleteFile_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow grow in gvFile.Rows)
            {
                CheckBox chkdel = (CheckBox)grow.FindControl("chkDel");
                if (chkdel.Checked)
                {
                    string fName = grow.Cells[1].Text;
                    DeleteFile(fName);
                }
            }
            showData();
        }
        private void DeleteFile(string FileName)
        {
            string newsDir = Path.Combine(DotNetNuke.Common.Globals.ApplicationMapPath, NewsCommon.IMAGEBANK);
            File.Delete(newsDir + FileName);
        }

        private void showData()
        {

            string newsDir = Path.Combine(DotNetNuke.Common.Globals.ApplicationMapPath, NewsCommon.IMAGEBANK);
            DirectoryInfo dir = new DirectoryInfo(newsDir);
            
            List<string> fileName = new List<string>();
            foreach (FileInfo f in dir.GetFiles("*.jpg"))
            {
                fileName.Add(f.Name.Trim());

            }
            gvFile.DataSource = fileName;
            gvFile.DataBind();
        }
    }
}