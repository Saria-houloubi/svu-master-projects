//The starting index to load the blogs
var startBlogIndex = 0;
//The count of blogs to get at each load
var countBlogs = 5;
var loadStartReqeust = 0;
var loadCountReqeust = 5;
//The base64 string encoding for the thumbnail
var thumbnailBase64 = '';
var thumbnailMimeType = '';
//Get the blogs table body
var blogsTableBody = document.getElementById('blogs_table_body_part');
//The id of the blog that is locked for edit
var editBlogId = '';
var requestTableBody = $("#hr_table_body")[0];
$(function () {
    //Start loading the first batch of the blogs
    loadMoreBlogs(undefined);
    loadMoreRequests(undefined);

    //Enable the tabs for the textareas
    var textAreas = document.getElementsByTagName('textarea');
    for (var i = 0; i < textAreas.length; i++) {
        enableTab(textAreas[i].id);
    }
});

//
//Removes any highlighted rows in table
//
function removeBlogEditHighlight() {
    //Remove higlight from any old filed
    var oldEditLockedRow = blogsTableBody.querySelector('.bg-warning');
    //Free up the row background
    if (oldEditLockedRow)
        oldEditLockedRow.classList.remove('bg-warning');
}

//
//loads more blogs based on the sent variables
//
function loadMoreBlogs(element) {
    if (element)
        changeButtonStatus(element, 'loading');

    loadBlogs(startBlogIndex, countBlogs, onLoadCallback);
}
//
//loads more blogs based on the sent variables
//
function loadMoreRequests(element) {
    if (element)
        changeButtonStatus(element, 'loading');

    loadHealthRequest(loadStartReqeust, loadCountReqeust, onRequestsLoadCallBack);
}


//
//Will be called once the load is done
//  showLoadMore : a flag to check if we need to show the load more button
//
function onRequestsLoadCallBack(showLoadMore) {
    loadStartReqeust = document.getElementsByClassName('hr-row').length;

    if (showLoadMore) {
        $('#load_more_request_btn')[0].classList.remove('collapse');
    } else {
        $('#load_more_request_btn')[0].classList.add('collapse');
    }

    changeButtonStatus($('#load_more_request_btn')[0], 'done');
}
//
//Will be called once the load is done
//  showLoadMore : a flag to check if we need to show the load more button
//
function onLoadCallback(showLoadMore) {
    startBlogIndex = document.getElementsByClassName('blog-row').length;

    if (showLoadMore) {
        $('#load_More_btn')[0].classList.remove('collapse');
    } else {
        $('#load_More_btn')[0].classList.add('collapse');
    }

    changeButtonStatus($('#load_More_btn')[0], 'done');
}

//
//Just a helper function to save the base64 string
//
function setImageEncoding(value, mimeType) {
    thumbnailBase64 = value;
    thumbnailMimeType = mimeType;
}
//
//Shortcut function to create a blog row
//
function createBlogTableRow(blog) {
    var row = createTableRow([blog.title, blog.previewContent, blog.visitCout, blog.creationDate, blog.lastUpdatedDate]);
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
    row.appendChild(createButtonIconTableCell('btn mt-1 text-white bg-secondary', 'fa fa-eye', null, `/awphealth/blog/${blog.id}`));

    row.classList.add('blog-row');
    return row;
}

//
//Create a heath request table row DOM element
//  healthRequest : the data to fill the row from
//
function CreateHealthRequestTableRow(healthRequest) {
    //Create the table row
    var row = createTableRow([healthRequest.subject, healthRequest.content, healthRequest.note, healthRequest.creationDate]);
    //Create the hidden id filed
    var hiddenId = document.createElement('input');
    hiddenId.hidden = true;
    hiddenId.setAttribute('value', healthRequest.id);
    hiddenId.classList.add('row-id');
    row.prepend(hiddenId);

    //Add the edit and replies button
    var replyCell = createButtonIconTableCell('btn mt-1 text-white bg-primary', 'fa fa-reply-all', null, "#request_replies_modal", 'modal', '#request_replies_modal');

    //get the anchor and add the wanted "data" attributes
    replyCell.getElementsByTagName('a')[0].setAttribute('data-requestId', healthRequest.id);

    row.appendChild(replyCell);
    //If the edit form is present
    if ($("#health_request_form")[0]) {
        //then add the edit button
        row.appendChild(createButtonIconTableCell('btn mt-1 text-white bg-primary', 'fa fa-edit', lockRequestForEdit));
    }

    row.classList.add('hr-row');
    return row;
}

//
//Loads the row data and Locks it for edit
//
function lockBlogForEdit() {
    removeBlogEditHighlight();
    //Get the row data
    var row = $(this).parentsUntil('tbody')[1];
    //Check that we got the right data
    if (row) {
        //Fill the values
        editBlogId = row.children[0].value;

        $.ajax('/awphealth/getblog',
            {
                method: 'GET',
                data: {
                    id: editBlogId
                },
                success: function (data) {

                    $('#blog_title').val(data.title);
                    $('#blog_preview_text').val(data.previewContent);
                    $('#blog_content').val(data.content);
                    $('#blog_note').val(data.note);

                    row.classList.add('bg-warning');
                    //Enable the edit button
                    document.getElementById('edit_blog_data_button').disabled = false;

                    window.location = "#blog_create_part";
                },
                error: function (err) {
                    showAlert('danger', err.responseJSON.message);
                    scrollWindowToTheTop();
                }
            });
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
function AddEditBlog(element, isAdd) {

    changeButtonStatus(element, 'loading');
    $.ajax('/AWPHealth/Blog',
        {
            method: 'POST',
            data: {
                id: isAdd ? '00000000-0000-0000-0000-000000000000' : editBlogId,
                Title: $('#blog_title').val(),
                Content: $('#blog_content').val(),
                Note: $('#blog_note').val(),
                PreviewContent: $('#blog_preview_text').val(),
                ThumbnailBase64: thumbnailBase64,
                ThumbnailMimeType: thumbnailMimeType
            },
            success: function (blog) {
                showAlert('success', "Operation Success!");
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

                clearUpForm();
            },
            error: function (err) {
                showAlert('error', err.responseJSON.message);
            }
        }).always(function () {
            changeButtonStatus(element, 'done');
        });
}

//
//Loads the blogs table
//  start: used for pagination
//  count : get a count number of blogs after the start index
//
function loadBlogs(start, count, onLoadCallback) {
    $.ajax('/AWPHealth/GetBlogs',
        {
            method: 'GET',
            data: {
                start,
                count
            },
            success: function (data) {
                //if no blogs found then show message
                if (data.length === 0 && start === 0) {
                    document.getElementById('blog_countent_status_noContent').classList.remove('collapse');
                }
                for (var i = 0; i < data.length; i++) {
                    blogsTableBody.appendChild(createBlogTableRow(data[i]));
                }

                onLoadCallback(data.length === count);
            },
            error: function (err) {
                showAlert('error', err.responseJSON.message);
                onLoadCallback(false);
            }
        }).done(function () {
            document.getElementById('blog_countent_status_loading').classList.add('collapse');

        });
}

//
//starts loading the blog data
//  start : the starting point to load from
//  count : the count of blogs  to load
//
function loadHealthRequest(start, count, onLoadCallback) {
    $.ajax('/AWPHealth/GetHealthRequests',
        {
            method: 'GET',
            data: {
                start,
                count
            },
            success: function (data) {
                if (data.length === 0 && start === 0) {
                    document.getElementById('health_request_countent_status_noContent').classList.remove('collapse');
                } else {

                    //Check if we found the data
                    if (requestTableBody) {
                        //Loop through the items and add them
                        for (var i = 0; i < data.length; i++) {
                            requestTableBody.appendChild(CreateHealthRequestTableRow(data[i]));
                        }
                    }
                }
                onLoadCallback(data.length === count);
            },
            error: function (err) {
                showAlert('danger', err.responseJSON.message);
                onLoadCallback(false);
            }
        }).done(function () {
            document.getElementById('health_request_countent_status_loading').classList.add('collapse');
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
//
//Clears up the form filed
//
function clearUpForm() {
    //Free up controles
    $('#blog_title').val('');
    $('#blog_content').val('');
    $('#blog_note').val('');
    $('#blog_preview_text').val('');
    $('#blog_thumbnail_file').val('');

    $(".thumbnail_status").addClass('collapse');
}