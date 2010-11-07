<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Your Notes</h2>
    <div id="wrapper">
        <div id="notepage">
            <div id="noteheader">Header</div>
            <div id="notes">
                <div id="noteslist">
                    <ol id="selectable"></ol>
                </div>
                <div id="note">
                    <div id="notetitle"></div>
                    <div id="notecontent">
                        <textarea rows="10" cols="100" id="notetextarea"></textarea>
                    </div>
                </div>
            </div>
            <div id="notefooter">Footer</div>
        </div>
    </div>

    <script type="text/javascript">$(function(){simpleRequest();});</script>
    <script type="text/javascript">$(function () { $('#selectable').selectable(); });</script>
</asp:Content>
