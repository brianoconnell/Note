<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (Request.IsAuthenticated) {
%>
        Welcome <b><%: Page.User.Identity.Name %></b>!
        [ <%: Html.ActionLink("Sign Out", "SignOut", "User") %> ]
<%
    }
    else {
%> 
        [ <%: Html.ActionLink("Sign In", "SignIn", "User") %> ]
<%
    }
%>
