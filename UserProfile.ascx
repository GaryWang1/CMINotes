<%@ Control Language="C#" Inherits="CMI_News.UserProfile"
    AutoEventWireup="true" CodeBehind="UserProfile.ascx.cs" %>
  <link rel="stylesheet" type="text/css" href="/DesktopModules/CMI_News/css/jquery.dropdown.css">
  <script src="/DesktopModules/CMI_News/scripts/jquery.dropdown.js"></script>
<script>
    function altFind(arr, callback) {
        for (var i = 0; i < arr.length; i++) {
            var match = callback(arr[i]);
            if (match) {
                return arr[i];
                break;
            }
        }
    }
  </script>
<style>

.dropdown-display-label:after, .dropdown-display:after {
    display: none;
}

    .dropdown-selected {
        display: block !important;
        border:none !important;
    }
    .profile{

        font-weight:bold;
        font-size:1.2em;
        color:#000;
        padding-left:8px;
        padding-bottom:8px;
        overflow: hidden;
        word-wrap: normal !important;
}

        .profileposition{

        font-size:1.1em;
        color:#000;
        padding-left:8px;
        padding-bottom:8px;
        overflow: hidden;
        word-wrap: normal !important;
}

        .profilepicture{

        float: left;

        padding-left:8px;
        padding-bottom:8px;

}

        .profileout {
    z-index: 100;
    margin-left: 4px;
    margin-top: 0px;
    float: none;
    width: 100%;
    display:block;
}

            .CMILabel {
        float: left;
        width: 180px;
        font-weight:bold;
        color:#000;
        padding-left:8px;
        padding-bottom:8px;

    }
    .CMIText {
        padding-bottom:8px;
    }
</style>
<div class="profileout">

<asp:Image runat="server" CssClass="profilepicture" ID="imgPicture" />
<div class="profile"><asp:HyperLink runat="server" ID="hplName"></asp:HyperLink></div>
<div class="profileposition">Position: <asp:Label runat="server" ID="lblPosition"></asp:Label></div>
<div class="profileposition">Compamy: <asp:Label runat="server" ID="lblCompany"></asp:Label></div>
<div class="profileposition">Last Visit Date: <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblLastViewDate"></asp:Label></div>
</div>
<br />
<div class="row">
    <asp:HiddenField runat="server" ID="hidCIKs2" ClientIDMode="Static" />
<div class="CMILabel CMIText">Corporations Followed:</div>     
<div class="dropdown-mul-2">
        <select style="display:none"  name="" id="" multiple > </select>
</div> 
    <asp:HiddenField runat="server" ID="hidMajorIndustryGroupIDs" ClientIDMode="Static" />
<div class="CMILabel CMIText">Industries Followed: </div>     
<div class="dropdown-mul-3">
        <select style="display:none"  name="" id="" multiple > </select>
</div> 


  <script>


      $('.dropdown-mul-2').dropdown({
          data: ciks,
          limitCount: 40,
          multipleMode: 'label',
          input: '<input type="text" size="43"  maxLength="20" placeholder="Type and Pick a Company to Follow ...">',
          confirm: 'Are you sure you want to unfollow the company?',
          choice: function (event, ui) {
              addCompany(ui.id);
              //$('.dropdown-search').find('input').val('');
          }

      });
      /*
      function updatehidCIKs2() {
          var hidCIKs2 = "";
          $('.dropdown-mul-2 i.del').each(function (index) {
              hidCIKs2 += this.dataset.id + ",";
          });
          if (hidCIKs2.length > 0)
              $("#hidCIKs2").val(hidCIKs2.substr(0, hidCIKs2.length - 1));
          else
              $("#hidCIKs2").val("");
      }
      */
      $('.dropdown-mul-3').dropdown({
          data: majorIndustryGroupIDs,
          limitCount: 40,
          multipleMode: 'label',
          input: '<input type="text" size="43"  maxLength="20" placeholder="Type and Pick an Industry to Follow ...">',
          confirm: 'Are you sure you want to unfollow the industry?',
          choice: function (event, ui) {
              addMajorIndustryGroup(ui.id);
              //$('.dropdown-search').find('input').val('');
          }
      });
      /*
      function updatehidMajorIndustryGroupIDs() {
          var hidMajorIndustryGroupIDs = "";
          $('.dropdown-mul-3 i.del').each(function (index) {
              hidMajorIndustryGroupIDs += this.dataset.id + ",";
          });
          if (hidMajorIndustryGroupIDs.length > 0)
              $("#hidMajorIndustryGroupIDs").val(hidMajorIndustryGroupIDs.substr(0, hidMajorIndustryGroupIDs.length - 1));
          else
              $("#hidMajorIndustryGroupIDs").val("");
      }
      */
      function addCompany(CIKCode) {
          $.ajax({
              type: "POST",
              cache: false,
              url: controllerPath + 'UserFollowCompany',
              beforeSend: sf.setModuleHeaders,
              data: ({
                  userId: <%= UserId%>,
                  CIKCode: CIKCode
              }),
              async: false
          });

          location.reload(true);
      }

      function addMajorIndustryGroup(MajorIndustryGroupId) {

          $.ajax({
              type: "POST",
              cache: false,
              url: controllerPath + 'UserFollowMajorIndustryGroup',
              beforeSend: sf.setModuleHeaders,
              data: ({
                  userId: <%= UserId%>,
                  MajorIndustryGroupId: MajorIndustryGroupId
              }),
              async: false
          });

          location.reload(true);
      }

      function deleteCompany(CIKCode) {
          $.ajax({
              type: "POST",
              cache: false,
              url: controllerPath + 'UserUnFollowCompany',
              beforeSend: sf.setModuleHeaders,
              data: ({
                  userId: <%= UserId%>,
                  CIKCode: CIKCode
              }),
              async: false
          });

          location.reload(true);
      }

      function deleteMajorIndustryGroup(MajorIndustryGroupId) {

          $.ajax({
              type: "POST",
              cache: false,
              url: controllerPath + 'UserUnFollowMajorIndustryGroup',
              beforeSend: sf.setModuleHeaders,
              data: ({
                  userId: <%= UserId%>,
                  MajorIndustryGroupId: MajorIndustryGroupId
              }),
              async: false
          });

          location.reload(true);
      }
</script>
</div>
