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
using System.Configuration;
using System.Data;
using System.Xml;
using System.Web;
using System.Collections.Generic;

using DotNetNuke;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Services.Search;
using DotNetNuke.Entities.Modules;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;

namespace CMI_News
{

    /// ----------------------------------------------------------------------------- 
    /// <summary> 
    /// The Controller class for CMI_News 
    /// </summary> 
    /// <remarks> 
    /// </remarks> 
    /// <history> 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public class CMI_NewsController 
    {
        //public const string MAJORINDUSTRYGROUPBANNERPATH = "/Portals/_default/CMI_NEWS_MajorIndustryGroupBanner";
        #region "Public Methods"

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// gets an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public List<CMI_NewsInfo> GetCMI_Newss()
        {

            return CBO.FillCollection<CMI_NewsInfo>(DataProvider.Instance().GetCMI_Newss());

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// gets an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public List<CMI_NewsInfo> GetCMI_NewssTop3()
        {

            return CBO.FillCollection<CMI_NewsInfo>(DataProvider.Instance().GetCMI_NewssTop3());

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// gets an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public List<CMI_NewsInfo> GetCMI_NewssPrev()
        {

            return CBO.FillCollection<CMI_NewsInfo>(DataProvider.Instance().GetCMI_NewssPrev());

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// gets an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public List<CMI_NewsInfo> GetCMI_NewssLatest()
        {

            return CBO.FillCollection<CMI_NewsInfo>(DataProvider.Instance().GetCMI_NewssLatest());

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// GetCMI_NewssSearchNum 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public int GetCMI_NewssSearchNum(string FilingDateFrom, string FilingDateTo, string MajorIndustryGroupIDs, string CIKCodes)
        {
            return Convert.ToInt32(DataProvider.Instance().GetCMI_NewssSearchNum(FilingDateFrom, FilingDateTo, MajorIndustryGroupIDs, CIKCodes));
   
       }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// gets an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public List<CMI_NewsInfo> GetCMI_NewssSearch(int Super, int UserId, string FilingDateFrom, string FilingDateTo,string MajorIndustryGroupIDs, string CIKCodes)
        {

            return CBO.FillCollection<CMI_NewsInfo>(DataProvider.Instance().GetCMI_NewssSearch(Super, UserId, FilingDateFrom, FilingDateTo,MajorIndustryGroupIDs, CIKCodes));

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// gets an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="NewsId">The Id of the item</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public CMI_NewsInfo GetCMI_News(int NewsId)
        {

            return (CMI_NewsInfo)CBO.FillObject(DataProvider.Instance().GetCMI_News(NewsId), typeof(CMI_NewsInfo));

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// adds an object to the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="objCMI_News">The CMI_NewsInfo object</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void AddCMI_News(CMI_NewsInfo objCMI_News)
        {

            if (objCMI_News.CIKCode >0)
            {
                DataProvider.Instance().AddCMI_News(objCMI_News);
            }

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// adds an object to the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="objCMI_News">The CMI_NewsInfo object</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void AddCMI_News(SqlTransaction trans, CMI_NewsInfo objCMI_News)
        {

            if (objCMI_News.CIKCode > 0)
            {
                DataProvider.Instance().AddCMI_News(trans, objCMI_News);
            }

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// saves an object to the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="objCMI_News">The CMI_NewsInfo object</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void UpdateCMI_News(CMI_NewsInfo objCMI_News)
        {

            if (objCMI_News.CIKCode>0)
            {
                DataProvider.Instance().UpdateCMI_News(objCMI_News);
            }

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// deletes an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="NewsId">The Id of the item</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void DeleteCMI_News(int NewsId)
        {

            DataProvider.Instance().DeleteCMI_News(NewsId);
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// deletes an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="NumLastest">The Id of the item</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void PushLatest(int NumLastest)
        {

            DataProvider.Instance().PushLatest(NumLastest);
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// deletes an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="UserID">The Id of the item</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void UserPull(int UserID)
        {

            DataProvider.Instance().UserPull(UserID);
        }
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// deletes an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="UserID">The Id of the item</param> 
        /// <param name="CIKCode">The Id of the item</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void UserFollowCompany(int UserID, long CIKCode)
        {

            DataProvider.Instance().UserFollowCompany(UserID, CIKCode);
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// deletes an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="UserID">The Id of the item</param> 
        /// <param name="CIKCode">The Id of the item</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void UserUnFollowCompany(int UserID, long CIKCode)
        {

            DataProvider.Instance().UserUnFollowCompany(UserID, CIKCode);
        }
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// deletes an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="UserID">The Id of the item</param> 
        /// <param name="MajorIndustryGroupID">The Id of the item</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void UserFollowMajorIndustryGroup(int UserID, int MajorIndustryGroupID)
        {

            DataProvider.Instance().UserFollowMajorIndustryGroup(UserID, MajorIndustryGroupID);
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// deletes an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="UserID">The Id of the item</param> 
        /// <param name="MajorIndustryGroupID">The Id of the item</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void UserUnFollowMajorIndustryGroup(int UserID, int MajorIndustryGroupID)
        {

            DataProvider.Instance().UserUnFollowMajorIndustryGroup(UserID, MajorIndustryGroupID);
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// deletes an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="UserID">The Id of the item</param> 
        /// <param name="NewsID">The Id of the item</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void UserRead(int UserID, int NewsID)
        {

            DataProvider.Instance().UserRead(UserID, NewsID);
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// deletes an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="UserID">The Id of the item</param> 
        /// <param name="NewsID">The Id of the item</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void UserHide(int UserID, int NewsID)
        {

            DataProvider.Instance().UserHide(UserID, NewsID);
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// deletes an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="UserID">The Id of the item</param> 
        /// <param name="NewsID">The Id of the item</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void UserUnRead(int UserID, int NewsID)
        {

            DataProvider.Instance().UserUnRead(UserID, NewsID);
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// deletes an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="UserID">The Id of the item</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable GetFollowedCompanys(int UserID)
        {

            return DataProvider.Instance().GetFollowedCompanys(UserID);
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// deletes an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="UserID">The Id of the item</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable GetFollowedMajorIndustryGroups(int UserID)
        {

            return DataProvider.Instance().GetFollowedMajorIndustryGroups(UserID);
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// deletes an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="UserID">The Id of the item</param> 
        /// <param name="CIKCode">The Id of the item</param>
        /// <param name="FillingType">The Id of the item</param>
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable GetFollowedCompanyNewss(int UserID, long CIKCode, string FillingType)
        {

            return DataProvider.Instance().GetFollowedCompanyNewss(UserID, CIKCode, FillingType);
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// deletes an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="UserID">The Id of the item</param> 
        /// <param name="SICCode">The Id of the item</param>
        /// <param name="FillingType">The Id of the item</param>
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable GetFollowedMajorIndustryGroupNewss(int UserID, int SICCode, string FillingType)
        {

            return DataProvider.Instance().GetFollowedMajorIndustryGroupNewss(UserID, SICCode, FillingType);
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// gets an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable GetCMI_Companies()
        {
            SqlDataProvider sqlDataProvider = new SqlDataProvider();
            return SqlHelper.ExecuteDataset(sqlDataProvider.ConnectionString, "CMI_NewsCompany_sGet").Tables[0];

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// gets an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable GetCMI_CompaniesWitNum()
        {
            SqlDataProvider sqlDataProvider = new SqlDataProvider();
            return SqlHelper.ExecuteDataset(sqlDataProvider.ConnectionString, "CMI_NewsCompany_sGetWithNum").Tables[0];

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// gets an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable GetCMI_CompaniesSortByName()
        {
            SqlDataProvider sqlDataProvider = new SqlDataProvider();
            return SqlHelper.ExecuteDataset(sqlDataProvider.ConnectionString, "CMI_NewsCompany_sGetSortByName").Tables[0];

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// gets an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="CIKCode">The Id of the item</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable GetCMI_Company(string CIKCode)
        {

            SqlDataProvider sqlDataProvider = new SqlDataProvider();
            return SqlHelper.ExecuteDataset(sqlDataProvider.ConnectionString, "CMI_NewsCompany_Get", CIKCode).Tables[0];

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// saves an object to the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="objCMI_News">The CMI_NewsInfo object</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void UpdateCMI_Company(string CIKCode, string CompanyName)
        {

            if (CIKCode.Trim() != "")
            {
                SqlDataProvider sqlDataProvider = new SqlDataProvider();
                SqlHelper.ExecuteScalar(sqlDataProvider.ConnectionString, "CMI_NewsCompany_Update", CIKCode, CompanyName);
            }

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// deletes an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="NewsId">The Id of the item</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void DeleteCMICompany(string CIKCode)
        {

            if (CIKCode.Trim() != "")
            {
                SqlDataProvider sqlDataProvider = new SqlDataProvider();
                SqlHelper.ExecuteScalar(sqlDataProvider.ConnectionString, "CMI_NewsCompany_Delete", CIKCode);
            }

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// gets an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable GetCMI_IndustriesByGroup(int MajorIndustryGroupID)
        {
            SqlDataProvider sqlDataProvider = new SqlDataProvider();
            return SqlHelper.ExecuteDataset(sqlDataProvider.ConnectionString, "CMI_NewsIndustry_sGetByGroup", MajorIndustryGroupID).Tables[0];

        }
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// gets an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable GetCMI_Industries()
        {
            SqlDataProvider sqlDataProvider = new SqlDataProvider();
            return SqlHelper.ExecuteDataset(sqlDataProvider.ConnectionString, "CMI_NewsIndustry_sGet").Tables[0];

        }
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// gets an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="CIKCode">The Id of the item</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable GetCMI_Industry(int SICCode)
        {

            SqlDataProvider sqlDataProvider = new SqlDataProvider();
            return SqlHelper.ExecuteDataset(sqlDataProvider.ConnectionString, "CMI_NewsIndustry_Get", SICCode).Tables[0];

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// adds an object to the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="objCMI_News">The CMI_NewsInfo object</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void AddCMI_Industry(int SICCode, int MajorIndustryGroupID, string Industry)
        {

            if (SICCode > 0)
            {
                SqlDataProvider sqlDataProvider = new SqlDataProvider();
                SqlHelper.ExecuteScalar(sqlDataProvider.ConnectionString, "CMI_NewsIndustry_Add", MajorIndustryGroupID, Industry, SICCode);
            }

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// saves an object to the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="objCMI_News">The CMI_NewsInfo object</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void UpdateCMI_Industry(int SICCode, int MajorIndustryGroupID, string Industry)
        {

            if (SICCode>0)
            {
                SqlDataProvider sqlDataProvider = new SqlDataProvider();
                SqlHelper.ExecuteScalar(sqlDataProvider.ConnectionString, "CMI_NewsIndustry_Update", MajorIndustryGroupID, Industry, SICCode);
            }

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// deletes an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="SICCode">The Id of the item</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void DeleteCMIIndustry(int SICCode)
        {

            if (SICCode > 0)
            {
                SqlDataProvider sqlDataProvider = new SqlDataProvider();
                SqlHelper.ExecuteScalar(sqlDataProvider.ConnectionString, "CMI_NewsIndustry_Delete", SICCode);
            }

        }




        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// gets an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable GetCMI_Industries2DigitByGroup(int MajorIndustryGroupID)
        {
            SqlDataProvider sqlDataProvider = new SqlDataProvider();
            return SqlHelper.ExecuteDataset(sqlDataProvider.ConnectionString, "CMI_News2DigitIndustry_sGetByGroup", MajorIndustryGroupID).Tables[0];

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// gets an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="CIKCode">The Id of the item</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable GetCMI_Industry2Digit(int SIC2Digit)
        {

            SqlDataProvider sqlDataProvider = new SqlDataProvider();
            return SqlHelper.ExecuteDataset(sqlDataProvider.ConnectionString, "CMI_News2DigitIndustry_Get", SIC2Digit).Tables[0];

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// adds an object to the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="objCMI_News">The CMI_NewsInfo object</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void AddCMI_2DigitIndustry(int SIC2Digit, int MajorIndustryGroupID, string Industry2Digit)
        {

            if (SIC2Digit > 0)
            {
                SqlDataProvider sqlDataProvider = new SqlDataProvider();
                SqlHelper.ExecuteScalar(sqlDataProvider.ConnectionString, "CMI_News2DigitIndustry_Add", MajorIndustryGroupID, Industry2Digit, SIC2Digit);
            }

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// saves an object to the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="objCMI_News">The CMI_NewsInfo object</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void UpdateCMI_2DigitIndustry(int SIC2Digit, int MajorIndustryGroupID, string Industry2Digit)
        {

            if (SIC2Digit > 0)
            {
                SqlDataProvider sqlDataProvider = new SqlDataProvider();
                SqlHelper.ExecuteScalar(sqlDataProvider.ConnectionString, "CMI_News2DigitIndustry_Update", MajorIndustryGroupID, Industry2Digit, SIC2Digit);
            }

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// deletes an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="SICCode">The Id of the item</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void DeleteCMI2DigitIndustry(int SIC2Digit)
        {

            if (SIC2Digit > 0)
            {
                SqlDataProvider sqlDataProvider = new SqlDataProvider();
                SqlHelper.ExecuteScalar(sqlDataProvider.ConnectionString, "CMI_News2DigitIndustry_Delete", SIC2Digit);
            }

        }
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// gets an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable GetCMI_MajorIndustryGroups()
        {
            SqlDataProvider sqlDataProvider = new SqlDataProvider();
            return SqlHelper.ExecuteDataset(sqlDataProvider.ConnectionString, "CMI_NewsMajorIndustryGroup_sGet").Tables[0];

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// gets an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="CIKCode">The Id of the item</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable GetCMI_MajorIndustryGroup(int MajorIndustryGroupID)
        {

            SqlDataProvider sqlDataProvider = new SqlDataProvider();
            return SqlHelper.ExecuteDataset(sqlDataProvider.ConnectionString, "CMI_NewsMajorIndustryGroup_Get", MajorIndustryGroupID).Tables[0];

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// adds an object to the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="objCMI_News">The CMI_NewsInfo object</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void AddCMI_MajorIndustryGroup(string MajorIndustryGroup, string BannerImage)
        {

            if (MajorIndustryGroup.Length > 0)
            {
                SqlDataProvider sqlDataProvider = new SqlDataProvider();
                SqlHelper.ExecuteScalar(sqlDataProvider.ConnectionString, "CMI_NewsIndustry_Add", MajorIndustryGroup, BannerImage);
            }

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// saves an object to the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="objCMI_News">The CMI_NewsInfo object</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void UpdateCMI_MajorIndustryGroup(int MajorIndustryGroupId, string MajorIndustryGroup, string BannerImage)
        {

            if (MajorIndustryGroupId > 0)
            {
                SqlDataProvider sqlDataProvider = new SqlDataProvider();
                SqlHelper.ExecuteScalar(sqlDataProvider.ConnectionString, "CMI_NewsMajorIndustryGroup_Update", MajorIndustryGroupId, MajorIndustryGroup,BannerImage);
            }

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// deletes an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="MajorIndustryGroupId">The Id of the item</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void DeleteCMIMajorIndustryGroup(int MajorIndustryGroupId)
        {

            if (MajorIndustryGroupId > 0)
            {
                SqlDataProvider sqlDataProvider = new SqlDataProvider();
                SqlHelper.ExecuteScalar(sqlDataProvider.ConnectionString, "CMI_NewsMajorIndustryGroup_Delete", MajorIndustryGroupId);
            }

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// IsPublished 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public bool IsPublished()
        {
            SqlDataProvider sqlDataProvider = new SqlDataProvider();
            
            if (Convert.ToBoolean(SqlHelper.ExecuteScalar(sqlDataProvider.ConnectionString,CommandType.Text,"select dbo.CMI_IsPublished()").ToString()))
                return true;
            else
                return false;
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// IsPublished 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void DoPublished()
        {
            SqlDataProvider sqlDataProvider = new SqlDataProvider();
            if (IsPublished())
                SqlHelper.ExecuteNonQuery(sqlDataProvider.ConnectionString, "CMI_UnDoPublish");
            else
                SqlHelper.ExecuteNonQuery(sqlDataProvider.ConnectionString, "CMI_DoPublish");
     
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// gets an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="CIKCode">The Id of the item</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable GetCMI_NewsPreferenceCompany(int UserID)
        {

            SqlDataProvider sqlDataProvider = new SqlDataProvider();
            return SqlHelper.ExecuteDataset(sqlDataProvider.ConnectionString, "CMI_NewsPreferenceCompany_sGet", UserID).Tables[0];

        }
                /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// gets an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="CIKCode">The Id of the item</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DataTable GetCMI_NewsPreferenceMajorIndustryGroup(int UserID)
        {

            SqlDataProvider sqlDataProvider = new SqlDataProvider();
            return SqlHelper.ExecuteDataset(sqlDataProvider.ConnectionString, "CMI_NewsPreferenceMajorIndustryGroup_sGet", UserID).Tables[0];

        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// GetNum_NewsUserDownload 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public int GetNum_NewsUserDownload(int UserID)
        {
            SqlDataProvider sqlDataProvider = new SqlDataProvider();

            return Convert.ToInt32(SqlHelper.ExecuteScalar(sqlDataProvider.ConnectionString, CommandType.Text, "select dbo.CMI_NewsUserDownload_GetNum(" + UserID.ToString() + ")").ToString());
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// deletes an object from the database 
        /// </summary> 
        /// <remarks> 
        /// </remarks> 
        /// <param name="UserId">The Id of the item</param> 
        /// <history> 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public void Add_NewsUserDownload(int UserId, int RecordDownloaded)
        {

            SqlDataProvider sqlDataProvider = new SqlDataProvider();
            SqlHelper.ExecuteScalar(sqlDataProvider.ConnectionString, "CMI_NewsUserDownload_Add", UserId, RecordDownloaded);

        }
        #endregion
    }


}