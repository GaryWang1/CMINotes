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
using Telerik.Web.UI;
using System.Globalization;
using System.Data.SqlClient;

namespace CMI_News
{
    public partial class UploadNews : PortalModuleBase, IActionable
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
                    showData();
            }

            catch (Exception exc)
            {
                //Module failed to load 
                Exceptions.ProcessModuleLoadException(this, exc);
            }

        }
        public DataTable GetDataTableFromCSVFile(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    //csvReader.SetDelimiters(new string[] { ((char)9).ToString() });
                    csvReader.SetDelimiters(new string[] { "|" });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] fieldData;
                    /*
                    csvReader.ReadFields();
                    csvReader.ReadFields(); 
                    csvReader.ReadFields(); 
                    csvReader.ReadFields();
                    csvReader.ReadFields(); */
                    // read column names
                    string[] colFields;
                    do
                    {
                        colFields = csvReader.ReadFields();
                        i++;
                    }
                    while(colFields[0].ToString() != "Filing Form");

                    i++;
                    fieldData = colFields;
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }
                    //csvData.Rows.Add(fieldData);

                    while (!csvReader.EndOfData)
                    {
                        fieldData = csvReader.ReadFields();
                        if(fieldData[1].ToString().Length>0)
                            csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
                FailureStatusDiv(ex.Message);

                File.Delete(litFileLocation.Text);
            }
            return csvData;
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
            bool fileUploadStatus = HandleFileUpload(fupFileUpload, litFileLocation, ".CSV");

            if (!fileUploadStatus)
            {
                return;
            }

            DataTable dt = GetDataTableFromCSVFile(litFileLocation.Text);
  
            string errMsg = "";

            SqlDataProvider sqp = new SqlDataProvider();
            SqlConnection conn = new SqlConnection(sqp.ConnectionString);
            conn.Open();

            SqlTransaction transaction = conn.BeginTransaction(IsolationLevel.Serializable);

            foreach (DataRow dr in dt.Rows)
            {
                CMI_NewsInfo aNews = new CMI_NewsInfo();
                try
                {
                    string format = "yyyy-MM-dd";
                    DateTime FilingDate;
                    if (DateTime.TryParseExact(dr["Filing Date"].ToString(), format, CultureInfo.InvariantCulture, DateTimeStyles.None, out FilingDate))
                        aNews.FilingDate = FilingDate;
                    else
                        errMsg += "Row " + i.ToString() + ": Filing Date, " + dr["Filing Date"].ToString() + ", is not a valid date.<br />";

                    DateTime AcceptanceDate;
                    if (DateTime.TryParseExact(dr["Acceptance Date"].ToString(), format, CultureInfo.InvariantCulture, DateTimeStyles.None, out AcceptanceDate))
                        aNews.AcceptanceDate = AcceptanceDate;
                    else
                        errMsg += "Row " + i.ToString() + ": Acceptance Date, " + dr["Acceptance Date"].ToString() + ", is not a valid date.<br />";

                    long CIKCode;
                    if (long.TryParse(dr["CIK"].ToString(), out CIKCode))
                        aNews.CIKCode = CIKCode;
                    else
                        errMsg += "Row " + i.ToString() + ": CIKCode, " + dr["CIK"].ToString() + ", is not a valid number.<br />";

                    int SICCode;
                    if (int.TryParse(dr["SIC Code -Industry"].ToString(), out SICCode))
                        aNews.SICCode = SICCode;
                    else
                        errMsg += "Row " + i.ToString() + ": SICCode, " + dr["SIC Code -Industry"].ToString() + ", is not a valid number.<br />";

                    if (dr["Specific Industry"].ToString().Length < 256)
                        aNews.Industry = dr["Specific Industry"].ToString();
                    else
                        errMsg += "Row " + i.ToString() + ": Specific Industry, " + dr["Specific Industry"].ToString() + ", has to be less than 256 characters.<br />";

                    aNews.MajorIndustryGroup = dr["Major Industry Group"].ToString();

                    aNews.MajorIndustryGroupId = 0;

                    int Featured;
                    if (int.TryParse(dr["Rank"].ToString(), out Featured))
                        aNews.Featured = Featured;
                    else
                        errMsg += "Row " + i.ToString() + ": Rank " + dr["Rank"].ToString() + ", is not a valid number.<br />";
                    if (dr["New Money Loan"].ToString() != "")
                    {
                        decimal NewMoneyLoan;
                        if (decimal.TryParse(dr["New Money Loan"].ToString(), out NewMoneyLoan))
                            aNews.NewMoneyLoan = NewMoneyLoan;
                        else
                            errMsg += "Row " + i.ToString() + ": New Money Loan, " + dr["New Money Loan"].ToString() + ", is not a valid number.<br />";
                    }
                    if (dr["New Money Bond/Notes"].ToString() != "")
                    {
                        decimal NewMoneyBondNotes;
                        if (decimal.TryParse(dr["New Money Bond/Notes"].ToString(), out NewMoneyBondNotes))
                            aNews.NewMoneyBondNotes = NewMoneyBondNotes;
                        else
                            errMsg += "Row " + i.ToString() + ": New Money Bond Notes, " + dr["New Money Bond/Notes"].ToString() + ", is not a valid number.<br />";
                    }

                    if (dr["Proposed Loan (Not Completed)"].ToString() != "")
                    {
                        decimal ProposedLoan;
                        if (decimal.TryParse(dr["Proposed Loan (Not Completed)"].ToString(), out ProposedLoan))
                            aNews.ProposedLoan = ProposedLoan;
                        else
                            errMsg += "Row " + i.ToString() + ": Proposed Loan, " + dr["Proposed Loan (Not Completed)"].ToString() + ", is not a valid number.<br />";
                    }
                    if (dr["Proposed Bonds/Notes (Not Completed)"].ToString() != "")
                    {
                        decimal ProposedBondNotes;
                        if (decimal.TryParse(dr["Proposed Bonds/Notes (Not Completed)"].ToString(), out ProposedBondNotes))
                            aNews.ProposedBondNotes = ProposedBondNotes;
                        else
                            errMsg += "Row " + i.ToString() + ": Proposed Bond Notes, " + dr["Proposed Bonds/Notes (Not Completed)"].ToString() + ", is not a valid number.<br />";
                    }
                    if (dr["Actual Currency"].ToString().Length < 128)
                        aNews.ActualCurrency = dr["Actual Currency"].ToString();
                    else
                        errMsg += "Row " + i.ToString() + ": Actual Currency, " + dr["Actual Currency"].ToString() + ", has to be less than 128 characters.<br />";

                    if (dr["Filing Hyperlink"].ToString().Length > 0)
                    {
                        if (Uri.IsWellFormedUriString(dr["Filing Hyperlink"].ToString(), UriKind.Absolute))
                        {
                            if (dr["Filing Hyperlink"].ToString().Length < 256)
                                aNews.FilingHyperlink = dr["Filing Hyperlink"].ToString();
                            else
                                errMsg += "Row " + i.ToString() + ": Filing Hyperlink, " + dr["Filing Hyperlink"].ToString() + ", has to be less than 256 characters.<br />";
                        }
                        else
                            errMsg += "Row " + i.ToString() + ": Filing Hyperlink, " + dr["Filing Hyperlink"].ToString() + ", is invalid.<br />";
                    }
                    if (dr["8K Form"].ToString().Length > 0)
                    {
                        if (Uri.IsWellFormedUriString(dr["8K Form"].ToString(), UriKind.Absolute))
                        {
                            if (dr["8K Form"].ToString().Length < 256)
                                aNews.K8FormHyperlink = dr["8K Form"].ToString();
                            else
                                errMsg += "Row " + i.ToString() + ": 8K Form Hyperlink, " + dr["8K Form"].ToString() + ", has to be less than 256 characters.<br />";
                        }
                        else
                            errMsg += "Row " + i.ToString() + ": 8k Form Hyperlink, " + dr["8K Form"].ToString() + ", is invalid.<br />";
                    }

                    if (dr["Headline Description"].ToString().Length < 1000)
                        aNews.HeadlineDescription = dr["Headline Description"].ToString();
                    else
                        errMsg += "Row " + i.ToString() + ": Headline Description, " + dr["Headline Description"].ToString() + ", has to be less than 1000 characters.<br />";


                    if (dr["Image ID"].ToString() != "")
                    {
                        long ImageID;
                        if (long.TryParse(dr["Image ID"].ToString(), out ImageID))
                            aNews.ImageID = ImageID;
                        else
                            errMsg += "Row " + i.ToString() + ": Image ID, " + dr["Image ID"].ToString() + ", is not a valid number.<br />";
                    }

                    aNews.IsBond = false;
                    aNews.IsLoan = false;
                    aNews.IsStock = false;

                    if (dr["Debt Transaction (Loan/Bond/Both)"].ToString().IndexOf("L")!=-1)
                        aNews.IsLoan = true;

                    if (dr["Debt Transaction (Loan/Bond/Both)"].ToString().IndexOf("B") != -1)
                        aNews.IsBond = true;

                    if (dr["Debt Transaction (Loan/Bond/Both)"].ToString().IndexOf("P") != -1)
                        aNews.IsStock = true;

                    aNews.k8FormDescription = dr["8k Form Description"].ToString();
                    aNews.FilingForm = dr["Filing Form"].ToString();
                    aNews.CompanyName = dr["Company"].ToString();
                    if (dr["Closed (Y)"].ToString() == "Y")
                        aNews.Closed = true;
                    else
                        aNews.Closed = false;

                    if (dr["Document Included"].ToString() == "Y")
                        aNews.DocumentIncluded = true;
                    else
                        aNews.DocumentIncluded = false;

                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                    FailureStatusDiv(errMsg);
                    transaction.Rollback();
                    File.Delete(litFileLocation.Text);
                    break;
                }
                if (errMsg.Length == 0)
                {
                    try
                    {
                        objNews.AddCMI_News(transaction, aNews);
                    }
                    catch (Exception ex)
                    {
                        errMsg = ex.Message;
                        FailureStatusDiv(errMsg);
                        transaction.Rollback();
                        File.Delete(litFileLocation.Text);
                        break;
                    }
                    i++;
                }
                else
                {
                    FailureStatusDiv(errMsg);
                    transaction.Rollback();
                    File.Delete(litFileLocation.Text);
                    break;
                }
            }
            if (errMsg.Length == 0)
            {
                SuccessStatusDiv(dt.Rows.Count.ToString() + " records have been uploaded to database");
                transaction.Commit();

                //
            }

            conn.Close();

        }

        private bool HandleFileUpload(FileUpload UploadControl, Literal FileLocationControl, string FileExtension)
        {
            try
            {
                if (!UploadControl.HasFile)
                {
                    FailureStatusDiv("Please select a proper document to upload.");
                    return false;
                }

                FileExtension = FileExtension.ToLower();

                string newsDir2 = Path.Combine(DotNetNuke.Common.Globals.ApplicationMapPath, NewsCommon.CMINEWS);

                if (!Directory.Exists(newsDir2))
                {
                    Directory.CreateDirectory(newsDir2);
                }

                string newsDir = Path.Combine(DotNetNuke.Common.Globals.ApplicationMapPath, NewsCommon.NEWSUPLOAD);


                if (!Directory.Exists(newsDir))
                {
                    Directory.CreateDirectory(newsDir);
                }

                if (!UploadControl.FileName.EndsWith(FileExtension))
                {
                    FailureStatusDiv("Please choose a valid '." + FileExtension + "' document.");
                    return false;
                }
                string savedPath = Path.Combine(newsDir, UploadControl.FileName);


                if (File.Exists(savedPath))
                {
                    FailureStatusDiv("A file by the name of'" + UploadControl.FileName + "' has been already uploaded.");
                    return false;
                }

                FileLocationControl.Text = savedPath;

                UploadControl.SaveAs(FileLocationControl.Text);
                //SuccessStatusDiv("The document has been uploaded!");
                return true;
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
                return false;
            }
        }

        private void SuccessStatusDiv(string StatusMessage)
        {
            litStatus.Text = StatusMessage;
            status.Attributes["Class"] = "alert alert-success";
        }

        private void FailureStatusDiv(string StatusMessage)
        {
            litStatus.Text = StatusMessage;
            status.Attributes["Class"] = "alert alert-danger";
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
            string newsDir = Path.Combine(DotNetNuke.Common.Globals.ApplicationMapPath, NewsCommon.NEWSUPLOAD);
            File.Delete(newsDir + FileName);
        }

        private void showData()
        {

            string newsDir = Path.Combine(DotNetNuke.Common.Globals.ApplicationMapPath, NewsCommon.NEWSUPLOAD);
            DirectoryInfo dir = new DirectoryInfo(newsDir);

            List<string> fileName = new List<string>();
            foreach (FileInfo f in dir.GetFiles("*.csv"))
            {
                fileName.Add(f.Name.Trim());

            }
            gvFile.DataSource = fileName;
            gvFile.DataBind();
        }
    }
}