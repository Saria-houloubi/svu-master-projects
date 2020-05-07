//
//Some main accross functions
//

//
//Shows an alert at the top of the page
//  type : the type of alert that its id shoud be "{type}Alert"
//  message : the message to set inside the alert
//  time : the time span to keep the alert showing for
//
function showAlert(type, message,time= 4000) {
    //Create the alert id
    var alertId = `${type}Alert`;
    //Get the alert element
    var alert = document.getElementById(alertId);
    //Set the message
    alert.innerText = message;
    //remove the class
    alert.classList.toggle('collapse');
    //Set the timeout to hide the message
    setTimeout(function () {
        alert.classList.toggle('collapse');
    }, time);
}