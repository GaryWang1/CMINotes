<%@ Control Language="C#" Inherits="CMI_News.TableView"
    AutoEventWireup="true" CodeBehind="TableView.ascx.cs" %>

<asp:HyperLink runat="server" ID="hplSearch" Text="Search News" NavigateUrl="~/Search.aspx"></asp:HyperLink>
<asp:Label runat="server" ID="pnlSubscribe">
    <p runat="server" id="pInstruction">Need some instruction here ...</p>
    <asp:HyperLink runat="server" ID="hplSubscribe" Text="Subscribe Now" NavigateUrl="~/SubscribeNow.aspx" CssClass="pull-left" ></asp:HyperLink>
    <asp:HyperLink runat="server" ID="hpl7dayTrial" Text="7 days Trial" NavigateUrl="~/7daysTrial.aspx" CssClass="pull-right" ></asp:HyperLink>
</asp:Label>
	<asp:GridView ID="gvNews" ClientIDMode="Static" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover">
	<Columns>
		<asp:BoundField ReadOnly="True" HeaderText="Filing Date" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" 
		DataField="FilingDate"></asp:BoundField>
        <asp:TemplateField HeaderText="Headline Description">
            <ItemTemplate>
                <%#CMI_News.NewsCommon.stripHTML(Eval("HeadlineDescription").ToString(),100)%>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Filing Hyperlink">
            <ItemTemplate>
                <a onclick="popupMax(this);return false;" href="<%#Eval("FilingHyperlink").ToString()%>">Filing Hyperlink</a>
            </ItemTemplate>
         </asp:TemplateField>       
        <asp:TemplateField HeaderText="8K Form Hyperlink">
            <ItemTemplate>
                <a onclick="popupMax(this);return false;" href="<%#Eval("K8FormHyperlink").ToString()%>">8K Form Hyperlink</a>
            </ItemTemplate>
         </asp:TemplateField>
	</Columns>
	</asp:GridView>