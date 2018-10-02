/*eslint-env browser*/

// Initialize variables used for maintaining the to do list
var toDoList      = document.querySelector("ul"),
    item          = document.getElementsByTagName("li"),
    userInput     = document.getElementById("userInput"),
    addItemButton = document.getElementById("btnAddItem");

// Helper function that gets the length of the user's input
function userInputLength() {
    "use strict";
    return userInput.value.length;
}

// Helper function that gets the length of the to do list
function toDoListLength() {
    "use strict";
    return item.length;
}

// Function that handles actions related to the to do list
function addNewItem() {
    "use strict";
    // Create a new list item
    var li            = document.createElement("li"),
        btnDeleteItem = document.createElement("button");
    
    // Assign the user's input to the new list item
    li.appendChild(document.createTextNode(userInput.value));
    
    // Append the new list item to the to do list
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
	// END ADD DELETE BUTTON

}
