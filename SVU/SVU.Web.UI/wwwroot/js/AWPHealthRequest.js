var count = 10;
var loadStart = 0;
//Get the table body
var requestTableBody = $("#hr_table_body")[0];
$(function () {
    loadHealthRequest(loadStart, count, onLoadCallBack);
})

//
//Removes any highlighted rows in table
//
function removeBlogEditHighlight() {
    //Remove higlight from any old filed
    var oldEditLockedRow = requestTableBody.querySelector('.bg-warning');
    //Free up the row background
    if (oldEditLockedRow)
        oldEditLockedRow.classList.remove('bg-warning');
}

//
// clears the request form filed
//
function clearFormFields() {
    removeBlogEditHighlight();
    //Get the input fileds
    var inputFields = document.getElementById('health_request_form').querySelectorAll('.form-control');
    //Hide the edit button
    document.getElementById('edit_hr_btn').classList.add('collapse');
    //Clear the input filed
    for (var i = 0; i < inputFields.length; i++) {
        inputFields[i].value = '';
    }
}
//
//loads more blogs based on the sent variables
//
function loadMoreRequests(element) {
    if (element)
        changeButtonStatus(element, 'loading');

    loadHealthRequest(loadStart, count, onLoadCallBack);
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

                //Check if we found the data
                if (requestTableBody) {
                    //Loop through the items and add them
                    for (var i = 0; i < data.length; i++) {
                        requestTableBody.appendChild(CreateHealthRequestTableRow(data[i]));
                    }
                }
                onLoadCallback(data.length === count);
            },
            error: function (err) {
                showAlert('danger', err.responseJSON.message);
                onLoadCallback(false);
            }
        });
}


//
//Will be called once the load is done
//  showLoadMore : a flag to check if we need to show the load more button
//
function onLoadCallBack(showLoadMore) {
    loadStart = document.getElementsByClassName('hr-row').length;

    if (showLoadMore) {
        $('#load_more_request_btn')[0].classList.remove('collapse');
    } else {
        $('#load_more_request_btn')[0].classList.add('collapse');
    }

    changeButtonStatus($('#load_more_request_btn')[0], 'done');
}

//
//Locks the request element for edit
//
function lockRequestForEdit() {
    document.getElementById('edit_hr_btn').classList.remove('collapse');

    removeBlogEditHighlight();
    //Get the row data
    var row = $(this).parentsUntil('tbody')[1];
    //Check that we got the right data
    if (row) {
        //Fill the values
        $("#request_id").val(row.children[0].value);
        //Select all the td fileds in the row
        var tds = row.querySelectorAll('td');
        //Fill the wanted info
        $("#Subject").val(tds[0].innerText);
        $("#Content").val(tds[1].innerText);
        $("#Note").val(tds[2].innerText);

        row.classList.add('bg-warning');
    }
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
        //row.appendChild(createButtonIconTableCell('btn mt-1 text-white bg-danger', 'fa fa-trash-alt', showDeleteConfirmation));
    }

    row.classList.add('hr-row');
    return row;
}