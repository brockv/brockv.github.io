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

This created the branch 'HWK2' and checked it out in one step. That moved me out of the master branch and into the new one, which allowed me to work without disturbing the contents of master. All that was left was to create some empty files I would require for this assignment, and push them to the repository.

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

![](images/form_items?raw=true)

In order to make completed items stand out I decided to add a strikethrough effect to the text of a list element once the user marked it as complete. In the styling phase I also applied a different color to really make it stand out from tasks that still needed to be completed. This is also the step where I removed the checkboxes in favor of a button that would allow the user to remove items from their to-do list.

![](images/form_updated?raw=true)

This is an example of the form with an item marked as complete by the user. The text is given a strikethrough effect and the item is given a dull background color to show it as inactive in order to make it stand out from incomplete tasks.

![](images/form_item_completed?raw=true)


### **IV: Creating the Content**

