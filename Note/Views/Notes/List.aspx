<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Note.ViewModels.ListNotesViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Your Notes</h2>
    <% if (Model.Notes != null && Model.Notes.Count > 0)
       {%>
    <ul>
        <% foreach (var note in Model.Notes)
           {%>
        <li>
            <%= note.Title%></li>
        <%
            }%>
    </ul>
    <%}
       else
       {%>
    You have no notes.
    <%=Html.ActionLink("Add one now", "new","notes") %>
    <%
        }%>
</asp:Content>
