using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Entities.Modules;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using DotNetNuke.Entities.Portals;
namespace CMI_News
{
    /// <summary>
    /// common constants, methods
    /// </summary>
    public class NewsCommon
    {
        #region singleton Constructor

        /// <summary>
        /// singleton instance of this class
        /// </summary>
        private static readonly NewsCommon instance = new NewsCommon();

        /// <summary>
        /// protected constructor
        /// </summary>
        protected NewsCommon()
        {
        }

        /// <summary>
        /// return singleton single of this class
        /// </summary>
        public static NewsCommon Instance { get { return instance; } }
        #endregion singleton Constructor

        #region Constants and Properties


        #pragma warning disable 1591

        public const string DATATYPE_Home_Top3 = "Home Top 3 News";
        public const string DATATYPE_Home_Table = "Home Table View News";
        public const string DATATYPE_NewsSearch = "News Search";

        public const string DATATYPE_User_Profile = "User Profile";
        public const string DATATYPE_User_Followed = "User Followed";

        public const string SETTING_DATATYPE = "DataType";

        public const string IMAGEBANK = "portals\\_default\\CMI_News\\imagesBank\\";
        public const string CMINEWS = "portals\\_default\\CMI_News\\";
        public const string NEWSUPLOAD = "portals\\_default\\CMI_News\\news_upload\\";
        #pragma warning restore 1591

        #endregion Constants and Properties

        #region Helper Methods


        /// <summary>
        /// Populate list of Data Types
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="isSchoolPortal"></param>
        public static void FillDataTypeList(System.Web.UI.WebControls.DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(NewsCommon.DATATYPE_Home_Top3);
            ddl.Items.Add(NewsCommon.DATATYPE_Home_Table);
            ddl.Items.Add(NewsCommon.DATATYPE_NewsSearch);
            ddl.Items.Add(NewsCommon.DATATYPE_User_Profile);
            ddl.Items.Add(NewsCommon.DATATYPE_User_Followed);

        }

        /// <summary>
        /// get the current data-type setting
        /// </summary>
        /// <param name="tabModuleId"></param>
        /// <returns></returns>
        public static string GetDataTypeSetting(int tabModuleId)
        {
            ModuleController objModules = new ModuleController();
            Hashtable htSettings = objModules.GetTabModuleSettings(tabModuleId);
            if (htSettings.ContainsKey(NewsCommon.SETTING_DATATYPE))
                return Convert.ToString(htSettings[NewsCommon.SETTING_DATATYPE]);
            else
                return "";
        }

        /// <summary>
        /// set the data-type-setting
        /// </summary>
        /// <param name="tabModuleId"></param>
        /// <param name="dataType"></param>
        public static void UpdateDataTypeSetting(int tabModuleId, string dataType)
        {
            ModuleController objModules = new ModuleController();
            objModules.UpdateTabModuleSetting(tabModuleId, NewsCommon.SETTING_DATATYPE, dataType);
        }
        //Validate File Is Image
        public static bool ValidateFileIsImage(string fileType)
        {
            switch (fileType)
            {
                case "image/gif":
                case "image/jpeg":
                case "image/pjpeg":
                    return true;
                //break;
                default:
                    return false;
                //break;
            }
        }

        //Validate Image Size
        public static bool ValidateImageSize(int fileSize, int maxSize)
        {

            if (maxSize * 1024 > fileSize)
                return true;
            else
                return false;
        }

        //Validate Max Image Dimensions
        public static bool ValidateMaxImageDimensions(HttpPostedFile file, int maxWidth, int maxHeight)
        {
            using (Bitmap bitmap = new Bitmap(file.InputStream, false))
            {
                if (bitmap.Width < maxWidth && bitmap.Height < maxHeight)
                    return true;
                else
                    return false;
            }
        }

        //Validate Exact Image Dimensions width - Max Image Dimensions height
        public static bool ValidateExactWMaxHImageDimensions(HttpPostedFile file, int exactWidth, int maxHeight)
        {
            using (Bitmap bitmap = new Bitmap(file.InputStream, false))
            {
                if (bitmap.Width == exactWidth && bitmap.Height < maxHeight)
                    return true;
                else
                    return false;
            }
        }
        //Validate Max Image Dimensions width - Exact Image Dimensions height
        public static bool ValidateExactHMaxWImageDimensions(HttpPostedFile file, int exactWidth, int maxHeight)
        {
            using (Bitmap bitmap = new Bitmap(file.InputStream, false))
            {
                if (bitmap.Width < exactWidth && bitmap.Height == maxHeight)
                    return true;
                else
                    return false;
            }
        }
        //Validate Exact Image Dimensions
        public static bool ValidateExactImageDimensions(HttpPostedFile file, int exactWidth, int exactHeight)
        {
            using (Bitmap bitmap = new Bitmap(file.InputStream, false))
            {
                if (bitmap.Width == exactWidth && bitmap.Height == exactHeight)
                    return true;
                else
                    return false;
            }
        }

        //GetFileSize from bites to KB or MB
        public static String GetFileSize(long x)
        {
            if (Convert.ToDouble(x) / 1024 / 1024 > 1)
                return (" (" + Math.Round(Convert.ToDouble(x) / 1024 / 1024, 1) + " MB)");
            else
                return (" (" + Math.Round(Convert.ToDouble(x) / 1024, 1) + " KB)");
        }

        /// <summary>
        /// Strips the HTML tags from strHTML.
        /// </summary>
        /// <param name="strHTML">strHTML</param>
        /// <returns></returns>
        public static string stripHTML(string strHTML)
        {
            Regex re = new Regex("<(.|\n)+?>");
            string strOutput = re.Replace(strHTML, "").Replace("&nbsp;", " ");

            return strOutput;

        }
        /// <summary>
        /// Strips the HTML tags from strHTML.
        /// </summary>
        /// <param name="strHTML">strHTML</param>
        /// <returns></returns>
        public static string stripHTML(string strHTML, int length)
        {
            Regex re = new Regex("<(.|\n)+?>");
            string strOutput = re.Replace(strHTML, "").Replace("&nbsp;", " ");

            return truncate(strOutput, length);

        }

        /// <summary>
        /// Strips the encode HTML tags from strHTML.
        /// </summary>
        /// <param name="strHTML">strHTML</param>
        /// <returns></returns>
        public static string stripEncodeHTML(string strHTML)
        {
            Regex re = new Regex("&lt;(.|\n)+?&gt;");
            string strOutput = re.Replace(strHTML, "").Replace("&amp;nbsp;", " ");

            return strOutput;
        }
        public static string truncate(string str, int length)
        {
            if (str.Length > length)
            {
                str = str.Substring(0, length);
                return str.Substring(0, str.LastIndexOf(' ')) + " ...";
            }
            else
                return str;
        }

        public static string replaceNewlines(string blockOfText, string replaceWith)
        {
            return blockOfText.Replace("\r\n", replaceWith).Replace("\n", replaceWith).Replace("\r", replaceWith);
        }
        public static string getDetailUrl(int portalId)
        {
            return PortalController.GetPortalSetting("DETAIL_PAGE_URL", portalId, DotNetNuke.Common.Globals.NavigateURL());
        }

        public static string ToYN(bool bl)
        {
            if (bl)
                return "Y";
            else
                return "N";
        }
        #endregion Helper Methods
    }
}
