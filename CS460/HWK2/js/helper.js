// Grab the list from the form
var toDoNodelist = document.getElementsByTagName("LI");

// Loop through each item in the list, appending the "close" button to each entry
var i;
for (i = 0; i < toDoNodelist.length; i++) {
    var span = document.createElement("SPAN");
    var txt = document.createTextNode("\u00D7");
    span.className = "close";
    span.appendChild(txt);
    toDoNodelist[i].appendChild(span);
}

// Figure out which "close" button the user pressed
var itemClosed = document.getElementsByClassName("close");

// Hide the list item associated with that "close" button
for (i = 0; i < itemClosed.length; i++) {
    itemClosed[i].onclick = function() {
        var div = this.parentElement;
        div.style.display = "none";
    }
}




