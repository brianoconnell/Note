/// <reference path="jquery-1.4.1-vsdoc.js" />
function simpleRequest() {
    jQuery.getJSON('getalljson', function (data) {
        $.each(data.Notes, function () {
            var newDiv = $("<div>").append(this.Title).click(this, function (event) {
                $('#title').empty().append(event.data.Title);
                $('#content').empty().append(event.data.Content);
            });
            $('#noteslist').append(newDiv);
        });
    });
}