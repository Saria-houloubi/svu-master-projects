//
//Some main accross functions
//

//
//Shows an alert at the top of the page
//  type : the type of alert that its id shoud be "{type}Alert"
//  message : the message to set inside the alert
//  time : the time span to keep the alert showing for
//
function showAlert(type, message, time = 4000) {
    //if it is an error then scroll the user to the top of the page
    if (type === 'error') {
        scrollWindowToTheTop();
    }
    //Create the alert id
    var alertId = `${type}Alert`;
    //Get the alert element
    var alert = document.getElementById(alertId);
    //Set the message
    alert.innerText = message;
    //remove the class
    alert.classList.toggle('collapse');
    //If the time was 0 keep it open
    if (time !== 0) {
        //Set the timeout to hide the message
        setTimeout(function () {
            alert.classList.toggle('collapse');
        }, time);
    }
}

//
//Hides an alert at the top of the page
//  type : the type of alert that its id shoud be "{type}Alert"
//
function hideAlert(type, message, time = 4000) {
    //Create the alert id
    var alertId = `${type}Alert`;
    //Get the alert element
    var alert = document.getElementById(alertId);
    //remove the class
    alert.classList.add('collapse');
}


//
//Reads one the file to base64 that is choosen using the input element
//  acceptTags : the file tags that are accepted to upload
//  inputElement : the DOM input file type element
//  loaderStatusId : a spinning loader to show to the user
//
function ReadOneImageFile(inputElement, acceptTags, loaderStatusId,setLoadValueCallBack) {
    //Try to get the loader
    var loader = document.getElementById(loaderStatusId);

    //If no file selected
    if (!inputElement.files.length > 0) {

        //Hide back all icons
        conventionLoaderChange(loader, -1);
        return;
    }

    //Create the file reader
    var reader = new FileReader();
    reader.onloadstart = function (event) {
        //Show the spinner
        conventionLoaderChange(loader, 0);
        //Get the name splited by the .
        var splitedFileName = inputElement.files[0].name.split('.');
        //check if the file type is from the accepted type
        if (!acceptTags.includes(splitedFileName[splitedFileName.length - 1])) {
            //Show error
            conventionLoaderChange(loader, 2);
            //Clear out the valu
            $(inputElement).val('');

            setLoadValueCallBack('');
            //Abort the loading
            this.abort();
        }

    };
    //Once the reader is readey
    reader.onload = function (event) {
        //Show success icon
        conventionLoaderChange(loader, 1);

        //Return the base64 encoding
        setLoadValueCallBack(btoa(reader.result));
    };

    reader.error = function (event) {
        //Show error icon
        conventionLoaderChange(loader, 2);

        setLoadValueCallBack('');
    };

    //read the file
    reader.readAsBinaryString(inputElement.files[0]);
}

//
//A custom method to work with our loader
// toggels teh loader spiinner => success or error icons
//
function conventionLoaderChange(loaderElement, childIndexToShow) {
    //Check if it undefined
    if (loaderElement) {

        loaderElement.querySelector(':not(.collapse)').classList.toggle('collapse');

        if (childIndexToShow >= 0)
            loaderElement.children[childIndexToShow].classList.remove('collapse');
    }
}

//
//Takse the user to the top of the  page
//
function scrollWindowToTheTop() {
    window.scrollTo(0, 0);
}

//
//Enable tabs in textareas
// id : the id of the input or textarea
//
function enableTab(id) {
    var el = document.getElementById(id);
    el.onkeydown = function (e) {
        if (e.keyCode === 9) { // tab was pressed

            // get caret position/selection
            var val = this.value,
                start = this.selectionStart,
                end = this.selectionEnd;

            // set textarea value to: text before caret + tab + text after caret
            this.value = val.substring(0, start) + '\t' + val.substring(end);

            // put caret at right position again
            this.selectionStart = this.selectionEnd = start + 1;

            // prevent the focus lose
            return false;

        }
    };
}

//
//Creates a table cell based with an icon button
//
function createButtonIconTableCell(buttonClassList, iconClassList, onClickCallback) {

    //Create the cell
    var cell = document.createElement('td');
    //Add the button to the cell
    cell.appendChild(createButtonIconElement(buttonClassList, iconClassList, onClickCallback));

    return cell;

}
//
//Creates a button icon element
//  buttonClasslist : a string with the button class space seprated
//  iconClassList : a string with the icon class space seprated
//  onClickCallBack : the callback function to be binded with the button
//
//
function createButtonIconElement(buttonClassList, iconClassList, onClickCallback) {
    //Create the button DOM element
    var button = document.createElement('a');
    //Add the needed class
    button.classList.add(...buttonClassList.split(' '));
    //create the element
    var icon = document.createElement('i');
    //Add the class
    icon.classList.add(...iconClassList.split(' '));
    //Add the icon
    button.appendChild(icon);
    //bind the onclick
    button.onclick = onClickCallback;
    return button;
}
//
//Creates row and fills it with the send data
//  data : a string array of the fileds values
//
function createTableRow(data) {
    //Create the row DOM element
    var row = document.createElement('tr');
    //Loop throug the data
    for (var i = 0; i < data.length; i++) {
        //Create the cell
        var cell = document.createElement('td');
        //Fill the data
        cell.textContent = data[i];
        //Add it to the row
        row.appendChild(cell);
    }

    return row;
}
