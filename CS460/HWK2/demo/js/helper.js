/*eslint-env browser*/
/*jslint browser: true*/
/*global $*/


/* GENERATE NUMBER AND APPEND TO A PREFIX */
/* ASSIGN THAT TO NEW LIST ITEMS AS THE ID */

var listItemPrefix = "li_";
var listItemID = 0;

///////////////////////////////////////////////////////
//                HELPER FUNCTIONS                   //
///////////////////////////////////////////////////////
/**
  * Grab the length of the user's input
  */
function userInputLength() {
    /* Enabled to shut the editor up */
    "use strict";
    
    /* Return the length of the input */
    return $("#userInput").val().length;
}

///////////////////////////////////////////////////////
//               TOGGLE ITEM START                   //
///////////////////////////////////////////////////////
/**
  * Called when the user left-clicks on an item in their list
  */
function toggleCompleted(li) {
    /* Enabled to shut the editor up */
    "use strict";

    /* Toggle the visuals for incomplete / completed items */
    li.classList.toggle("completed");

    /* Move the item to the appropriate list */
    if ($(li).hasClass("completed")) {
        $(li).appendTo("#completedTasks");
    } else {
        $(li).appendTo("#incompleteTasks");
    }
}
///////////////////////////////////////////////////////
//               TOGGLE ITEM END                     //
///////////////////////////////////////////////////////


///////////////////////////////////////////////////////
//                  ADD ITEM START                   //
///////////////////////////////////////////////////////
/**
  * Called when the user adds an item to their list 
  */
function addNewItem() {
    /* Enabled to shut the editor up */
    "use strict";
    
    /* Create a new list item and a button to delete it */
    var li            = document.createElement("li"),
        btnDeleteItem = document.createElement("button");
    
    /* Assign the text from the input to the new list item */
    $(li).append(document.createTextNode($("#userInput").val()));
    
    /* Append the new list item to the to-do list */
    //$("#incompleteTasks").append(li);
    
    /* Reset the text field and give it focus for the user's convenience */
    $("#userInput").val("");
    $("#userInput").focus();
    
    li.classList.add("context-menu-one");
    $(li).attr("id", listItemPrefix + listItemID);
    listItemID = listItemID + 1;
    
    /* Add an event listener to each newly created item for toggling completion */
    $(li).click(function () {
        toggleCompleted(li);
    });
    
    /* Add the 'delete' class to newly created items */
	function deleteListItem() {
		li.classList.add("delete");
	}
    
    /* Add the delete button to newly created items, and bind the event listener for it */
    $(btnDeleteItem).append(document.createTextNode("X"));
	$(li).append(btnDeleteItem);
    $(btnDeleteItem).click(function () {
        deleteListItem();
    });
    
    /* Append the new list item to the to-do list */
    $("#incompleteTasks").append(li);

}
///////////////////////////////////////////////////////
//                  ADD ITEM END                     //
///////////////////////////////////////////////////////


///////////////////////////////////////////////////////
//               CONTEXT MENU START                  //
///////////////////////////////////////////////////////

/**
  * Show a custom context menu when the user right clicks on an item
  * in the "To-DO" portion of their list.
  */
$("body").on("contextmenu", "#incompleteTasks li", function (event) {
    
    // Avoid the real one
    event.preventDefault();
    
    // Show contextmenu
    $("#contextMenu").finish().toggle(100).
    
    // In the right position (the mouse)
    css({
        top: event.pageY + "px",
        left: event.pageX + "px"
    });
});


// If the document is clicked somewhere
$(document).bind("mousedown", function (e) {
    
    // If the clicked element is not the menu
    if (!$(e.target).parents("#contextMenu").length > 0) {
        
        // Hide it
        $("#contextMenu").hide(100);
    }
});


// If the menu element is clicked
$("#contextMenu item").click(function(){
    
    // This is the triggered action name
    switch($(this).attr("data-action")) {
        
        // A case for each action. Your actions here
        case "first": $("#userInput").val("IT WORKED");; break;
        case "second": alert("second"); break;
        case "third": alert("third"); break;
    }
  
    // Hide it AFTER the action was triggered
    $("#contextMenu").hide(100);
});
///////////////////////////////////////////////////////
//               CONTEXT MENU END                    //
///////////////////////////////////////////////////////


///////////////////////////////////////////////////////
//                      MAIN                         //
///////////////////////////////////////////////////////
function main() {
    /* Enabled to shut the editor up */
    "use strict";
    
    /* Called when the user clicks the button to add an item */
    $("#btnAddItem").on('click', function () {
        if (userInputLength() > 0) {
            addNewItem();
        }
    });
    
    /* Called when the user presses 'Enter' to add an item */
    $("#userInput").keypress(function () {
        if (userInputLength() > 0 && event.which === 13) {
            addNewItem();
        }
    });
}

/* Call main() once the page is fully loaded */
$(document).ready(main);

