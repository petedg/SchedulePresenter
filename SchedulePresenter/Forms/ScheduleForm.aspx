<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScheduleForm.aspx.cs" Inherits="SchedulePresenter.Forms.ScheduleForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Plan zajęć - Uniwersytet Śląski w Katowicach</title>
    <script src="//code.jquery.com/jquery-1.12.0.min.js"></script>
    <script src="/Resources/js/jquery.mmenu.all.min.js" type="text/javascript"></script>
    <link href="/Resources/css/jquery.mmenu.all.css" type="text/css" rel="stylesheet" />
    <link href="/Resources/css/schedule.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {
            $("#app-menu").mmenu({
                extensions: ["theme-white", "effect-menu-slide", "multiline", "pagedim-black"],
                slidingSubmenus: false,
                navbar: {
                    title: "WYBÓR GRUPY",

                }
            });
        });
    </script>
</head>
<body>
    <!-- The page -->
    <div class="page">
        <form id="form1" runat="server">     
             <header>
                <a href="#app-menu"></a>
                <div>
                    <span>Plan zajęć - Uniwersytet Śląski w Katowicach</span>
                </div>
             </header>

             <div id="content">                
                <h4 id="label" runat="server">Aby rozpocząć, wybierz grupę z menu...</h4>
                 <asp:DropDownList ID="DropDownList1" CssClass="dropDown" Visible="false" runat="server" AutoPostBack="true"></asp:DropDownList>
                 <asp:Button ID="buttonPdf" CssClass="btt" runat="server" Text="Pobierz plan w formacie PDF" Visible="false" OnClick="buttonPdf_Click"/>
                 <asp:Button ID="buttonPng" CssClass="btt" runat="server" Text="Pobierz plan w formacie PNG" Visible="false" OnClick="buttonPng_Click"/>
             </div>

              <footer>

              </footer>     
        </form>
    </div>      

    <!-- The menu -->
      <nav id="app-menu">
          <ul id="menuList" runat="server">            
            
         </ul>
      </nav>
</body>
</html>
