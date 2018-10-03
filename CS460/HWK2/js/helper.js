// Needed to stop the script from freaking out about getting information from the document
/*eslint-env browser*/

// Initialize variables used for maintaining the to-do list
var toDoList      = document.querySelector("ul"),
    item          = document.getElementsByTagName("li"),
    userInput     = document.getElementById("userInput"),
    addItemButton = document.getElementById("btnAddItem");

// Helper function that gets the length of the user's input
function userInputLength() {
    "use strict";
    return userInput.value.length;
}

<<<<<<< HEAD
// Helper function that gets the length of the to-do list
function toDoListLength() {
=======
// Helper function that gets the length of a given list item
function newItemLength() {
>>>>>>> ef9210f3d03e5e0716f4c01b79f459f2df188b7d
    "use strict";
    return item.length;
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
    toDoList.appendChild(li);
    
    // Reset the text field for the user's convenience
    userInput.value = "";
    
    // Add the 'checked' class to newly created items
	function markCompleted() {
		li.classList.toggle("checked");
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

// Event handler for when the user clicks the button
function addItemAfterClick() {
    "use strict";
	if (newItemLength() > 0) {
		addNewItem();
	}
}

// Event handler for when the user presses 'Enter'
function addItemAfterKeypress(event) {
    "use strict";
	if (userInputLength() > 0 && event.which === 13) {
		addNewItem();
	}
}

// Add an event listener for when the user clicks on the button
addItemButton.addEventListener("click", addItemAfterClick);

// Add an event listener for when the user presses 'Enter'
userInput.addEventListener("keypress", addItemAfterKeypress);
