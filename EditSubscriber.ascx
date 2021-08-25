<%@ Control Language="C#" Inherits="CMI_News.EditSubscriber"
    AutoEventWireup="true" CodeBehind="EditSubscriber.ascx.cs" %>
	<asp:GridView ID="gvNews" runat="server" AutoGenerateColumns="false"
	AllowSorting="True"  AllowPaging="True" PageSize="4">
	<Columns>
		<asp:BoundField ReadOnly="True" HeaderText="Filing Date"
		DataField="FilingDate" SortExpression="FilingDate"></asp:BoundField>
		<asp:BoundField HeaderText="Company Name" DataField="CompanyName"
		SortExpression="CompanyName"></asp:BoundField>
		<asp:BoundField HeaderText="Headline Description" DataField="HeadlineDescription" >
		</asp:BoundField>
		<asp:BoundField HeaderText="Industry" DataField="Industry"
		SortExpression="Industry"></asp:BoundField>
		<asp:BoundField HeaderText="SIC Code" DataField="SICCode"
		SortExpression="SICCode"></asp:BoundField>
		<asp:BoundField HeaderText="Major Industry Group" DataField="MajorIndustryGroup"
		SortExpression="MajorIndustryGroup"></asp:BoundField>
	</Columns>
	</asp:GridView>