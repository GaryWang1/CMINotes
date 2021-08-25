<%@ Control Language="C#" Inherits="CMI_News.HomeTop3"
    AutoEventWireup="true" CodeBehind="HomeTop3.ascx.cs" %>
<script>
    /*
    $(function () {
        $("[id^=dialog_]").dialog({
            autoOpen: false,
            resizable: false,
            height: 500,
            width: 300
        });

        $("#opener").on("click", function () {
            $("#dialog").dialog("open");
        });
    });
    */
  </script>
<style>

    .ui-dialog .ui-dialog-titlebar-close {
    position: absolute;
    right: .30em !important;
    top: 20% !important;
}

</style>
	<asp:Repeater runat="server" ID="rptNews" >
		<ItemTemplate>
            <div class="row">
                <img class="img-responsive" alt="<%#Eval("MajorIndustryGroup")%>" src="/Portals/_default/CMI_NEWS/ImagesBank/iStock-<%#Eval("ImageID")%>.jpg" title="<%#Eval("MajorIndustryGroup")%>">
                <h3><%#Eval("HeadlineDescription").ToString()%></h3>
                <p><%#CMI_News.NewsCommon.stripHTML(Eval("k8FormDescription").ToString(),300)%>
                    <span class="font-weight-bold" id="opener_<%#Eval("NewsID").ToString()%>">More ...</span>
                </p>
                <div id="dialog_<%#Eval("NewsID").ToString()%>" title="8k Form Description">
                  <%#Eval("k8FormDescription").ToString().Trim()%>
                </div>
<script>
    $(function () {
        $("#dialog_<%#Eval("NewsID").ToString()%>").dialog({
            autoOpen: false,
            resizable: false,
            width:  800
        });

        $("#opener_<%#Eval("NewsID").ToString()%>").on("click", function () {
            $("#dialog_<%#Eval("NewsID").ToString()%>").dialog("open");
        });
    });
</script>
                <div class="pull-left"><a onclick="popupMax(this);return false;" href=<%#Eval("FilingHyperlink").ToString()%>>Filing Hyperlink</a></div>
                <div class="pull-right"><a onclick="popupMax(this);return false;" href=<%#Eval("K8FormHyperlink").ToString()%>>8K Form Hyperlink</a></div>
                <br />
            </div>
			<hr class="col-xs-12">
		</ItemTemplate>
	</asp:Repeater>