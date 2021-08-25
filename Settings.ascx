<%@ Control Language="C#" AutoEventWireup="false" Inherits="CMI_News.Settings" Codebehind="Settings.ascx.cs" %>
<style>
    .CMILabel {
        float: left;
        width: 280px;
        font-weight:bold;
        color:#000;
        padding-left:8px;
        padding-bottom:6px;

    }
    .CMIText {
        padding-bottom:5px;
    }
</style>
<asp:Label CssClass="CMILabel" runat="server" ID="lblDataType" Text = "Display Type: " ControlName="lblDataType" />
<asp:DropDownList runat="server" ID="ddlDataType"></asp:DropDownList>
<div style='clear:both'></div>
<asp:Label CssClass="CMILabel" runat="server" ID="lblCMINewsSearchShowNum"  Text = "Maximum Rows Display for Search 

Results: " ControlName="txtCMINewsSearchShowNum" />
<asp:textbox id="txtCMINewsSearchShowNum"  runat="server" Width="20px" /> (Default 50)
<div style='clear:both'></div>
<asp:Label CssClass="CMILabel" runat="server" ID="Label2" Text = "Daily Transaction Download limit: " 

ControlName="txtCMINewsMaximumDownloadNum" />
<asp:textbox id="txtCMINewsMaximumDownloadNum"  runat="server" Width="20px" /> (Default 50)
<div style='clear:both'></div>
<asp:Label CssClass="CMILabel" runat="server" ID="lblCMINewsTableShowNum" Text = "Rows Display for Table View: " 

ControlName="txtCMINewsTableShowNum" />
<asp:textbox id="txtCMINewsTableShowNum" runat="server" Width="20px" /> (Default 30)
<div style='clear:both'></div>
<asp:Label CssClass="CMILabel" runat="server" ID="lblCMINewsTop3ShowNum" Text = "Items Display for Top 3: " 

ControlName="txtCMINewsTop3ShowNum" />
<asp:textbox id="txtCMINewsTop3ShowNum"  runat="server" Width="20px" /> (Default 3)
<div style='clear:both'></div>
<asp:Label CssClass="CMILabel" runat="server" ID="Label1" Text = "Items Display for Admin Latest News List: " 

ControlName="txtCMINewsLatestShowNum" />
<asp:textbox id="txtCMINewsLatestShowNum" runat="server" Width="20px" /> (Default 60)
<div style='clear:both'></div>
<asp:Label CssClass="CMILabel" runat="server" ID="Label3" Text = "Subscribe Now Title: " 

ControlName="txtSubscribeNowTitle" />
<asp:textbox id="txtSubscribeNowTitle"  Width="500px" runat="server" />
<div style='clear:both'></div>
<asp:Label CssClass="CMILabel" runat="server" ID="Label4" Text = "Subscribe Now Link: " ControlName="txtSubscribeNowlink" 

/>
<asp:textbox id="txtSubscribeNowlink"   Width="500px" runat="server" />
<div style='clear:both'></div>
<asp:Label CssClass="CMILabel" runat="server" ID="Label5" Text = "7 days Trial Title: " ControlName="txt7daysTrialTitle" 

/>
<asp:textbox id="txt7daysTrialTitle"  Width="500px" runat="server" />
<div style='clear:both'></div>
<asp:Label CssClass="CMILabel" runat="server" ID="Label6" Text = "7 days Trial Link: " ControlName="txt7daysTrialLink" />
<asp:textbox id="txt7daysTrialLink"  Width="500px" runat="server" />
<div style='clear:both'></div>
<asp:Label CssClass="CMILabel" runat="server" ID="Label7" Text = "News Search Title: " ControlName="txtNewsSearchTitle" />
<asp:textbox id="txtNewsSearchTitle"   Width="500px" runat="server" />
<div style='clear:both'></div>
<asp:Label CssClass="CMILabel" runat="server" ID="Label8" Text = "News Search Link: " ControlName="txtNewsSearchLink" />
<asp:textbox id="txtNewsSearchLink"  Width="500px" runat="server" />
<div style='clear:both'></div>
<asp:Label CssClass="CMILabel" runat="server" ID="Label9" Text = "Table View Instruction " 

ControlName="txtTableViewInstruction" />
<asp:textbox id="txtTableViewInstruction"   Width="500px" Height="80px" TextMode="MultiLine" runat="server" />




