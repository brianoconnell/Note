<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Notes.master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>
<asp:Content ID="HeadConent1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">simpleRequest();</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     <div id="wrapper">
        <div id="page">
            <div id="header">Header</div>
            <div id="notes">
                <div id="noteslist"></div>
                <div id="note">
                    <div id="title"></div>
                    <div id="content"></div>
                </div>
            </div>
            <div id="footer">Footer</div>
            
        </div>
    </div>

    
</asp:Content>
