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

namespace CMI_News
{

    /// ----------------------------------------------------------------------------- 
    /// <summary> 
    /// The Info class for CMI_News 
    /// </summary> 
    /// <remarks> 
    /// </remarks> 
    /// <history> 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public class CMI_NewsInfo
    {
        // initialization 
        public CMI_NewsInfo()
        {
        }
        // public properties 
        public int NewsID { get; set; }

        public DateTime AcceptanceDate { get; set; }
        public DateTime FilingDate { get; set; }

        public long CIKCode { get; set; }
        public string CompanyName { get; set; }
        public int SICCode { get; set; }
        public string Industry { get; set; }
        public int MajorIndustryGroupId { get; set; }
        public string MajorIndustryGroup { get; set; }
        public string BannerImage { get; set; }

        public bool IsLoan { get; set; }
        public bool IsBond { get; set; }
        public bool IsStock { get; set; }

        public decimal NewMoneyLoan { get; set; }
        public decimal NewMoneyBondNotes { get; set; }
        public bool Closed { get; set; }

        public string k8FormDescription { get; set; }

        public string ActualCurrency { get; set; }

        public bool DocumentIncluded { get; set; }

        public string FilingHyperlink { get; set; }

        public string HeadlineDescription { get; set; }

        public string K8FormHyperlink { get; set; }

        public int Featured { get; set; }

        public decimal ProposedLoan { get; set; }
        public decimal ProposedBondNotes { get; set; }

        public long ImageID { get; set; }

        public string FilingForm { get; set; }
    }

}