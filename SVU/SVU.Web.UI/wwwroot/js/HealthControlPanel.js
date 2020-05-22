//The starting index to load the blogs
var startBlogIndex = 0;
//The count of blogs to get at each load
var countBlogs = 5;
//The base64 string encoding for the thumbnail
var thumbnailBase64 = '';
var thumbnailMimeType = '';
//Get the blogs table body
var blogsTableBody = document.getElementById('blogs_table_body_part');
//The id of the blog that is locked for edit
var editBlogId = '';
$(function () {
    //Start loading the first batch of the blogs
    loadBlogs(startBlogIndex, countBlogs);

    //Enable the tabs for the textareas
    var textAreas = document.getElementsByTagName('textarea');
    for (var i = 0; i < textAreas.length; i++) {
        enableTab(textAreas[i].id);
    }
})
//
//Just a helper function to save the base64 string
//
function setImageEncoding(value,mimeType) {
    thumbnailBase64 = value;
    thumbnailMimeType = mimeType;
}
//
//Shortcut function to create a blog row
//
function createBlogTableRow(blog) {
    var row = createTableRow([blog.title, blog.content, blog.note, blog.visitCout, blog.creationDate, blog.lastUpdatedDate]);
    //Create the hidden id filed
    var hiddenId = document.createElement('input');
    hiddenId.hidden = true;
    hiddenId.setAttribute('value', blog.id);
    hiddenId.classList.add('row-id');
    row.prepend(hiddenId);
    row.appendChild(createButtonIconTableCell(`btn mt-1 text-white bg-${blog.hasThumbnail ? 'success' : 'danger'} disabled`, `fa fa-image text-white`));
    //Add the edit ,delete  and preview button
    row.appendChild(createButtonIconTableCell('btn mt-1 text-white bg-primary', 'fa fa-edit', lockBlogForEdit));
    row.appendChild(createButtonIconTableCell('btn mt-1 text-white bg-danger', 'fa fa-trash-alt', showDeleteConfirmation));
    row.appendChild(createButtonIconTableCell('btn mt-1 text-white bg-secondary', 'fa fa-eye', null,`/awphealth/blog/${blog.id}`));

    return row;
}

//
//Loads the row data and Locks it for edit
//
function lockBlogForEdit() {
    //Get the last editing row
    var oldEditLockedRow = blogsTableBody.querySelector('.bg-warning');
    //Free up the row background
    if (oldEditLockedRow)
        oldEditLockedRow.classList.remove('bg-warning');
    //Get the row data
    var row = $(this).parentsUntil('tbody')[1];
    //Check that we got the right data
    if (row) {
        //Fill the values
        editBlogId = row.children[0].value;
        $('#blog_title').val(row.children[1].innerText);
        $('#blog_content').val(row.children[2].innerText);
        $('#blog_note').val(row.children[3].innerText);

        row.classList.add('bg-warning');
        //Enable the edit button
        document.getElementById('edit_blog_data_button').disabled = false;
    }
    else {
        showAlert('error', 'something wrong happend while trying to parse the record');
        scrollWindowToTheTop();
    }
}

//
//Adds or edits a blog
//  isAdd : a flag to check if the operation is an add or edit
//
function AddEditBlog(element,isAdd) {
    //Disable the button
    $(element).attr('disabled', true);
    //Show the spinner
    $(element).children('svg').removeClass('collapse');
    $.ajax('/AWPHealth/Blog',
        {
            method: 'POST',
            data: {
                id: isAdd ? '00000000-0000-0000-0000-000000000000' : editBlogId,
                Title: $('#blog_title').val(),
                Content: $('#blog_content').val(),
                Note: $('#blog_note').val(),
                ThumbnailBase64 : thumbnailBase64,
                ThumbnailMimeType: thumbnailMimeType,
            },
            success: function (blog) {
                showAlert('success', "Blog added!");
                //Craete the  new blog row
                var newRow = createBlogTableRow(blog);
                //Mark the new row
                newRow.classList.toggle('bg-success');
                //Remove the mark after 3 secondd
                setTimeout(function myfunction() {
                    newRow.classList.toggle('bg-success');
                }, 3000);
                //If the opeation is an edit
                if (!isAdd) {
                    //Get the old row
                    var editingRow = document.querySelector('tr.bg-warning');

                    //insert the row
                    blogsTableBody.insertBefore(newRow, editingRow);

                    //remove the old row
                    blogsTableBody.removeChild(editingRow);
                } else {
                    //Add it to the list
                    blogsTableBody.prepend(newRow);
                }
            },
            error: function (err) {
                showAlert('error', err.responseJSON.message);
            }
        }).always(function () {
            //Enable the button
            $(element).attr('disabled', false);
            //hide the spinner
            $(element).children('svg').addClass('collapse');

        });
}

//
//Loads the blogs table
//  start: used for pagination
//  count : get a count number of blogs after the start index
//
function loadBlogs(start, count) {
    $.ajax('/AWPHealth/Blogs',
        {
            method: 'GET',
            data: {
                start,
                count
            },
            success: function (data) {
                //if no blogs found then show message
                if (data.length === 0) {
                    document.getElementById('blog_countent_status_noContent').classList.remove('collapse');
                }
                for (var i = 0; i < data.length; i++) {
                    blogsTableBody.appendChild(createBlogTableRow(data[i]));
                }
                //Just move the start to the end of the count
                startBlogIndex += data.length;
            },
            error: function (err) {
                showAlert('error', err.responseJSON.message);
            }
        }).done(function () {
            document.getElementById('blog_countent_status_loading').classList.add('collapse');

        });
}
var rowToDelete;
//
//Shows the delete confirmation modal
//
function showDeleteConfirmation() {
    //Get the row of the button
    rowToDelete = $(this).parentsUntil('tbody')[1];
    
    $("#delete_confirmation_modal input").val(rowToDelete.children[1].innerText);
    $("#delete_confirmation_modal").modal('show');

}
//
//Send a delete request for the blog 
//  id : the id of the blog to delete
//
function deleteBlog() {
    //Hide the modal
    $("#delete_confirmation_modal").modal('hide');
    //Get the row of the button
    var row = rowToDelete;
    //The first child is always the id
    var id = row.children[0].value;
    //TOOD: Add delete confirmation
    //sedn the delete request
    $.ajax(`/AWPHealth/DeleteBlog/${id}`, {
        method: 'DELETE',
        success: function (res) {
            showAlert('success', 'Deleted!');
            //Disable the edit button if this was the row that we where editing
            if (row.classList.contains('bg-warning')) {
                document.getElementById('edit_blog_data_button').disabled = true;
            }
            //Remove the row
            row.remove();

        }, error: function (err) {
            showAlert('error', err.responseJSON.message);
        }
    });
}