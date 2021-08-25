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

namespace CMI_News
{
    /// <summary>
    /// User control for uploading a file
    /// </summary>
    public partial class EditMajorIndustryGroup : System.Web.UI.UserControl
    {
        private object _dataItem = null;


        public object DataItem
        {
            get
            {
                return this._dataItem;
            }
            set
            {
                this._dataItem = value;
            }
        }
        
        protected void Page_Load(object sender, System.EventArgs e)
        {
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
        //protected void cmdUpload_Click(object sender, EventArgs e)
        //{
        //    clearMessage();
        //    int ImageWidth = 38;
        //    int ImageHeight = 33;
        //    if (fileUpload.HasFile)
        //    {
                    
        //        string fileNameSave = fileUpload.FileName;

        //        string path = Server.MapPath(CMI_NewsController.MAJORINDUSTRYGROUPBANNERPATH);
        //        /*if (ImageWidth > 0 && ImageHeight > 0 && !Common.ValidateExactImageDimensions(fileUpload.PostedFile, ImageWidth, ImageHeight))
        //        {
        //            setErrorMessage("Banner image has to be " + ImageWidth.ToString() + " px X " + ImageHeight.ToString() + " px!",0);
        //            return;
        //        }
        //        */
        //        if (string.IsNullOrEmpty(path))
        //        {
        //            setErrorMessage("System error. No Destination Folder was specified");
        //            return;
        //        }


        //        string savedFilePath = System.IO.Path.Combine(path, fileUpload.FileName);
        //        fileUpload.SaveAs(savedFilePath);
        //        imgBanner.ImageUrl = CMI_NewsController.MAJORINDUSTRYGROUPBANNERPATH + "/" + fileUpload.FileName;

        //        setInfoMessage("Successfully uploaded a file.");

        //    }
        //    else
        //    {
        //        setErrorMessage("No file was specified.");
        //    }
        //}

        //private void clearMessage()
        //{
        //     lblMsg.Text = "";

        //}

        //private void setInfoMessage(string msg)
        //{
        //    lblMsg.Text = msg;
        //    lblMsg.ForeColor = System.Drawing.Color.Black;
        //}


        //private void setErrorMessage(string msg)
        //{
        //    lblMsg.Text = msg;
        //    lblMsg.ForeColor = System.Drawing.Color.Red;
        //}
    }
}