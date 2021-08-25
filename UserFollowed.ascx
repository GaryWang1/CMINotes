<%@ Control Language="C#" Inherits="CMI_News.UserFollowed"
    AutoEventWireup="true" CodeBehind="UserFollowed.ascx.cs" %>
<script src="/DesktopModules/CMI_News/scripts/popup.js"></script>
<script>
    $(function () {
        $(".divCollapses").on("click", function () {
            $(this).next().slideToggle();
        });
    });

    var sf = $.ServicesFramework(<%= ModuleId%>);
    var controllerPath = sf.getServiceRoot('WebAPIs') + 'CMINotesUserAPI/';

    function UserRead(link, id) {
        $.ajax({
            type: "POST",
            cache: false,
            url: controllerPath + 'UserRead',
            beforeSend: sf.setModuleHeaders,
            data: ({
                userId: <%= UserId%>,
                newsId: id
            })
        });

        $(link).removeClass("detaillink0").parent().removeClass("detail0");
        return popupMax(link);
    }

    function UserHide(link, id) {

        if (confirm("Are you sure you want to remove this record? ")) {
            $.ajax({
                type: "POST",
                cache: false,
                url: controllerPath + 'UserHide',
                beforeSend: sf.setModuleHeaders,
                data: ({
                    userId: <%= UserId%>,
                    newsId: id
                })
                });

            if ($(link).parent().siblings().size() == 0)
            {
                $(link).parent().parent().prev().addClass('disabled').removeClass('divCollapses');
                $(link).parent().parent().removeAttr("style");
            }
            $(link).parent().remove();   
        }
    return false;
    }

  </script>
<style>
.heading1{

        float: none;
        font-weight:bold;
        font-size:1.2em;
        color:#000;
        padding-left:8px;
        padding-top:8px;
        padding-bottom:8px;
        text-align:center;
        background:#d9edf7;
}

.heading2{

        float: none;
        font-weight:bold;
        font-size:1.1em;
        color:#000;
        padding-left:8px;
        padding-bottom:8px;
}
.heading3{

        float: none;
        color:#000;
        padding-left:16px;
        padding-bottom:8px;
}
.disabled{

    color:#ccc !important;
}
.detailsection{
       padding-left:24px;
       /*padding-bottom:8px;*/
       font-size:1.0em;
       display:none;
}
.detail0{
font-weight:bold;

}
a.detaillink1, a.detaillink1:link, a.detaillink1:visited, a.detaillink1:hover, a.detaillink1:focus, a.detaillink1:active{
  color:#337ab7;
}
 a.detaillink1:hover, a.detaillink1:focus, a.detaillink0:hover, a.detaillink0:focus{
  text-decoration:underline;
}
a.detaillink0, a.detaillink0:link, a.detaillink0:visited, a.detaillink0:hover, a.detaillink0:focus, a.detaillink0:active{
  color:#337ab7;
    font-weight:bold;
}
span.detail0, span.detail1{
display:block;

}

.deleteNews:after {
  content: '\D7';
  font-size: 20px;
  font-style:italic;
  color:red;
padding-right: 8px;
float:right;
}
.deleteNews{
  cursor: pointer;
}
 a.deleteNews:hover, a.deleteNews:focus{
  text-decoration:none;
}

</style>

<div class="row">
	<asp:Repeater runat="server" ID="rptCompany" OnItemDataBound="rptCompany_ItemDataBound" >         
        <HeaderTemplate>
            <div class="heading1">
                Corporations being Followed
            </div>
        </HeaderTemplate>
		<ItemTemplate>
            <div><div class="divCollapses heading2"><%#Eval("CompanyName")%></div>
                <div id="CIK<%#Eval("CIKCode")%>">
                    <div class="divCollapses heading3">Corporate Loan Activity</div>
                    <div class="detailsection">
                        <asp:Repeater ID="rptLoan" runat="server">
                            <ItemTemplate>
                                <span class="detail<%#Eval("Status")%>"><%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"FilingDate"))).ToString("dd/MM/yyyy") %> 
                                <a class="detaillink<%#Eval("Status")%>" onclick="return UserRead(this, <%#Eval("NewsID")%>);" href="<%#Eval("K8FormHyperlink")%>">
                                    <%# DataBinder.Eval(Container.DataItem,"HeadlineDescription") %></a>
                                    <a onclick="return UserHide(this, <%#Eval("NewsID")%>);" href="#" class="deleteNews"></a>
                                </span>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <div class="divCollapses heading3">Corporate Bond Activity</div>
                    <div class="detailsection">
                        <asp:Repeater ID="rptBond" runat="server">
                            <ItemTemplate>
                                <span class="detail<%#Eval("Status")%>"><%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"FilingDate"))).ToString("dd/MM/yyyy") %> 
                                <a class="detaillink<%#Eval("Status")%>" onclick="return UserRead(this, <%#Eval("NewsID")%>);" href="<%#Eval("K8FormHyperlink")%>">
                                    <%# DataBinder.Eval(Container.DataItem,"HeadlineDescription") %></a>
                                    <a onclick="return UserHide(this, <%#Eval("NewsID")%>);" href="#" class="deleteNews"></a>
                                </span>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="divCollapses heading3">Preferred Stock</div>
                    <div class="detailsection">
                        <asp:Repeater ID="rptStock" runat="server">
                            <ItemTemplate>
                                <span class="detail<%#Eval("Status")%>"><%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"FilingDate"))).ToString("dd/MM/yyyy") %> 
                                <a class="detaillink<%#Eval("Status")%>" onclick="return UserRead(this, <%#Eval("NewsID")%>);" href="<%#Eval("K8FormHyperlink")%>">
                                    <%# DataBinder.Eval(Container.DataItem,"HeadlineDescription") %></a>
                                    <a onclick="return UserHide(this, <%#Eval("NewsID")%>);" href="#" class="deleteNews"></a>
                                </span>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
		</ItemTemplate>            
	</asp:Repeater>
</div>

<div class="row">
	<asp:Repeater runat="server" ID="rptMajorIndustryGroup" OnItemDataBound="rptMajorIndustryGroup_ItemDataBound" >  
        <HeaderTemplate>
            <div class="heading1">
            Industries being followed
        </div>
    </HeaderTemplate>
		<ItemTemplate>
            <div><div class="divCollapses heading2"><%#Eval("MajorIndustryGroup")%></div>
                <div id="MajorIndustryGroupID<%#Eval("MajorIndustryGroupID")%>">
                    <div class="divCollapses heading3">Corporate Loan Activity</div>
                    <div class="detailsection">
                        <asp:Repeater ID="rptLoan" runat="server">
                            <ItemTemplate>
                                <span class="detail<%#Eval("Status")%>"><%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"FilingDate"))).ToString("dd/MM/yyyy") %> 
                                <a class="detaillink<%#Eval("Status")%>" onclick="return UserRead(this, <%#Eval("NewsID")%>);" href="<%#Eval("K8FormHyperlink")%>">
                                    <%# DataBinder.Eval(Container.DataItem,"HeadlineDescription") %></a>
                                    <a onclick="return UserHide(this, <%#Eval("NewsID")%>);" href="#" class="deleteNews"></a>
                                </span>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <div class="divCollapses heading3">Corporate Bond Activity</div>
                    <div class="detailsection">
                        <asp:Repeater ID="rptBond" runat="server">
                            <ItemTemplate>
                                <span class="detail<%#Eval("Status")%>"><%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"FilingDate"))).ToString("dd/MM/yyyy") %> 
                                <a class="detaillink<%#Eval("Status")%>" onclick="return UserRead(this, <%#Eval("NewsID")%>);" href="<%#Eval("K8FormHyperlink")%>">
                                    <%# DataBinder.Eval(Container.DataItem,"HeadlineDescription") %></a>
                                    <a onclick="return UserHide(this, <%#Eval("NewsID")%>);" href="#" class="deleteNews"></a>
                                </span>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="divCollapses heading3">Preferred Stock</div>
                    <div class="detailsection">
                        <asp:Repeater ID="rptStock" runat="server">
                            <ItemTemplate>
                                <span class="detail<%#Eval("Status")%>"><%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"FilingDate"))).ToString("dd/MM/yyyy") %> 
                                <a class="detaillink<%#Eval("Status")%>" onclick="return UserRead(this, <%#Eval("NewsID")%>);" href="<%#Eval("K8FormHyperlink")%>">
                                    <%# DataBinder.Eval(Container.DataItem,"HeadlineDescription") %></a>
                                    <a onclick="return UserHide(this, <%#Eval("NewsID")%>);" href="#" class="deleteNews"></a>
                                </span>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
		</ItemTemplate>                     
	</asp:Repeater>
</div>