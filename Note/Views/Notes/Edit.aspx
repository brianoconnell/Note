<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Note.ViewModels.EditNoteViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>
    <% using(Html.BeginForm()) {%>
    <fieldset>
        <ul>
            <li><%= Html.LabelFor(x=>x.Title) %> <%=Html.TextBoxFor(note => note.Title) %></li>
            <li><%= Html.LabelFor(x=>x.Content) %> <%=Html.TextAreaFor(note => note.Content) %></li>
        </ul>
    </fieldset>
    <input type="submit" />
    <%} %>
</asp:Content>
