<%@ Control Language="C#" Inherits="CMI_News.ViewCMI_News"
    AutoEventWireup="true" CodeBehind="ViewCMI_News.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<script src="/DesktopModules/CMI_News/scripts/popup.js"></script>
<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
<div class="moduleborder">
    <asp:Panel runat="server" ID="pnlNewsSearch">
        <div class="row">
        <div class="heading1">
            Advanced Search
        </div>
          <link rel="stylesheet" type="text/css" href="/DesktopModules/CMI_News/css/jquery.dropdown.css">
          <script src="/DesktopModules/CMI_News/scripts/jquery.dropdown.js"></script>
        <script>
            function altFind(arr, callback) {
                for (var i = 0; i < arr.length; i++) {
                    var match = callback(arr[i]);
                    if (match) {
                        return arr[i];
                        break;
                    }
                }
            }

            $(function () {
                var currentDate = new Date();
                $(".datepicker").datepicker({
                    dateFormat: "mm-dd-yy",
                    numberOfMonths: 1
                });
            });

          </script>
        <style>
            .CMILabel {
                float: left;
                width: 180px;
                font-weight:bold;
                color:#000;
                padding-left:8px;
                padding-bottom:8px;

            }
            .CMIText {
                padding-bottom:8px;
            }

            .ui-dialog .ui-dialog-titlebar-close {
            position: absolute;
            right: .30em !important;
            top: 20% !important;
        }
            .hasDatepicker {
            position: relative;
            z-index: 9;
        }
            /*.ui-dialog-titlebar-close {
            visibility: hidden;
        }*/
        .dropdown-display-label:after, .dropdown-display:after {
            display: none;
        }

        .moduleborder{
          border-style: solid;
          border-width: thin;
            border-color:darkgray;
            padding-left:20px;
            padding-right:20px;
            padding-top:20px;
        }
        </style>
        <asp:Panel runat="server" ID="PnlSearch">
        <div class="CMILabel">
        Filing Date:       </div>      <div class="CMIText">&nbsp;From&nbsp;&nbsp;&nbsp; 
        <asp:TextBox runat="server" ID="txtStartDate" CssClass="datepicker" ClientIDMode="Static" Width="77px"></asp:TextBox>&nbsp;&nbsp;&nbsp;To&nbsp;&nbsp;&nbsp;
			        <asp:TextBox runat="server" ID="txtEndDate" CssClass="datepicker" ClientIDMode="Static" Width="77px"></asp:TextBox>
              </div>
        <asp:HiddenField runat="server" ID="hidCIKs" ClientIDMode="Static" />
        <div class="CMILabel CMIText">Companies:       </div>     
        <div class="dropdown-mul-1">
                <select style="display:none"  name="" id="" multiple placeholder="Select"> </select>
        </div> 


          <script>


              $('.dropdown-mul-1').dropdown({
                  data: ciks,
                  limitCount: 40,
                  multipleMode: 'label',
                  choice: function (event, ui) {
                      //alert(ui.name + ui.id);
                      $('.dropdown-mul-1').find('.dropdown-search').find('input').val('');
                  }
              });
              function updateHidCIKs() {
                  var hidCIKs = "";
                  $('.dropdown-mul-1 i.del').each(function (index) {
                      hidCIKs += this.dataset.id + ",";
                  });
                  if (hidCIKs.length > 0)
                      $("#hidCIKs").val(hidCIKs.substr(0, hidCIKs.length - 1));
                  else
                      $("#hidCIKs").val("");
              }

        </script>

        <div class="CMILabel">Major Industry Group:       </div>
        <div class="CMIText">
			        <asp:CheckBoxList runat="server" ID="cklMajorIndustryGroup" RepeatColumns="2" CellSpacing="12" Width="700px" ClientIDMode="Static"
                        DataTextField="MajorIndustryGroup" DataValueField="MajorIndustryGroupId"></asp:CheckBoxList>
        </div>
            <asp:Label ID="lblNoDownload" runat="server" CssClass="alert alert-danger" Visible="false"></asp:Label>
            <asp:Label runat="server" CssClass="alert alert-info" ID="lblNum" Visible="false"></asp:Label><br /><br />
            <asp:button runat="server" ID="btnSearch" Text="Search" CssClass="btn btn-primary" OnClientClick="updateHidCIKs()" 
                OnClick="btnSearch_Click" />&nbsp;&nbsp;&nbsp;
        <asp:button runat="server" ID="Button1" Text="Reset" CssClass="btn btn-info" OnClick="Button1_Click"/>&nbsp;&nbsp;&nbsp;
                <asp:button runat="server" ID="btnDownload" Text="Download Search Result to csv file" CssClass="btn btn-primary" OnClick="btnDownload_Click" Visible="false" />
        &nbsp;&nbsp;&nbsp;<asp:button runat="server" ID="btnDownloadPl" Text="Download Search Result to csv file (|)" CssClass="btn btn-primary"  OnClick="btnDownload_Click" Visible="false" /><br />
        <Telerik:RadGrid  runat="server" ID="rgrList" EnableEmbeddedBaseStylesheet="false"
          AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" EnableEmbeddedSkins="false" OnPreRender ="rgrList_PreRender"
          OnNeedDataSource="rgrList_NeedDataSource"  OnItemCommand="rgrList_InsertCommand" OnDeleteCommand="rgrList_DeleteCommand"  OnEditCommand="rgrList_EditCommand">
            <MasterTableView CommandItemDisplay="Top" DataKeyNames="NewsID" CssClass="table table-striped table-hover">
                <CommandItemSettings  ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                <Columns>
                    <Telerik:GridBoundColumn UniqueName="FilingDate" DataField="FilingDate" ReadOnly="true" HeaderText="Filing Date"  Display="true"
                         DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" ></Telerik:GridBoundColumn>


                    <Telerik:GridBoundColumn UniqueName="CompanyName" DataField="CompanyName"  HeaderText="Company" Display="true"></Telerik:GridBoundColumn>

                    <Telerik:GridTemplateColumn UniqueName="HeadlineDescription"  HeaderText="Headline Description">
                        <ItemTemplate>
                            <%#CMI_News.NewsCommon.stripHTML(Eval("HeadlineDescription").ToString(),50)%>
                         </ItemTemplate>
                    </Telerik:GridTemplateColumn>
            
            
                    <Telerik:GridBoundColumn UniqueName="Industry" DataField="Industry" ReadOnly="true" HeaderText="Industry"  Display="true"></Telerik:GridBoundColumn>
                    <Telerik:GridBoundColumn UniqueName="SICCode" DataField="SICCode"  HeaderText="SIC Code" Display="true"></Telerik:GridBoundColumn>
                    <Telerik:GridBoundColumn UniqueName="MajorIndustryGroup" DataField="MajorIndustryGroup"  HeaderText="Major Industry Group" Display="false"></Telerik:GridBoundColumn>

                 <Telerik:GridTemplateColumn HeaderText="Filing Hyperlink" UniqueName="FilingHyperlink">
                    <ItemTemplate>
                        <a onclick="popupMax(this);return false;" href="<%#Eval("FilingHyperlink").ToString()%>">Filing Hyperlink</a>
                    </ItemTemplate>
                 </Telerik:GridTemplateColumn>       
                <Telerik:GridTemplateColumn HeaderText="8K Form Hyperlink" UniqueName="K8FormHyperlink">
                    <ItemTemplate>
                        <a onclick="popupMax(this);return false;" href="<%#Eval("K8FormHyperlink").ToString()%>">8K Form Hyperlink</a>
                    </ItemTemplate>
                 </Telerik:GridTemplateColumn>
                    <Telerik:GridButtonColumn ButtonType="ImageButton" UniqueName="EditColumn" HeaderText="Edit" CommandName="Edit" Text="Edit" Display="true"
                    ImageUrl="~/images/edit.gif"></Telerik:GridButtonColumn>
            
                    <Telerik:GridButtonColumn ButtonType="ImageButton" UniqueName="DeleteColumn" HeaderText="Delete" CommandName="Delete" Text="Delete" Display="true"
                    ImageUrl="~/images/delete.gif"  ConfirmDialogType="Classic" ConfirmText="Are you sure you want to permanently delete this News?" ConfirmTitle="Delete News"></Telerik:GridButtonColumn>
                </Columns>
            </MasterTableView>
        </Telerik:RadGrid>
</div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlDetail" Visible="false">
            <div class="CMILabel">News ID:</div>
            <div><asp:Label runat="server" ID="lblNewsID"></asp:Label></div>
        <div class="CMILabel">Filing Date:</div>
        <div><asp:Label runat="server" ID="lblFilingDate"></asp:Label></div>
        <div class="CMILabel">Acceptance Date:</div>
        <div><asp:Label runat="server" ID="lblAcceptanceDate"></asp:Label></div>
        <div class="CMILabel">CIK Code:</div>
        <div><asp:Label runat="server" ID="lblCIKCode"></asp:Label></div>
            <div class="CMILabel">Company Name:</div>
        <div><asp:Label runat="server" ID="lblCompanyName"></asp:Label></div>
            <div class="CMILabel">Headline Description:</div>
        <div><asp:Label runat="server" ID="lblHeadlineDescription"></asp:Label></div>
        <div class="CMILabel">SIC Code:</div>
        <div><asp:Label runat="server" ID="lblSICCode"></asp:Label></div>
        <div class="CMILabel">Industry:</div>
        <div><asp:Label runat="server" ID="lblIndustry"></asp:Label></div>
        <div class="CMILabel">Loan:</div>
        <div><asp:Label runat="server" ID="lblIsLoan"></asp:Label></div>
        <div class="CMILabel">Bond:</div>
        <div><asp:Label runat="server" ID="lblIsBond"></asp:Label></div>
        <div class="CMILabel">Preferred Stock:</div>
        <div><asp:Label runat="server" ID="lblIsStock"></asp:Label></div>
        <div class="CMILabel">New Money Loan:</div>
        <div><asp:Label runat="server" ID="lblNewMoneyLoan"></asp:Label></div>
            <div class="CMILabel">New Money Bond Notes:</div>
        <div><asp:Label runat="server" ID="lblNewMoneyBondNotes"></asp:Label></div>
        <div class="CMILabel">Proposed Pricing:</div>
        <div><asp:Label runat="server" ID="lblProposedPricing"></asp:Label></div>
            <div class="CMILabel">Closed:</div>
        <div><asp:Label runat="server" ID="lblClosed"></asp:Label></div>
        <div class="CMILabel">Actual Currency:</div>
        <div><asp:Label runat="server" ID="lblActualCurrency"></asp:Label></div>
        <div class="CMILabel">8k Form Description:</div>
        <div><asp:Label runat="server" ID="lblk8FormDescription"></asp:Label></div>
        <div class="CMILabel">Document Included:</div>
        <div><asp:Label runat="server" ID="lblDocumentIncluded"></asp:Label></div>
            <div class="CMILabel">Filing Hyperlink:</div>
        <div><asp:HyperLink runat="server" ID="hplFilingHyperlink" ></asp:HyperLink></div>
            <div class="CMILabel">8k Form Hyperlink:</div>
        <div><asp:HyperLink runat="server" ID="hplK8FormHyperlink"></asp:HyperLink></div>
                <div class="CMILabel">Featured:</div>
        <div><asp:Label runat="server" ID="lblFeatured"></asp:Label></div>
            <input type="button" title="back" value="Back to Search" class="btn btn-info"  onclick="window.history.back();">

        </asp:Panel>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlControls">
        <div>
            <asp:PlaceHolder runat="server" ID="phControls"></asp:PlaceHolder>    
        </div>
        <div style="clear:both;"></div>
    </asp:Panel>
    <asp:Panel runat="server" CssClass="alert alert-danger" ID="pnlError" Visible="false">
     
    Due to some technical issues, this page is currently unavailable. A message has been sent to our technical team, and we will work to get the page online as soon as possible. Thank you for your patience. 

    </asp:Panel>
</div>