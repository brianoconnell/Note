<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Note.ViewModels.UserSignInViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SignIn
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Sign In</h2>
    <% using(Html.BeginForm())
{ %>
        <fieldset>
        <ul>
            <li><%= Html.LabelFor(x => x.Username) %> <%= Html.TextBoxFor(x=>x.Username) %></li>
            <li><%= Html.LabelFor(x => x.Password) %> <%= Html.PasswordFor(x=>x.Password) %></li>
            <li><%= Html.LabelFor(x => Model.StaySignedIn) %> <%= Html.CheckBoxFor(x => x.StaySignedIn) %></li>
        </ul>
        </fieldset>
        <fieldset class="submit">
        <input type="submit"/>
        </fieldset>
  
<% } %>
</asp:Content>
