<%@ Control Language="C#" Inherits="CMI_News.IndustryGroup"
    AutoEventWireup="true" CodeBehind="IndustryGroup.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<h1>Major Industry Group Admin</h1>
<Telerik:RadGrid  runat="server" ID="rgrList" EnableEmbeddedBaseStylesheet="false"
  AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" EnableEmbeddedSkins="false" OnUpdateCommand="rgrList_OnUpdateCommand" OnInsertCommand="rgrList_OnInsertCommand" 
  OnNeedDataSource="rgrList_NeedDataSource" AllowAutomaticInserts ="false" OnItemCommand ="rgrList_ItemCommand"
   OnDeleteCommand="rgrList_OnDeleteCommand" OnItemDataBound="rgrList_ItemDataBound">
    <MasterTableView CommandItemDisplay="TopAndBottom"  CssClass="table table-striped table-hover" DataKeyNames="MajorIndustryGroupID" EditMode="EditForms" InsertItemDisplay="Bottom">
        <CommandItemSettings AddNewRecordText="New Major Industry Group"  ShowAddNewRecordButton="true" ShowRefreshButton="false" AddNewRecordImageUrl="~/images/add.gif"/>
        <Columns>
            <Telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" HeaderText="Edit" CancelImageUrl="~/images/cancel.gif" UpdateImageUrl="~/images/save.gif" EditImageUrl="~/images/edit.gif"> 
            </Telerik:GridEditCommandColumn>

            <Telerik:GridBoundColumn UniqueName="MajorIndustryGroupID" DataField="MajorIndustryGroupID" ReadOnly="true" HeaderText="MajorIndustryGroupID"></Telerik:GridBoundColumn>
            <Telerik:GridBoundColumn UniqueName="MajorIndustryGroup" DataField="MajorIndustryGroup"  HeaderText="Major Industry Group"></Telerik:GridBoundColumn>

<telerik:GridButtonColumn  HeaderText="2-Digit Industry List" Text="2-Digit Industry List" UniqueName="D2IndustryList" CommandName="D2IndustryList">          
</telerik:GridButtonColumn> 
<telerik:GridButtonColumn  HeaderText="Industry List" Text="Industry List" UniqueName="IndustryList" CommandName="IndustryList">          
</telerik:GridButtonColumn> 

            <Telerik:GridButtonColumn ButtonType="ImageButton" UniqueName="DeleteCommandColumn" HeaderText="Delete" CommandName="Delete" Text="Delete" Visible="false"
            ImageUrl="~/images/delete.gif"  ConfirmDialogType="Classic" ConfirmText="Are you sure you want to permanently delete this Major Industry Group?" ConfirmTitle="Delete Major Industry Group"></Telerik:GridButtonColumn>
        </Columns>
        <EditFormSettings UserControlName="DesktopModules/CMI_News/EditMajorIndustryGroup.ascx" EditFormType="WebUserControl">
            <EditColumn UniqueName="EditCommandColumn1">
            </EditColumn>
        </EditFormSettings> 
    </MasterTableView>
<ClientSettings AllowRowsDragDrop="true">
   <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" />
</ClientSettings>
</Telerik:RadGrid>

<asp:Label runat="server" CssClass="alert alert-danger" ID="lblErr" Visible="false"></asp:Label><br />
<a href='<%=DotNetNuke.Common.Globals.NavigateURL()%>' class="btn btn-info" >Main View</a>