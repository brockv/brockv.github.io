/*eslint-env browser*/
/*jslint browser: true*/
/*global $ */

// Initialize variables used for maintaining the to-do list
//var toDoList      = document.querySelector("ul"),
var    userInput     = document.getElementById("userInput");

// Helper function that gets the length of the user's input
function userInputLength() {
    "use strict";
    return userInput.value.length;
}

// Function that handles actions related to the to-do list
function addNewItem() {
    "use strict";
    // Create a new list item
    var li            = document.createElement("li"),
        btnDeleteItem = document.createElement("button");
    
    // Assign the user's input to the new list item
    li.appendChild(document.createTextNode(userInput.value));
    
    // Append the new list item to the to-do list
    $("ul").append(li);
    //toDoList.appendChild(li);
    
    // Reset the text field for the user's convenience
    $("#userInput").val('');
    
    // Add the 'completed' class to newly created items
	function markCompleted() {
		li.classList.toggle("completed");
	}

	li.addEventListener("click", markCompleted);
    
    // Add the 'delete' class to newly created items
	function deleteListItem() {
		li.classList.add("delete");
	}
    
    // Add the delete button to newly created items
	btnDeleteItem.appendChild(document.createTextNode("X"));
	li.appendChild(btnDeleteItem);
	btnDeleteItem.addEventListener("click", deleteListItem);

}

// Called once the page is loaded
function main() {
    "use strict";
    // Add an event handler for when the user clicks the button
    $("#btnAddItem").on('click', function () {
        if (userInputLength() > 0) {
            addNewItem();
        }
    });
    
    // Add an event handler for when the user presses 'Enter'
    $("#userInput").keypress(function () {
        if (userInputLength() > 0 && event.which === 13) {
            addNewItem();
        }
    });
}

// Call the main function once the page is loaded
$(document).ready(main);

