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
using DotNetNuke;
using System.Data.SqlClient;

namespace CMI_News
{

    /// ----------------------------------------------------------------------------- 
    /// <summary> 
    /// An abstract class for the data access layer 
    /// </summary> 
    /// <remarks> 
    /// </remarks> 
    /// <history> 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public abstract class DataProvider
    {

        #region "Shared/Static Methods"

        /// <summary>
        /// singleton reference to the instantiated object 
        /// </summary>
        private static DataProvider objProvider = null;

        /// <summary>
        /// constructor
        /// </summary>
        static DataProvider()
        {
            CreateProvider();
        }

        /// <summary>
        /// dynamically create provider 
        /// </summary>
        private static void CreateProvider()
        {
            objProvider = (DataProvider)DotNetNuke.Framework.Reflection.CreateObject("data", "CMI_News", "");
        }

        /// <summary>
        /// return the provider 
        /// </summary>
        /// <returns></returns>
        public static DataProvider Instance()
        {
            return objProvider;
        }

        #endregion

        #region "Abstract methods"

        public abstract IDataReader GetCMI_Newss();
        public abstract IDataReader GetCMI_NewssTop3();
        public abstract IDataReader GetCMI_NewssPrev();
        public abstract IDataReader GetCMI_NewssLatest();
        public abstract IDataReader GetCMI_NewssSearch(int Super, int UserId, string FilingDateFrom, string FilingDateTo, string MajorIndustryGroupIDs, string CIKCodes);
        public abstract int GetCMI_NewssSearchNum(string FilingDateFrom, string FilingDateTo, string MajorIndustryGroupIDs, string CIKCodes);
        public abstract IDataReader GetCMI_News(int NewsId);
        public abstract void AddCMI_News(CMI_NewsInfo objCMI_News);
        public abstract void AddCMI_News(SqlTransaction trans, CMI_NewsInfo objCMI_News);
        public abstract void UpdateCMI_News(CMI_NewsInfo objCMI_News);
        public abstract void DeleteCMI_News(int NewsId);


        public abstract void PushLatest(int NumLastest);
        public abstract void UserPull(int UserID);
        public abstract void UserFollowCompany(int UserID, long CIKCode);
        public abstract void UserUnFollowCompany(int UserID, long CIKCode);
        public abstract void UserFollowMajorIndustryGroup(int UserID, int MajorIndustryGroupID);
        public abstract void UserUnFollowMajorIndustryGroup(int UserID, int MajorIndustryGroupID);

        public abstract void UserRead(int UserID, int NewsID);
        public abstract void UserHide(int UserID, int NewsID);
        public abstract void UserUnRead(int UserID, int NewsID);

        public abstract DataTable GetFollowedCompanys(int UserID);
        public abstract DataTable GetFollowedMajorIndustryGroups(int UserID);

        public abstract DataTable GetFollowedCompanyNewss(int UserID, long CIKCode, string FillingType);
        public abstract DataTable GetFollowedMajorIndustryGroupNewss(int UserID, int SICCode, string FillingType);
        #endregion

    }
}