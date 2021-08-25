<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditCMI_News.ascx.cs" Inherits="CMI_News.EditCMI_News" %>
<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
  <script src="/DesktopModules/CMI_News/scripts/number.js"></script>
<script>

    $(function () {
        var currentDate = new Date();
        $(".datepicker").datepicker({
            dateFormat: "mm-dd-yy",
            numberOfMonths: 1
        });
        var selecteddate = $(".datepicker").datepicker("getDate");
        if (selecteddate ==null)
            $(".datepicker").datepicker("setDate", currentDate);

    });

    function CIKChanged(field)
    {
        validateNumericOnly(field,0);

        var v =field.value;
        if (v.length == 10) {
            var obj = ciks.find(o => o.value === v);
            if (obj == null)
                document.getElementById("txtCompanyName").value="";
            else
                document.getElementById("txtCompanyName").value = obj.company;
        }
    }

    
    function CIKblur(field)
    {
        var v =field.value;

        var obj = ciks.find(o => o.value === v);
        if (obj == null)
            document.getElementById("txtCompanyName").value="";
    }
  </script>
<style>

    .hasDatepicker {
    position: relative;
    z-index: 9;
}

</style>
<table style="border-collapse: separate; border-spacing: 6px;" >
	<tr>
		<td class="SubHead"><asp:Label id="lblAcceptanceDate" runat="server" AssociatedControlID="txtAcceptanceDate" 
            Text="Acceptance Date"></asp:Label></td>
		<td>
			<asp:TextBox runat="server" ID="txtAcceptanceDate" CssClass="datepicker" ClientIDMode="Static"></asp:TextBox>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtAcceptanceDate"
				CssClass="NormalRed" Display="Dynamic" ErrorMessage="<br>Acceptance Date is required" Runat="server" />
		</td>
	</tr>
	<tr>
		<td class="SubHead"><asp:Label id="lblFilingDate" runat="server" AssociatedControlID="txtFilingDate" 
            Text="Filing Date"></asp:Label></td>
		<td>
			<asp:TextBox runat="server" ID="txtFilingDate" CssClass="datepicker" ClientIDMode="Static"></asp:TextBox>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtFilingDate"
				CssClass="NormalRed" Display="Dynamic" ErrorMessage="<br>Filing Date is required" Runat="server" />
		</td>
	</tr>
	<tr>
		<td class="SubHead"><asp:Label id="lblCIKCode" runat="server" AssociatedControlID="txtCIKCode" Text="CIK Code"></asp:Label></td>
		<td>
			<asp:TextBox runat="server" ID="txtCIKCode" MaxLength="10" onkeyup="CIKChanged(this)" onblur="CIKblur(this)" ClientIDMode="Static"></asp:TextBox>
			<asp:RequiredFieldValidator ID="valCIKCode" ControlToValidate="txtCIKCode"
				CssClass="NormalRed" Display="Dynamic" ErrorMessage="<br>CIK Code is required" Runat="server" />
		</td>
	</tr>
	<tr>
		<td class="SubHead"><asp:Label id="lblCompanyName" runat="server" AssociatedControlID="txtCompanyName" 
            Text="Company Name"></asp:Label></td>
		<td>
			<asp:TextBox runat="server" ID="txtCompanyName" ClientIDMode="Static" Width="500px"></asp:TextBox>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCompanyName"
				CssClass="NormalRed" Display="Dynamic" ErrorMessage="<br>Company Name is required" Runat="server" />
		</td>
	</tr>
    <tr>
		<td class="SubHead"><asp:Label id="lblMajorIndustryGroup" runat="server" AssociatedControlID="rblMajorIndustryGroup" 
            Text="Major Industry Group"></asp:Label></td>
		<td>
			<asp:RadioButtonList runat="server" ID="rblMajorIndustryGroup" AutoPostBack="true" RepeatColumns="2" CellSpacing="12" 
                DataTextField="MajorIndustryGroup" DataValueField="MajorIndustryGroupId" OnSelectedIndexChanged="rblMajorIndustryGroup_SelectedIndexChanged"></asp:RadioButtonList>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rblMajorIndustryGroup"
				CssClass="NormalRed" Display="Dynamic" ErrorMessage="<br>Major Industry Group is required" Runat="server" />
		</td>
	</tr>
    <tr>
		<td class="SubHead"><asp:Label id="lblIndustry" runat="server" AssociatedControlID="ddlIndustry" Text="Industry"></asp:Label></td>
		<td>
			<asp:DropDownList runat="server" ID="ddlIndustry" 
                DataTextField="Industry" DataValueField="SICCode"></asp:DropDownList>
            <asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" text="*" controltovalidate="ddlIndustry" 
                CssClass="NormalRed" Display="Dynamic" ErrorMessage="<br>Industry is required" 
                initialvalue="None"></asp:requiredfieldvalidator>
		</td>
	</tr>
    <tr>
		<td class="SubHead"><asp:Label id="lblLoan" runat="server" AssociatedControlID="ckbLoan" Text="Debt Transaction"></asp:Label></td>
		<td>
			<asp:CheckBox runat="server" ID="ckbLoan" Text="Loan" ></asp:CheckBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:CheckBox runat="server" ID="ckbBond" Text="Bond" ></asp:CheckBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:CheckBox runat="server" ID="ckbStock" Text="Stock" ></asp:CheckBox>
		</td>
	</tr>
	<tr>
		<td class="SubHead"><asp:Label id="lblMoneyLoan" runat="server" AssociatedControlID="txtMoneyLoan" Text="New Money Loan"></asp:Label></td>
		<td>
			<asp:TextBox runat="server" ID="txtMoneyLoan"  onkeyup="validateNumericOnly(this, 0)"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td class="SubHead"><asp:Label id="lblMoneyBond" runat="server" AssociatedControlID="txtMoneyBond" Text="New Money Bond/Notes"></asp:Label></td>
		<td>
			<asp:TextBox runat="server" ID="txtMoneyBond" onkeyup="validateNumericOnly(this, 0)"></asp:TextBox>
		</td>
	</tr>
    	<tr>
		<td class="SubHead"><asp:Label id="lblProposedLoan" runat="server" AssociatedControlID="txtProposedLoan" Text="Proposed Loan"></asp:Label></td>
		<td>
			<asp:TextBox runat="server" ID="txtProposedLoan"  onkeyup="validateNumericOnly(this, 0)"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td class="SubHead"><asp:Label id="lblProposedBondNotes" runat="server" AssociatedControlID="txtProposedBondNotes" Text="Proposed Money Bond/Notes"></asp:Label></td>
		<td>
			<asp:TextBox runat="server" ID="txtProposedBondNotes" onkeyup="validateNumericOnly(this, 0)"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td class="SubHead"><asp:Label id="lblClosed" runat="server" AssociatedControlID="chkClosed" Text="Closed"></asp:Label></td>
		<td>
            <asp:CheckBox runat="server" ID="chkClosed" />
		</td>
	</tr>
	<tr>
		<td class="SubHead"><asp:Label id="lblDocumentIncluded" runat="server" AssociatedControlID="chkDocumentIncluded" 
            Text="Document Included"></asp:Label></td>
		<td>
            <asp:CheckBox runat="server" ID="chkDocumentIncluded" />
		</td>
	</tr>
    <tr>
		<td class="SubHead"><asp:Label id="Label1" runat="server" AssociatedControlID="txtActualCurrency" Text="Actual Currency"></asp:Label></td>
		<td>
			<asp:TextBox runat="server" ID="txtActualCurrency" MaxLength="5"></asp:TextBox>
		</td>
	</tr>
    <tr>
		<td class="SubHead"><asp:Label id="lbl8kFormDescription" runat="server" AssociatedControlID="txt8kFormDescription" 
            Text="8k Form Description"></asp:Label></td>
		<td>
			<asp:TextBox runat="server" ID="txt8kFormDescription" TextMode="MultiLine" Width="800px" Height="280px"></asp:TextBox>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txt8kFormDescription"
				CssClass="NormalRed" Display="Dynamic" ErrorMessage="<br>8k Form Description is required" Runat="server" />
		</td>
	</tr>
    <tr>
		<td class="SubHead">
            <asp:Label id="lblFilingHyperlink" AssociatedControlID="txtFilingHyperlink" runat="server"  Text="Filing Hyperlink"></asp:Label>
		</td>
		<td>
			<asp:TextBox runat="server" ID="txtFilingHyperlink" Width="800px" MaxLength="256"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFilingHyperlink" 
                Text="*" CssClass="NormalRed" Display="Dynamic" ErrorMessage="<br>Filing Hyperlink is required"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator runat="server" ControlToValidate="txtFilingHyperlink" ID="RegularExpressionValidator1" 
            ValidationExpression="(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?"
            Text="Invalid URL" ForeColor="red"></asp:RegularExpressionValidator>		
		</td>
	</tr>
    <tr>
		<td class="SubHead"><asp:Label id="lblHeadlineDescription" runat="server" AssociatedControlID="txtHeadlineDescription" 
            Text="Headline Description"></asp:Label></td>
		<td>
			<asp:TextBox runat="server" ID="txtHeadlineDescription" TextMode="MultiLine" Width="800px" Height="80px"></asp:TextBox>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtHeadlineDescription"
				CssClass="NormalRed" Display="Dynamic" ErrorMessage="<br>Headline Description is required" Runat="server" />
		</td>
	</tr>
    <tr>
		<td class="SubHead">
            <asp:Label id="lbl8KFormHyperlink" AssociatedControlID="txt8KFormHyperlink" runat="server"  Text="8K Form Hyperlink"></asp:Label>
		</td>
		<td>
			<asp:TextBox runat="server" ID="txt8KFormHyperlink" Width="800px" MaxLength="256"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt8KFormHyperlink" 
                Text="*" CssClass="NormalRed" Display="Dynamic" ErrorMessage="<br>8k Form Hyperlink is required"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator runat="server" ControlToValidate="txt8KFormHyperlink" ID="RegularExpressionValidator2" 
            ValidationExpression="(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?"
            Text="Invalid URL" ForeColor="red"></asp:RegularExpressionValidator>		
		</td>
	</tr>
    <tr>
		<td class="SubHead"><asp:Label id="lblFeatured" runat="server" AssociatedControlID="rblFeatured" 
            Text="Featured"></asp:Label></td>
		<td>
			<asp:RadioButtonList runat="server" ID="rblFeatured" RepeatColumns="4" CellSpacing="12" CellPadding="12" >
                <asp:ListItem Text="None" Value="4" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Featured 1" Value="1"></asp:ListItem>
                <asp:ListItem Text="Featured 2" Value="2"></asp:ListItem>
                <asp:ListItem Text="Featured 3" Value="3"></asp:ListItem>
			</asp:RadioButtonList>
		</td>
	</tr>
    	<tr>
		<td class="SubHead"><asp:Label id="lblImageID" runat="server" AssociatedControlID="txtImageID" Text="ImageID"></asp:Label></td>
		<td>
			<asp:TextBox runat="server" ID="txtImageID" ClientIDMode="Static" onkeyup="validateNumericOnly(this, 0)"></asp:TextBox>
		</td>
	</tr>
    	<tr>
		<td class="SubHead"><asp:Label id="Label2" runat="server" AssociatedControlID="txtFilingForm" Text="Filing Form"></asp:Label></td>
		<td>
			<asp:TextBox runat="server" ID="txtFilingForm"></asp:TextBox>
		</td>
	</tr>
</table>
<p>
    <asp:linkbutton cssclass="btn btn-primary" id="cmdUpdate" runat="server" text="Update" OnClick="cmdUpdate_Click"></asp:linkbutton>&nbsp;
    <asp:linkbutton cssclass="btn btn-info" id="cmdCancel" runat="server" text="Cancel" causesvalidation="False" OnClick="cmdCancel_Click"></asp:linkbutton>&nbsp;
    <asp:linkbutton cssclass="btn btn-danger" id="cmdDelete" runat="server"  text="Delete" causesvalidation="False" OnClick="cmdDelete_Click"></asp:linkbutton>&nbsp;
</p>
