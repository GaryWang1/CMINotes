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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

using DotNetNuke.Common.Utilities;
using DotNetNuke.Framework.Providers;

namespace CMI_News
{

    /// ----------------------------------------------------------------------------- 
    /// <summary> 
    /// SQL Server implementation of the abstract DataProvider class 
    /// </summary> 
    /// <remarks> 
    /// </remarks> 
    /// <history> 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public class SqlDataProvider : DataProvider
    {


        #region "Private Members"

        private const string ProviderType = "data";
        private const string ModuleQualifier = "CMI_News_";

        private ProviderConfiguration _providerConfiguration = ProviderConfiguration.GetProviderConfiguration(ProviderType);
        private string _connectionString;
        private string _providerPath;
        private string _objectQualifier;
        private string _databaseOwner;

        #endregion

        #region "Constructors"

        public SqlDataProvider()
        {

            // Read the configuration specific information for this provider 
            Provider objProvider = (Provider)_providerConfiguration.Providers[_providerConfiguration.DefaultProvider];

            // Read the attributes for this provider 

            //Get Connection string from web.config 
            _connectionString = Config.GetConnectionString();

            if (_connectionString == "")
            {
                // Use connection string specified in provider 
                _connectionString = objProvider.Attributes["connectionString"];
            }

            _providerPath = objProvider.Attributes["providerPath"];

            _objectQualifier = objProvider.Attributes["objectQualifier"];
            if (_objectQualifier != "" & _objectQualifier.EndsWith("_") == false)
            {
                _objectQualifier += "_";
            }

            _databaseOwner = objProvider.Attributes["databaseOwner"];
            if (_databaseOwner != "" & _databaseOwner.EndsWith(".") == false)
            {
                _databaseOwner += ".";
            }

        }

        #endregion

        #region "Properties"

        public string ConnectionString
        {
            get { return _connectionString; }
        }

        public string ProviderPath
        {
            get { return _providerPath; }
        }

        public string ObjectQualifier
        {
            get { return _objectQualifier; }
        }

        public string DatabaseOwner
        {
            get { return _databaseOwner; }
        }

        #endregion

        #region "Private Methods"

        private string GetFullyQualifiedName(string name)
        {
            return DatabaseOwner + ObjectQualifier + ModuleQualifier + name;
        }

        private object GetNull(object Field)
        {
            return DotNetNuke.Common.Utilities.Null.GetNull(Field, DBNull.Value);
        }

        #endregion

        #region "Public Methods"

        public override IDataReader GetCMI_Newss()
        {
            return (IDataReader)SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("sGet"));
        }

        public override IDataReader GetCMI_NewssTop3()
        {
            return (IDataReader)SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("sGetTop3"));
        }

        public override IDataReader GetCMI_NewssPrev()
        {
            return (IDataReader)SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("sGetPrev"));
        }

        public override IDataReader GetCMI_NewssLatest()
        {
            return (IDataReader)SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("sGetLatest"));
        }

        public override IDataReader GetCMI_NewssSearch(int Super, int UserId, string FilingDateFrom, string FilingDateTo, string MajorIndustryGroupIDs, string CIKCodes)
        {
            return (IDataReader)SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("sGetSearch"), Super, UserId, FilingDateFrom, FilingDateTo, MajorIndustryGroupIDs, CIKCodes);
        }

        public override int GetCMI_NewssSearchNum(string FilingDateFrom, string FilingDateTo, string MajorIndustryGroupIDs, string CIKCodes)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("sGetSearchNum"), FilingDateFrom, FilingDateTo, MajorIndustryGroupIDs, CIKCodes);
        }

        public override IDataReader GetCMI_News(int NewsId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Get"), NewsId);
        }

        public override void AddCMI_News(CMI_NewsInfo objCMI_News)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Add"), objCMI_News.AcceptanceDate,
                objCMI_News.FilingDate, objCMI_News.CIKCode, objCMI_News.SICCode, objCMI_News.IsLoan,
                objCMI_News.IsBond, objCMI_News.IsStock, objCMI_News.NewMoneyLoan, objCMI_News.NewMoneyBondNotes,
                objCMI_News.ProposedLoan, objCMI_News.ProposedBondNotes, objCMI_News.Closed,
                objCMI_News.ActualCurrency, objCMI_News.k8FormDescription, objCMI_News.DocumentIncluded,
                objCMI_News.FilingHyperlink, objCMI_News.HeadlineDescription,
                objCMI_News.K8FormHyperlink, objCMI_News.Featured, objCMI_News.CompanyName,
                objCMI_News.Industry, objCMI_News.ImageID, objCMI_News.MajorIndustryGroupId,
                objCMI_News.FilingForm, objCMI_News.MajorIndustryGroup);
        }
        public override void AddCMI_News(SqlTransaction trans, CMI_NewsInfo objCMI_News)
        {
            SqlHelper.ExecuteNonQuery(trans, GetFullyQualifiedName("Add"), objCMI_News.AcceptanceDate,
                objCMI_News.FilingDate, objCMI_News.CIKCode, objCMI_News.SICCode, objCMI_News.IsLoan,
                objCMI_News.IsBond, objCMI_News.IsStock, objCMI_News.NewMoneyLoan, objCMI_News.NewMoneyBondNotes,
                objCMI_News.ProposedLoan, objCMI_News.ProposedBondNotes, objCMI_News.Closed,
                objCMI_News.ActualCurrency, objCMI_News.k8FormDescription, objCMI_News.DocumentIncluded,
                objCMI_News.FilingHyperlink, objCMI_News.HeadlineDescription,
                objCMI_News.K8FormHyperlink, objCMI_News.Featured, objCMI_News.CompanyName,
                objCMI_News.Industry, objCMI_News.ImageID, objCMI_News.MajorIndustryGroupId,
                objCMI_News.FilingForm, objCMI_News.MajorIndustryGroup);
        }

        public override void UpdateCMI_News(CMI_NewsInfo objCMI_News)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Update"), objCMI_News.NewsID, objCMI_News.AcceptanceDate, objCMI_News.FilingDate,
                objCMI_News.CIKCode, objCMI_News.SICCode, objCMI_News.IsLoan, objCMI_News.IsBond, objCMI_News.IsStock, objCMI_News.NewMoneyLoan,
                objCMI_News.NewMoneyBondNotes, objCMI_News.ProposedLoan, objCMI_News.ProposedBondNotes, objCMI_News.Closed, objCMI_News.ActualCurrency,
                objCMI_News.k8FormDescription, objCMI_News.DocumentIncluded, objCMI_News.FilingHyperlink, objCMI_News.HeadlineDescription,
                objCMI_News.K8FormHyperlink, objCMI_News.Featured, objCMI_News.CompanyName, objCMI_News.ImageID,
                objCMI_News.FilingForm);
        }

        public override void DeleteCMI_News(int NewsId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Delete"), NewsId);
        }

        public override void PushLatest(int NumLastest)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("PushLatest"), NumLastest);
        }

        public override void UserPull(int UserID)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("UserPull"), UserID);
        }
        public override void UserFollowCompany(int UserID, long CIKCode)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("UserFollowCompany"), UserID, CIKCode);
        }

        public override void UserUnFollowCompany(int UserID, long CIKCode)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("UserUnFollowCompany"), UserID, CIKCode);

        }

        public override void UserFollowMajorIndustryGroup(int UserID, int MajorIndustryGroupID)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("UserFollowMajorIndustryGroup"), UserID, MajorIndustryGroupID);
        }

        public override void UserUnFollowMajorIndustryGroup(int UserID, int MajorIndustryGroupID)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("UserUnFollowMajorIndustryGroup"), UserID, MajorIndustryGroupID);
        }

        public override void UserRead(int UserID, int NewsID)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("UserRead"), UserID, NewsID);
        }
        public override void UserHide(int UserID, int NewsID)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("UserHide"), UserID, NewsID);
        }
        public override void UserUnRead(int UserID, int NewsID)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("UserUnRead"), UserID, NewsID);
        }


        public override DataTable GetFollowedCompanys(int UserID)
        {
            return SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("sGetFollowedCompany"), UserID).Tables[0];
        }

        public override DataTable GetFollowedMajorIndustryGroups(int UserID)
        {
            return SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("sGetFollowedMajorIndustryGroup"), UserID).Tables[0];
        }

        public override DataTable GetFollowedCompanyNewss(int UserID, long CIKCode, string FillingType)
        {
            return SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("sGetFollowedCompanyNews"), UserID, CIKCode, FillingType).Tables[0];
        }

        public override DataTable GetFollowedMajorIndustryGroupNewss(int UserID, int SICCode, string FillingType)
        {
            return SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("sGetFollowedMajorIndustryGroupNews"), UserID, SICCode, FillingType).Tables[0];
        }
        #endregion


    }
}