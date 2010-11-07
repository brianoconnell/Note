/// <reference path="jquery-1.4.1-vsdoc.js" />

// Makes a simple request to retriece all notes for the current user.
function simpleRequest() {
    jQuery.getJSON('getalljson', function (data) {
        $.each(data.Notes, function () {
            var newDiv = $("<li>").append(this.Title).click(this, function (event) {
                $('#notetitle').empty().append(event.data.Title);
                $('#notetextarea').val(event.data.Content);
            });
            newDiv.addClass('ui-widget-content');
            $('#selectable').append(newDiv);
        });
    });
}