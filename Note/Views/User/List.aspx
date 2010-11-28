<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Note.ViewModels.ListUsersViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>List</h2>

    <ul>
        <% foreach (var user in Model.Users)
{%>
    <li><%=user.Username %></li>
<%} %>
    </ul>
</asp:Content>
