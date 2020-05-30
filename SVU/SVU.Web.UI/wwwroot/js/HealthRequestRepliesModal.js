//The id of the request to send the reply for
var requestIdForReplies;
var noRepliesImage = document.getElementById('no-replies-image');
var requestRepliesPart = document.getElementById("request_replies");

//
//Called once the user starts typing in
//
function onTypingContent(element) {

    //Check if the value is not empty
    if (element.value === '') {
        $('#send_request_reply_btn').addClass('disabled');
    } else {
        $('#send_request_reply_btn').removeClass('disabled');
    }

}

//
//Bind an action when the modal shows
//
$("#request_replies_modal").on("show.bs.modal", function (event) {
    $(".request-reply-message").remove();
    //get the button the triggerd the evnet
    var button = $(event.relatedTarget);
    //Get the request id
    requestIdForReplies = button.data('requestid');
    //Get the data for the request
    $.ajax(`/awphealth/GetHealthRequest/${requestIdForReplies}`, {
        method: 'GET',
        success: function (data) {
            $("#request_subject_modal")[0].innerText = data.subject;
            $("#request_content_modal")[0].innerText = data.content;

            //Check if we have any replies
            if (data.replies.length !== 0) {
                noRepliesImage.classList.add('collapse');
                for (var i = 0; i < data.replies.length; i++) {
                    requestRepliesPart.appendChild(createRequestReply(data.replies[i].content, data.replies[i].isAway))
                }
            }
            else {
                noRepliesImage.classList.remove('collapse');
            }
        },
        error: function (err) {
            showAlert('error', err.responseText)
        }
    })
})

//
//Send the reply on the displayed request
//
function sendRequestReply() {

    //Set the button as busy
    changeButtonStatus(this, 'loading');

    $.ajax('/awphealth/HealthRequestReply', {
        method: 'POST',
        contentType: "application/json",
        data: JSON.stringify({
            requestId: requestIdForReplies,
            content: $("#request_reply_content").val()
        }),
        success: function (data) {
            //Hide the no replay image
            if (!noRepliesImage.classList.contains('collapse'))
                noRepliesImage.classList.add('collapse');
            //Add the reply
            document.getElementById("request_replies").appendChild(createRequestReply(data.content, false));

            $("#request_reply_content").val('')
        },
        error: function (err) {
            showAlert('error', err.responseText)
        }
    }).done(function () {
        changeButtonStatus(this, 'done');
    })

}

//
//Create a reply chat div
//  content : the content of the message
//  isAway : if the replay is from any other side than ours
//
function createRequestReply(content, isAway) {
    //Create the dom element
    var reply = document.createElement('span');
    //Add the content
    reply.innerText = content;

    reply.classList.add('request-reply-message')
    //Add the needed class if the message is from other orgins
    if (isAway) {
        reply.classList.add('reply-away');
    }

    return reply;
}