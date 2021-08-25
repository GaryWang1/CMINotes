<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UploadNews.ascx.cs" Inherits="CMI_News.UploadNews" %>

<div id="status" runat="server">
    <asp:Literal ID="litStatus" runat="server" ></asp:Literal>
</div>
<br />
<h3>News File Upload:</h3>
<p><asp:Literal ID="litFileLocation" runat="server" Visible="false"></asp:Literal></p>

            <asp:FileUpload ID="fupFileUpload" runat="server" />
<br />
<asp:Button runat="server" ID="btnUpload" CssClass="btn btn-primary" OnClick="btnUpload_Click" Text="Upload News Item File" />
<h3>Archived News Files:</h3>
        <asp:GridView ID="gvFile" runat="server" AutoGenerateColumns="true"  CssClass="table table-striped table-hover" ShowHeader="false">  
            <Columns>  
                <asp:TemplateField>  
                    <ItemTemplate>  
                        <asp:CheckBox ID="chkDel" runat="server" />  
                    </ItemTemplate>  
                </asp:TemplateField>  

            </Columns>  
        </asp:GridView>  <br />
<asp:Button runat="server" ID="btnDeleteFile" CssClass="btn btn-danger" OnClick="btnDeleteFile_Click" Text="Delete Selected Archived News Files" />
<a href='<%=DotNetNuke.Common.Globals.NavigateURL()%>' class="btn btn-info"  >Main View</a>