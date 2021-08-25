<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsAdmin.ascx.cs" Inherits="CMI_News.NewsAdmin" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<Telerik:RadGrid  runat="server" ID="rgrList" ItemStyle-Font-Size="Large" MasterTableView-Font-Size="Large"  
  AutoGenerateColumns="false" CssClass="listTable" EnableEmbeddedSkins="true" 
  OnNeedDataSource="rgrList_NeedDataSource"  OnItemCommand="rgrList_InsertCommand" OnDeleteCommand="rgrList_DeleteCommand">
    <MasterTableView CommandItemDisplay="Top" DataKeyNames="NewsID" >
        <CommandItemSettings AddNewRecordText="Add News" ShowRefreshButton="false" AddNewRecordImageUrl="~/images/add.gif"/>
        <Columns>
            <Telerik:GridTemplateColumn >
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# EditUrl("NewsID",DataBinder.Eval(Container.DataItem,"NewsID").ToString()) %>'
                        Visible="<%# IsEditable %>" runat="server" >
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit"
                            Visible="<%#IsEditable%>" /></asp:HyperLink>
                 </ItemTemplate>
            </Telerik:GridTemplateColumn>
            <Telerik:GridBoundColumn UniqueName="NewsID" DataField="NewsID" ReadOnly="true" HeaderText="NewsID"  Display="false"></Telerik:GridBoundColumn>
            <Telerik:GridBoundColumn UniqueName="FilingDate" DataField="FilingDate" ReadOnly="true" HeaderText="Filing Date"  Display="true"
                 DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" ></Telerik:GridBoundColumn>
            <Telerik:GridBoundColumn UniqueName="CIKCode" DataField="CIKCode"  HeaderText="CIK Code" Display="true"></Telerik:GridBoundColumn>
            <Telerik:GridBoundColumn UniqueName="CompanyName" DataField="CompanyName"  HeaderText="Company Name" Display="true"></Telerik:GridBoundColumn>
            <Telerik:GridTemplateColumn UniqueName="HeadlineDescription"  HeaderText="Headline Description">
                <ItemTemplate>
                    <%#CMI_News.NewsCommon.stripHTML(Eval("HeadlineDescription").ToString(),50)%>
                 </ItemTemplate>
            </Telerik:GridTemplateColumn>            
            <Telerik:GridBoundColumn UniqueName="Industry" DataField="Industry" ReadOnly="true" HeaderText="Industry"  Display="true"></Telerik:GridBoundColumn>
            <Telerik:GridBoundColumn UniqueName="SICCode" DataField="SICCode"  HeaderText="SICCode" Display="true"></Telerik:GridBoundColumn>
            <Telerik:GridBoundColumn UniqueName="MajorIndustryGroup" DataField="MajorIndustryGroup"  HeaderText="Major Industry Group" Display="false"></Telerik:GridBoundColumn>

            <Telerik:GridButtonColumn ButtonType="ImageButton" UniqueName="DeleteColumn" HeaderText="Delete" CommandName="Delete" Text="Delete" Display="true"
            ImageUrl="~/images/delete.gif"  ConfirmDialogType="Classic" ConfirmText="Are you sure you want to permanently delete this News?" ConfirmTitle="Delete News"></Telerik:GridButtonColumn>
        </Columns>
    </MasterTableView>
</Telerik:RadGrid>

<a href='<%=DotNetNuke.Common.Globals.NavigateURL()%>' class="btn btn-info" >Main View</a>