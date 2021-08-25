<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UploadImages.ascx.cs" Inherits="CMI_News.UploadImages" %>

<h3>Upload Top 3 Images to Image Bank:</h3>
<p>Allow multiple images selection. The image file names have to be something like, "iStock-123456.jpg", images do not follow the naming convention can not be uploaded.</p>
<br />         <asp:FileUpload ID="fupFileUpload" AllowMultiple="true" runat="server" />
<br /><br />
<asp:Button runat="server" ID="btnUpload" CssClass="btn btn-primary" OnClick="btnUpload_Click" Text="Upload Images" />
<br /><br />

<div id="status" runat="server" class="alert alert-success"  Visible="false">
    <asp:Label ID="lblStatus" runat="server" ></asp:Label>
</div>
<div id="statusErr" runat="server" class="alert alert-danger" Visible="false">
    <asp:Label ID="lblStatusErr" runat="server" ></asp:Label>
</div>
<h3>Image Bank:</h3>
        <asp:GridView ID="gvFile" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-hover" ShowHeader="false">  
            <Columns>  
                <asp:TemplateField>  
                    <ItemTemplate>  
                        <asp:CheckBox ID="chkDel" runat="server" />  
                    </ItemTemplate>  
                </asp:TemplateField>  

            </Columns>  
        </asp:GridView>  <br />
<asp:Button runat="server" ID="btnDeleteFile" CssClass="btn btn-danger" OnClick="btnDeleteFile_Click" Text="Delete Selected Images" />
<a href='<%=DotNetNuke.Common.Globals.NavigateURL()%>' class="btn btn-info" >Main View</a>