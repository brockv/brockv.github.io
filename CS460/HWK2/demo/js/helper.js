/*eslint-env browser*/
/*jslint browser: true*/
/*global $ */

// Grab the input field from the form for later use
var userInput = document.getElementById("userInput");

// Helper function that gets the length of the user's input
function userInputLength() {
    "use strict";
    return userInput.value.length;
}

// Function that handles actions related to the to-do list
function addNewItem() {
    "use strict";
    // Create a new list item and a button to delete it
    var li            = document.createElement("li"),
        btnDeleteItem = document.createElement("button");
    
    // Assign the user's input to the new list item
    $(li).append(document.createTextNode(userInput.value));
    
    // Append the new list item to the to-do list
    $("ul").append(li);
    
    // Reset the text field for the user's convenience
    $("#userInput").val('');
    
    // Add the 'completed' class to newly created items
	function markCompleted() {
		li.classList.toggle("completed");
	}

    // Add an event listener to each newly created item for toggling completion
    $(li).click(function () {
        markCompleted();
    });
    
    // Add the 'delete' class to newly created items
	function deleteListItem() {
		li.classList.add("delete");
	}
    
    // Add the delete button to newly created items, and bind the event listener for it
    $(btnDeleteItem).append(document.createTextNode("X"));
	$(li).append(btnDeleteItem);
    $(btnDeleteItem).click(function () {
        deleteListItem();
    });

}

// Called once the page is loaded
function main() {
    "use strict";
    // Called when the user clicks the button to add an item
    $("#btnAddItem").on('click', function () {
        if (userInputLength() > 0) {
            addNewItem();
        }
    });
    
    // Called when the user presses 'Enter' to add an item
    $("#userInput").keypress(function () {
        if (userInputLength() > 0 && event.which === 13) {
            addNewItem();
        }
    });
}

// Call main() once the page is fully loaded
$(document).ready(main);

