/*eslint-env browser*/
/*jslint browser: true*/
/*global $*/


/* GENERATE NUMBER AND APPEND TO A PREFIX */
/* ASSIGN THAT TO NEW LIST ITEMS AS THE ID */

/* Initialize variables for generating id's for dynamically created list items */
var listItemPrefix = "li_";
var listItemID = 0;

/* Initialize a variable for storing clicked list items */
var selectedListItem = "";

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
    var li            = document.createElement("li");
    
    /* Assign the text from the input to the new list item */
    $(li).append(document.createTextNode($("#userInput").val()));
    
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
    /* Enabled to shut the editor up */
    "use strict";
    
    /* Prevent the default context menu from being used */
    event.preventDefault();
    
    /* Get the id of the list item the user clicked on */
    selectedListItem = "#" + $(this).attr("id");
    
    /* Fade the context menu in */
    $("#contextMenu").finish().toggle(100).
        /* Make sure it is by the mouse */
        css({
            top: event.pageY + "px",
            left: event.pageX + "px"
        });
});


/*
 * Hide the context menu if the use clicks off of it
 */
$(document).bind("mousedown", function (e) {
    /* Enabled to shut the editor up */
    "use strict";
    
    /* If the clicked element is not the menu */
    if (!$(e.target).parents("#contextMenu").length > 0) {
        
        /* Hide it */
        $("#contextMenu").hide(100);
    }
});

/*
 * Handle context menu actions
 */
$("#contextMenu item").click(function (e) {
    /* Enabled to shut the editor up */
    "use strict";

    /* This is the triggered action name */
    switch ($(this).attr("action")) {
    /* A case for each action. Your actions here */
    case "editTask":
        /* Enable editing of the selected list item and give it focus */
        $(selectedListItem).attr('contenteditable', 'true');
        $(selectedListItem).focus();
            
        /* Disable editing if the current list item loses focus */
        $(selectedListItem).focusout(function () {
            $(selectedListItem).attr('contenteditable', 'false');
            if ($(selectedListItem).text() === "") {
                $(selectedListItem).addClass("delete");
            }
        });
            
        /* Disable editing if the user presses 'Enter' */
        $(selectedListItem).keypress(function () {
            if (event.which === 13) {
                $(selectedListItem).attr('contenteditable', 'false');
                if ($(selectedListItem).text() === "") {
                    $(selectedListItem).addClass("delete");
                }
            }
        });
        break;
    case "deleteTask":
        $(selectedListItem).addClass("delete");
        break;
    default:
        break;
    }
  
    /* Hide it AFTER the action was triggered */
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

