<%@ Page Title="" Language="C#" MasterPageFile="Layout.Master" AutoEventWireup="true" CodeBehind="MainForm.aspx.cs" Inherits="SchedulePresenter.Forms.MainForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Plan zajęć - Uniwersytet Śląski w Katowicach</title>
    <link rel="stylesheet" type="text/css" href="/Resources/Styles/MainForm.css" />
</asp:Content>
<asp:Content ContentPlaceHolderID="NavbarPlaceholder" runat="server">
    <div id="navbarContent" style="text-wrap: normal;" <!-- overflow-wrap:break-word;--> >
        <asp:Panel ID="departmentsPanel" runat="server" HorizontalAlign="Center" Height="20" CssClass="navbarContent" Wrap="true">
           
        </asp:Panel>
    </div>    
</asp:Content>
<asp:Content  ContentPlaceHolderID="LeftMenuPlaceHolder" runat="server">    
    <asp:Panel runat="server" ScrollBars="Auto">
        <div id="labelGroups">
            <asp:Label ID="Label2" runat="server" Text="WYBÓR GRUPY"></asp:Label>
        </div>        
        <div id="leftBarContent">
            <asp:TreeView ID="treeViewGroups" runat="server" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">            
            </asp:TreeView>
        </div>        
    </asp:Panel>            
</asp:Content>
<asp:Content  ContentPlaceHolderID="MainContentPlaceholer" runat="server">    
    <div id="mainContent">        
        <asp:Label ID="Label1" CssClass="groupLabel" runat="server" Text=""></asp:Label>
        <asp:DropDownList ID="DropDownList1" CssClass="dropDown" runat="server" ></asp:DropDownList>
        <asp:Button ID="buttonPdf" CssClass="btt" runat="server" Text="Pobierz plan w formacie PDF" Visible="false" OnClick="buttonPdf_Click"/>
        <asp:Button ID="buttonPng" CssClass="btt" runat="server" Text="Pobierz plan w formacie PNG" Visible="false" OnClick="buttonPng_Click"/>
    </div>    
</asp:Content>
