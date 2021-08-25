<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Company.ascx.cs" Inherits="CMI_News.Company" %>
<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
<script>

    function CIKChanged(v)
    {
        if (v.length == 10) {
            var obj = ciks.find(o => o.value === v);
            if (obj == null)
            {
                document.getElementById("txtCompanyName").value="";
                document.getElementById("cmdDelete").style.display="none";
            }
            else
            {
                document.getElementById("txtCompanyName").value = obj.company;
                if (obj.num=="0")
                    document.getElementById("cmdDelete").style.display="";
                else
                    document.getElementById("cmdDelete").style.display="none";
            }
        }
    }
  </script>
  <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">

<table>
	<tr>
		<td class="SubHead"><asp:Label id="lblCIKCode" runat="server" AssociatedControlID="txtCIKCode" Text="CIK Code: "></asp:Label></td>
		<td>
			<asp:TextBox runat="server" ID="txtCIKCode" MaxLength="10"  ClientIDMode="Static"></asp:TextBox>
			<asp:RequiredFieldValidator ID="valCIKCode" ControlToValidate="txtCIKCode"
				CssClass="NormalRed" Display="Dynamic" ErrorMessage="<br>CIK Code is required" Runat="server" />
		</td>
	</tr>
	<tr>
		<td class="SubHead"><asp:Label id="lblCompanyName" runat="server" AssociatedControlID="txtCompanyName" 
            Text="Company Name: "></asp:Label></td>
		<td>
			<asp:TextBox runat="server" ID="txtCompanyName" ClientIDMode="Static" Width="500px"></asp:TextBox>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCompanyName"
				CssClass="NormalRed" Display="Dynamic" ErrorMessage="<br>Company Name is required" Runat="server" />
		</td>
	</tr>
</table>
<p>
    <asp:linkbutton id="cmdUpdate" ClientIDMode="Static" runat="server" CssClass="btn btn-primary" text="Update" OnClick="cmdUpdate_Click"></asp:linkbutton>&nbsp;
    <asp:linkbutton id="cmdCancel" runat="server"  CssClass="btn btn-info" text="Cancel" causesvalidation="False" OnClick="cmdCancel_Click"></asp:linkbutton>&nbsp;
    <asp:linkbutton id="cmdDelete" ClientIDMode="Static" runat="server" 
          CssClass="btn btn-danger" text="Delete" causesvalidation="False"
        OnClientClick="confirm('Are you sure you want to permanently delete this News?')" OnClick="cmdDelete_Click"></asp:linkbutton>&nbsp;
</p>

<a href='<%=DotNetNuke.Common.Globals.NavigateURL()%>' class="btn btn-info" >Main View</a>