# **Homework II**

For this assignment we were instructed to create something interesting using JavaScript and jQuery, as well as demonstrate the skills we learned from the first assignment using HTML, CSS, and Bootstrap. Similar to the first assignment, were to use HTML to build the pages, CSS to style them, and Bootstrap to format the layout. This time though, JavaScript and jQuery were used for the front-end of what we created. I had no experience with either of those, so I decided to focus on a project that was interesting, but reasonable enough to meet the requirements in the time allotted.

## **Relevant Links**
- [Home](https://brockv.github.io/)
- [Assignment Page](http://www.wou.edu/~morses/classes/cs46x/assignments/HW2.html)
- [Code Repository](https://github.com/brockv/brockv.github.io/tree/master/CS460/HWK2/demo)
- [Site Demo](https://brockv.github.io/CS460/HWK2/demo/index.html)

### **I: Creating a Feature Branch**

One of the major parts of this assignment was to demonstrate and expand our knowledge of branching in repositories. One of the goals was to create a feature branch off of the main branch, and develop the assignment within it, only merging the two back together at the end after thorough testing. So the first thing I did was set up my directories, like so:

```bash
mkdir HWK2
mkdir HWK2/css
mkdir HWK2/js
cd HWK2
```

The next step was to create a new branch and switch over to it. This can be done a few ways, but I chose to use the following command as it takes care of a few steps at the same time:

```bash
git checkout -b HWK2
```

This created the branch 'HWK2' and checked it out in one step. That moved me out of the master branch and into the new one, which allowed me to work without affecting anything in the master branch. All that was left was to create some empty files I would require for this assignment, and push them to the repository.

```bash
touch index.html
touch css/style.css
touch js/helper.js
git add .
git commit -m "Initial commit."
git push -u origin HWK2
```

### **II: Deciding on a Project**

For me, this was honestly the most difficult part of this assignment. In the past I often made the mistake of choosing a project idea that was too difficult, or that required more time than I had. Having no experience in some of the major aspects of this assignment made it even more important that I choose carefully.

After looking at a few ideas, and comparing the amount of work required for each, I finally settled on a sort of to-do list. It's relatively simple, but it could be made interesting enough through its presentation. More importantly, it met all of the requirements!

### **III: Creating a Rough Draft**

Now that I had an idea, it was time to decide what it should look like. Having some sketches before you sit down to create it drastically reduces the amount of time you'll spend on trivial details. I went through a few designs, but eventually went with a simple layout and decided I would make it interesting through the styling.

This was the basic idea. An input field for the user to enter items, a button to add the item to their to-do list, with the list elements underneath. The initial design had each list element with it's own checkbox for the user mark them as complete, but this eventually changed.

![](images/form_items.png?raw=true)

In order to make completed items stand out I decided to add a strikethrough effect to the text of a list element once the user marked it as complete. In the styling phase I also applied a different color to really make it stand out from tasks that still needed to be completed. This is also the step where I removed the checkboxes in favor of a button that would allow the user to remove items from their to-do list.

![](images/form_updated.png?raw=true)

This is an example of the form with an item marked as complete by the user. The text is given a strikethrough effect and the item is given a dull background color to show it as inactive in order to make it stand out from incomplete tasks.

![](images/form_item_completed.png?raw=true)


### **IV: Creating and Styling the Page**

Now that I had an idea of how I wanted the page to look, it was time to begin coding. I started with the HTML and CSS files to get the page layout started and looking nice. There wasn't much to do for the HTML portion, as the layout was fairly simple. 

First, I needed a title and some instructions for the user:

```html
<!-- Create a container to hold all of the form elements -->
<div class="container">
    <div class="row">
        <!-- Display the page header -->
        <div class="intro col-12">
            <h1>Task Tracker 2.0</h1>
            <div>
                <div class="border1"></div>
                <div c></div>
            </div>
        </div>
    </div>

    <!-- Move into a row -->
    <div class="row">
        <!-- Display helper text -->
        <div class="helpText col-12">
            <p id="firstHelpText">Easily keep track of tasks by adding them to your to-do list.</p>
            <p id="secondHelpText">Clicking on an item will mark it as complete.</p>
            <p>Clicking on the "X" will remove an item item from your to-do list.</p>
        </div>
    </div>
</div>
```

 Next up was an input for the user to type items into, and a button to them to submit new items:
 
```html
<div class="row">
    <div class="col-12">
        <!-- Create the input field, set a default a prompt, and limit input length -->
        <input id="userInput" type="text" placeholder="Enter an item..." maxlength="27">
        <button id="btnAddItem"><img src="images/pencil.PNG"></button>
    </div>
</div>
```

The last thing I needed was the list itself, which started empty:

```html
<!-- Create an empty list for the user to add items to -->
<div class="row">
    <div class="listItems col-12">
        <ul class="col-12 offset-0 col-sm-8 offset-sm-2">
        </ul>
    </div>
</div>
```

Once I had those completed, I moved on to the CSS to start styling the page. The look of the page went through a lot of changes before I finally settled on the colors and theme. I didn't want this to be all business and no fun, so I went with a softer look. Rounding the corners of the input, buttons, and list items helped to achieve this, as well as using lighter colors on a nice gray background.

```css
/* Set the border, width, and padding for the user input field */
input
{
    border-radius: 5px;
    min-width: 65%;
    padding: 5px;
    border-style: solid;
}

/* Style the button to add items to the list */
#btnAddItem
{
    cursor: pointer;
    padding: 5px 15px;
    border-radius: 4px;
    border-color: #000000;
    border-style: solid;
    color: #605d60;
    background-color: #515151;
    transition: all 0.75s ease;
    -webkit-transition: all 0.75s ease;
    -moz-transition: all 0.75s ease;
    -ms-transition: all 0.75s ease;
    -o-transition: all 0.75 ease;
    font-weight: normal;
}

/* Add a hover effect to the add items button */
#btnAddItem:hover
{
    background-color: #c972c9;
}
```

The list is easily the most important piece of the page, at least visually, so I wanted to make sure it stood out while also fitting the theme I had going. This consisted of making sure the corners of all the list elements were rounded in the same style as the input and button, and using a nice purple to make them stand out from the rest of the form. While testing this I realized the user could select the items in the list, which I didn't want, so I eventually added the code at the bottom the the 'li' section below, preventing that from happening.

```css
/* Set the alignment and margin for the list */
ul
{
    text-align: left;
    margin-top: 20px;
}

/* Style the individual list items */
li
{
    cursor: pointer;
    list-style: none;
    padding: 10px 20px;
    color: #000000;
    text-transform: capitalize;
    font-weight: 600;
    border: 2px solid #000000;
    border-radius: 5px;
    margin-bottom: 10px;
    background: #c972c9;
    transition: all 0.75s ease;
    -webkit-transition: all 0.5s ease;
    -moz-transition: all 0.5s ease;
    -ms-transition: all 0.5s ease;
    -o-transition: all 0.5 ease;
    
    /* Make the list items unselectable */
    -webkit-user-select: none;
    -moz-user-select: none;
    -ms-user-select: none;
    user-select: none;
}
```

So far, so good! The next step was to style the 'delete' button each newly created list item gets, and a color change when the user hovered over a list item, just for the visual that it's highlighted.

```css
/* Add a hover effect to the individual list items */
li:hover
{
	background: #aa5aaa;
}

/* Style the delete item button assigned to each list item */
li > button
{
    font-weight: normal;
    background: none;
    border: none;
    float: right;
    color: #000000;
    font-weight: 800;
    cursor: pointer;
}
```

All that was left was a way to show a list item as completed, and to "remove" deleted items (which really just consisted of hiding them).

```css
/* Set list items to have a strikethrough when marked completed by the user*/
.completed
{
    background: #888 !important;
    color: #ddd;
    text-decoration: line-through;
}

/* Hide individual list items deleted by the user */
.delete
{
    display: none;
}
```

### **IV: Writing the JavaScript and jQuery**

One of the goals of this assignment was to alter the page somehow in response to user interaction, and we were to use JavaScript and jQuery to accomplish this. There were a few ways of doing this with the current design. Allowing the user to add new items to their list either by clicking the button or pressing 'Enter' was the obvious one. There was also allowing the user to mark an item as complete, and also deleting items from the list.

In order to let the user add items to the list, I first needed to setup the event handlers and listeners for both the button and the keypress.

```js
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
```
Next was making it so the user could mark items as complete, and showing that visually. To do this I toggled a class from my style sheet (shown above) on the item. The class changes the color and applies a strikethrough effect to the text in order to make it very obivous that something has changed.

```js
/* Add the 'completed' class to newly created items */
function markCompleted() {
    li.classList.toggle("completed");
    }

/* Add an event listener to each newly created item for toggling completion */
$(li).click(function () {
    markCompleted();
});
```

The last thing to do was provide the user a way to delete items from their list. I didn't want extra stuff such as buttons cluttering the page, so I decided to place the 'delete' option directly on the list items. This resulted in a much cleaner look, and makes it very apparent what the button will do.

```js
/* Add the delete button to newly created items, and bind the event listener for it */
$(btnDeleteItem).append(document.createTextNode("X"));
$(li).append(btnDeleteItem);
$(btnDeleteItem).click(function () {
    deleteListItem();
});
```

### **V: Making Sure Everything Works**

With everything in place and the individual parts tested, it was time to make sure it all played nice together and functioned properly. I loaded the HTML file, successfully created some items for my to-do list, and was able to both mark them as completed and delete them with no problems. Success!

#### **The final result:**

![](images/create_items.gif?raw=true)

![](images/mark_complete.gif?raw=true)

![](images/delete_items.gif?raw=true)

### **VI: Merging Back Into the Master Branch**

The last step in all of this was to merge the feature branch back into the main branch. In order to do that, I first needed to switch back to the master branch, then merge the feature branch into it. After a final commit, I used the following commands to accomplish that:

```bash
git checkout master
git merge HWK2
git push -u origin master
```

# **Addition Feature Revisions**

A little while after completing the assignment and submitting it, I started to feel like there was more I could have done with the webpage. So I pulled it up and started to think.

### **I: Separating the Completed Tasks From the Incomplete Tasks**

Right off the bat I realized that there wasn't enough of a difference between completed and incomplete tasks. The color difference was nice, but a large enough list could become an unorganized mess very quickly. The first thing to do was create a separate list to hold completed tasks. Then, I gave each section a title so there would be no confusion as to which list was for what.

```html
<!-- Create an empty list for the user to add items to -->
<div class="row">
    <div class="listItems col-12">
        <figure>
            <figcaption>To-Do:<hr/></figcaption>
            <ul id="incompleteTasks" class="col-12 offset-0 col-sm-8 offset-sm-2">
            </ul>
        </figure>
    </div>
</div>

<!-- Create an empty list for completed tasks to be moved to -->
<div class="row">
    <div class="listItems col-12">
        <figure>
            <figcaption>Completed:<hr/></figcaption>
            <ul id="completedTasks" class="col-12 offset-0 col-sm-8 offset-sm-2">
            </ul>
        </figure>
    </div>
</div>
```

Next up was moving the tasks between lists. This required a refactoring of the existing code that marked tasks as completed. This:

```js
/* Add the 'completed' class to newly created items */
function markCompleted() {
    li.classList.toggle("completed");
    }

/* Add an event listener to each newly created item for toggling completion */
$(li).click(function () {
    markCompleted();
});
```
Became this:

```js
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

/* Add an event listener to each newly created item for toggling completion */
$(li).click(function () {
    toggleCompleted(li);
});
```

This allowed for tasks to move between the lists when the user clicked on them when marking them complete / incomplete.

### **II: Creating and Displaying a Custom Context Menu**

Another feature I wanted to add was a custom context menu. This was beyond the requirements of the assignment, and was done purely as a learning experience. I did a lot of reading on sites such as [Stack Exchange](https://stackoverflow.com/), [W3Schools](https://www.w3schools.com), and poked around in the documentation [here](https://api.jquery.com/contextmenu/) as well. At first it seemed like everyone was using some version of a plugin (such as [this one](https://swisnl.github.io/jQuery-contextMenu/)) to implement and manage their context menus. While that would have been convenient, it would have defeated the purpose of this learning exercise, and that's not what I wanted at all. I dug through tutorial after tutorial, and every forum thread I could find, and once I had a decent understanding of the mechanics I got to work. The first thing I needed to do was show the custom context menu when right-clicking on a list item...

```js
/**
  * Show a custom context menu when the user right clicks on an item
  * in the "To-DO" portion of their list.
  */
$("body").on("contextmenu", "li", function (event) {
    /* Enabled to shut the editor up */
    "use strict";
    
    /* Prevent the default context menu from being used */
    event.preventDefault();
    
    /* Fade the context menu in */
    $("#contextMenu").finish().toggle(100).
        /* Make sure it is by the mouse */
        css({
            top: event.pageY + "px",
            left: event.pageX + "px"
        });
});
```
... and to hide it when clicking outside of it.

```js
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
```

Next, I needed a way to perform an action when a menu item is clicked on.

```js
/*
 * Handle context menu actions
 */
$("#contextMenu item").click(function (e) {
    /* Enabled to shut the editor up */
    "use strict";

    /* Perform the appropriate action */
    switch ($(this).attr("action")) {
    /* Edit the list item */
    case "editTask":
        /* Only allow editing of incomplete tasks. No cheating! */
        var temp = document.getElementById("editTaskOption");
        if (temp.getAttribute("state") !== "disabled") {
            /* Enable editing of the selected list item and give it focus */
            $(selectedListItem).attr('contenteditable', 'true');
            $(selectedListItem).focus();

            /* Disable editing if the current list item loses focus */
            $(selectedListItem).focusout(function () {
                $(selectedListItem).attr('contenteditable', 'false');
                /* If there's no text when it loses focus, delete it */
                if ($(selectedListItem).text() === "") {
                    $(selectedListItem).addClass("delete");
                }
            });

            /* Disable editing if the user presses 'Enter' */
            $(selectedListItem).keypress(function () {
                if (event.which === 13) {
                    $(selectedListItem).attr('contenteditable', 'false');
                    /* If there's no text when the key is pressed, delete it */
                    if ($(selectedListItem).text() === "") {
                        $(selectedListItem).addClass("delete");
                    }
                }
            });
        }
        break;
    /* Delete the list item */
    case "deleteTask":
        if ($(this).attr("state") !== "disabled") {
            $(selectedListItem).addClass("delete");
        }
        break;
    /* Do nothing -- Close the context menu */
    default:
        break;
    }
  
    /* Hide it AFTER the action was triggered */
    $("#contextMenu").hide(100);
});
```

This uses a switch statement to determine which action should be performed. The two options I currently have in my context menu are 'Edit Task' and 'Delete Task', and both are handled in this function. This, however, had a flaw in that it would effect every instance of li on the page instead of just the one that was clicked on. In order to resolve that I needed a way to assign id's to the list items as they were being created. To accomplish that I needed a counter.

```js
/* Initialize variables for generating id's for dynamically created list items */
var listItemPrefix = "li_";
var listItemID = 0;
```

And to use it to generate the id for every new item added to the user's list.
```js
/* Assign an id to each newly created list item */
li.classList.add("context-menu-one");
$(li).attr("id", listItemPrefix + listItemID);
listItemID = listItemID + 1;
```

Once I had a way to assign id's to every new list item. I had a way to manipulate them. First, I grabbed the id of the list item the user right-clicked, and prepend a '#' to make match the syntax later on.

```js
/* Get the id of the list item the user clicked on */
selectedListItem = "#" + $(this).attr("id");
```

Using that I was able to allow the user to edit an existing task:
```js
/* Enable editing of the selected list item and give it focus */
$(selectedListItem).attr('contenteditable', 'true');
$(selectedListItem).focus();

/* Disable editing if the current list item loses focus */
$(selectedListItem).focusout(function () {
    $(selectedListItem).attr('contenteditable', 'false');
    /* If there's no text when it loses focus, delete it */
    if ($(selectedListItem).text() === "") {
        $(selectedListItem).addClass("delete");
    }
});

/* Disable editing if the user presses 'Enter' */
$(selectedListItem).keypress(function () {
    if (event.which === 13) {
        $(selectedListItem).attr('contenteditable', 'false');
        /* If there's no text when the key is pressed, delete it */
        if ($(selectedListItem).text() === "") {
            $(selectedListItem).addClass("delete");
        }
    }
});
```
And also allow them to delete individual items in their lists:
```js
$(selectedListItem).addClass("delete");
```

### **III: Styling the Context Menu**

I added the following to my style sheet to fit the context menu into the theme of my page, and to help indicate to the user which items were enabled / disabled.

```css
/* Overall styling of the items */
item
{
    font-family:'Arial', sans-serif;
    font-weight: bold;
    font-size: 13px;
    text-align: left;
    line-height: 12px;
    display: block;
    padding-left: 20px;
    padding-right: 20px;
    padding-top: 10px;
    padding-bottom: 10px;
}

/* Gray out disabled items */
item[state*="disabled"] {
    color: #AAA;
}

/* Change the cursor to a point over enabled menu items */
item:not([state*="disabled"]):hover
{
    cursor: pointer;
}

/* Change the cursor, text color, and background color when over enabled menu items*/
item:not([state*="disabled"]):hover
{
    color: #000000;
    background-color: #51DF70;
    cursor: pointer;
}
```

### **IV: The New Working Page**

In the end, the result was better than I had expected. It wasn't anything fancy, but it worked and I learned a lot in the process, which was the goal. So I was very pleased with the results.

![](images/new_create_items.gif?raw=true)

![](images/new_edit_items.gif?raw=true)

![](images/new_delete_items.gif?raw=true)

