<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Note.ViewModels.UserRegisterViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Register
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Register</h2>

    <%using (Html.BeginForm())
      {%>
        <ul>
            <li><%=Html.LabelFor(x =>x.Username) %> <%=Html.TextBoxFor(x => x.Username) %></li>
            <li><%= Html.LabelFor(x => x.Password) %> <%=Html.PasswordFor(x=>x.Password) %></li>
            <li><%= Html.LabelFor(x => x.PasswordRepeat) %> <%=Html.PasswordFor(x => x.PasswordRepeat) %></li>
            <li><%= Html.LabelFor(x => x.Email) %> <%= Html.TextBoxFor(x => x.Email) %></li>
        </ul>

        <input type="submit" />
    <%
      }%>
</asp:Content>
