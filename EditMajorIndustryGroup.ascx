<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditMajorIndustryGroup.ascx.cs" Inherits="CMI_News.EditMajorIndustryGroup" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<script type="text/javascript">
    $(document).ready(function () {
        $('#iconUploadBtn').click(function () {
            $('#fileUpload').trigger('click');
        });

        $('#fileUpload').change(function () {
            $("#cmdUpload")[0].click();
        });

        $('#imageUploadBtn').click(function () {
            $('#fileUpload2').trigger('click');
        });

        $('#fileUpload2').change(function () {
            $("#cmdUpload2")[0].click();
        });
    });
</script>

<style>
    #cmdUpload, #fileUpload, #cmdUpload2, #fileUpload2 {
		display:none;
	}
	
	#lblMsg, #lblMsg2 {
		margin-left:25px;
	}
table.spacing { 
    border-spacing: 12px !important;
    border-collapse: separate;
}
.mycheckbox input[type="checkbox"] 
{ 
    margin-right: 8px; 
}
.mycheckbox
{ 
	font-size: 120% !important;
}
</style>
<table id="Table2" class="spacing" width="100%" border="0" >
    <tr>
        <td><h5>Major Industry Group ID:</h5>
        </td>
        <td>             
            <h5><asp:Label runat="server" ID="lblMajorIndustryGroupID" Text='<%# DataBinder.Eval(Container, "DataItem.MajorIndustryGroupID") %>'></asp:Label></h5>
        </td>
    </tr>
    <tr>
        <td><h5>Major Industry Group:</h5>
        </td>
        <td>
            <asp:TextBox ID="txtMajorIndustryGroup" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MajorIndustryGroup") %>'>
            </asp:TextBox>
        </td>
    </tr>

    <tr>
        <td colspan="2"></td>
    </tr>
    <tr>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>&nbsp;
                                    <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                        CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
