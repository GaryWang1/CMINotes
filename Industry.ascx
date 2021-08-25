<%@ Control Language="C#" Inherits="CMI_News.Industry"
    AutoEventWireup="true" CodeBehind="Industry.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<h1>Industry Admin: <asp:Literal runat="server" ID="ltlIndustry"></asp:Literal></h1>
<Telerik:RadGrid  runat="server" ID="rgrList" EnableEmbeddedBaseStylesheet="false"
  AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover"  EnableEmbeddedSkins="false" OnUpdateCommand="rgrList_OnUpdateCommand" OnInsertCommand="rgrList_OnInsertCommand" 
  OnNeedDataSource="rgrList_NeedDataSource" AllowAutomaticInserts ="false"
   OnDeleteCommand="rgrList_OnDeleteCommand" OnItemDataBound="rgrList_ItemDataBound">
    <MasterTableView CommandItemDisplay="TopAndBottom" CssClass="table table-striped table-hover" DataKeyNames="SICCode" EditMode="InPlace" InsertItemDisplay="Bottom">
        <CommandItemSettings AddNewRecordText="New Industry"  ShowAddNewRecordButton="true" ShowRefreshButton="false" AddNewRecordImageUrl="~/images/add.gif"/>
        <Columns>
            <Telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" HeaderText="Edit" CancelImageUrl="~/images/cancel.gif" UpdateImageUrl="~/images/save.gif" EditImageUrl="~/images/edit.gif"> 
            </Telerik:GridEditCommandColumn>

            <Telerik:GridBoundColumn UniqueName="SICCode" DataField="SICCode" HeaderText="SIC Code"></Telerik:GridBoundColumn>
            <Telerik:GridBoundColumn UniqueName="Industry" DataField="Industry"  HeaderText="Industry"></Telerik:GridBoundColumn>

            <Telerik:GridButtonColumn ButtonType="ImageButton" UniqueName="DeleteCommandColumn" HeaderText="Delete" CommandName="Delete" Text="Delete" Visible="false"
            ImageUrl="~/images/delete.gif"  ConfirmDialogType="Classic" ConfirmText="Are you sure you want to permanently delete this Industry?" ConfirmTitle="Delete Industry"></Telerik:GridButtonColumn>
        </Columns>
    </MasterTableView>
<ClientSettings AllowRowsDragDrop="true">
   <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" />
</ClientSettings>
</Telerik:RadGrid>

<asp:Label runat="server" CssClass="alert alert-danger" ID="lblErr" Visible="false"></asp:Label><br />
<a href='<%=EditUrl("IndustryGroup")%>' class="btn btn-info" >Major Industry List</a>

<a href='<%=DotNetNuke.Common.Globals.NavigateURL()%>' class="btn btn-info" >Main View</a>