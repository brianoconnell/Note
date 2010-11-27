/// <reference path="jquery-1.4.1-vsdoc.js" />
var notes;


// Makes a simple request to retrieve all notes for the current user.
function onLoad() {
    jQuery.getJSON('getalljson', function (data) {
        notes = data.Notes;
        $.each(notes, function () {
            var listItem = $('<li>').append(this.Title).click(this.Id, function (event) {
                populateNoteDetails(event.data);
            });
            listItem.addClass('ui-widget-content');
            $('#selectable').append(listItem);
        });
    });
}

function populateNoteDetails(noteId) {
    $.each(notes, function () {
        if (this.Id === noteId) {
            $('#notetitle').empty().append(this.Title);
            $('#notetextarea').val(this.Content);
            $('#noteid').val(this.Id);
        }
    });
}

function postNoteUpdate() {
    var noteId = $('#noteid').val();
    var noteContent = $('#notetextarea').val();
    var noteTitle = $('#notetitle').text();
    var editNoteModel = {
        "model.Title": noteTitle,
        "model.Content": noteContent
    };
    
    jQuery.post('updatenotejson?noteId=' + noteId, editNoteModel, function (result) {
        if (result.Error) {
            // Display an error message somewhere.
        }
        else {
            $.each(notes, function () {
                if (this.Id == noteId) {
                    this.Title = noteTitle;
                    this.Content = noteContent;
                }
            });
        }
    }, 'json');
}