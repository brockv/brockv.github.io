# **Homework IV**

This weeks assignment was to write a simple, multi-page MVC web application that does not utilize a database. We were to demonstrate knowledge of GET, POST, and Request objects, as well as learn the basics of the Razor view engine by creating a "Miles to Metric Converter" page, and a "Color Mixer" page.

## **Relevant Links**
- [Home](https://brockv.github.io/)
- [Assignment Page](http://www.wou.edu/~morses/classes/cs46x/assignments/HW4_1819.html)
- [Code Repository](https://github.com/brockv/brockv.github.io/tree/master/CS460/HWK4/Homework4)

### **I: Setting Up the Project**

I began by creating a new, empty MVC project in Visual Studio, which gave me just the bare bones to get started. Then I created a Controller and a View associated with it to handle the Home page. Once the Home page was built, it was time to get started on the first of the required pages.


### **II: Creating the Converter Page**

To get this going, I added a method to the HomeController, and generated a View associated with it. This would be used for my Converter page. For this page we were instructed to use raw HTML only.

The first thing I added to the page was an input field for the user to enter the number of miles they'd like to convert, and a button for them to submit the value for conversion.

```html
<div>
    <!-- Create the input field, set a default a prompt, and limit input length -->
    <input id="userInput" type="text" name="miles_to_convert" pattern="^[0-9]\d*(\.\d+)?$" autocomplete="off"
           required="required" title="A positive number." placeholder="Enter a positive integer..." maxlength="27">
    <button id="btnConvert" class="btn btn-default">&raquo; Convert &laquo;</button>
</div>
```

The next thing I needed was a way for the user to select which metric unit they wanted to convert their value into. The obvious choice for this was radio buttons in a single group.

```html
<div id="radioButtons" class="box-container col-md-4 col-xs-12">
    <fieldset>
        <legend>Select unit to convert to:</legend>
        <div>
            <input type="radio" id="millimeters"
                   name="units" value="millimeters" checked />
            <label class="radioLabel" for="millimeters">Millimeters</label>
        </div>
        <div>
            <input type="radio" id="centimeters"
                   name="units" value="centimeters" />
            <label class="radioLabel" for="centimeters">Centimeters</label>
        </div>
        <div>
            <input type="radio" id="meters"
                   name="units" value="meters" />
            <label class="radioLabel" for="meters">Meters</label>
        </div>
        <div>
            <input type="radio" id="kilometers"
                   name="units" value="kilometers" />
            <label class="radioLabel" for="kilometers">Kilometers</label>
        </div>
    </fieldset>
</div>
```

The last thing needed for this page was a way to display the conversion results to the user. A simple label near the bottom of the page worked perfectly.

```html
<label id="conversionResults">@ViewBag.Message</label>
```

Now that the page had all the necessary elements, it was time to move back to the Controller to handle the calculations required for converting the user's input to the desired metric unit. Inside the Controller, I grabbed both the user's input and the selected radio button using the following:

```c#
string userInput = Request["miles_to_convert"];
string unit = Request["units"];
```

To do the conversion, I first attempted to parse the input to a double, only performing the calculations on a success. If the parse was successful, the conversion is done to the appropriate metric unit using a switch statement, and the results are passed out of the Controller to be displayed back on the page.

```c#
/* Attempt to parse the input we grabbed from the form */
if (Double.TryParse(userInput, out miles))
{
    /* If we parsed it successfully, do the appropriate conversion */
    switch (unit)
    {
        case "millimeters":                        
            miles = miles * Properties.Settings.Default.MILLIMETERS_PER_MILE; ;
            break;
        case "centimeters":
            miles = miles * Properties.Settings.Default.CENTIMETERS_PER_MILE;
            break;
        case "meters":
            miles = miles * Properties.Settings.Default.METERS_PER_MILE;
            break;
        case "kilometers":
            miles = miles * Properties.Settings.Default.KILOMETERS_PER_MILE;
            break;
        default:
            break;
    }

    /* Construct a message to display relating to the conversion */
    ViewBag.Message = (userInput + " mile(s) is equal to " + miles + " " + unit + ".");
}
```

### **III: Creating the Color Mixer Page**

As with the previous step, the first thing I needed to do was create a Controller that would handle the Color Mixer page. However, for this page we were to use Razor HTML helpers.

The first thing I needed was a place for the user to enter the color codes. I used the following to accomplish that:

```html
<!-- Prompt for the first color -->
<div>
    @Html.Label("First color:")
    @Html.TextBox("firstColor", "@Request["firstColor"]", new
{
   @class = "form-control",
   id = "firstColor",
   placeholder = "#AABBCC",
   name = "firstColor",
   pattern = "^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$",
   title = "HTML hexadecimal color code (#AABBCC)",
   required = "required",
   autocomplete = "off"
})
</div>
```

Doing it this way not only alerted the user that both fields were required, but also performed client-side validation, making sure the user's input matched the desired pattern (#ABC / #AABBCC).

Now that I had the inputs, I needed a button for the user to submit the colors, and a section to display all three colors (first color, second color, and the final color).

The following code snippet shows / hides the color boxes based on a bool that is set in the Controller:

```html
<!-- If the flag has been set to true in the controller, show the color boxes -->
if (ViewBag.ShowBoxes)
{
    <box class="colorBox" style="@ViewBag.FirstColorBox"></box>
    <label class="colorBoxLabel">+</label>
    <div class="colorBox" style="@ViewBag.SecondColorBox"></div>
    <label class="colorBoxLabel">=</label>
    <div class="colorBox" style="@ViewBag.FinalColorBox"></div>
}
```

With the page all set, I moved back to the Controller. After grabbing the color codes from the form, and a quick check to make sure neither value was null, I converted the strings into Color  objects so they could be added together.

```c#
/* Grab the color codes from the text boxes */
string firstColor = Request.Form["firstColor"];
string secondColor = Request.Form["secondColor"];

/* Make sure the fields aren't null before proceeding */
if (firstColor != null || secondColor != null)
{
    /* Convert the strings into Color objects so we can add them together */
    Color rgbColorOne = ColorTranslator.FromHtml(firstColor);
    Color rgbColorTwo = ColorTranslator.FromHtml(secondColor);
 
 ...
```

Once they were converted to Color objects, I broke them down into their individual channels and added them together. Once I had the values for each of the final color's channels, I subtracted 255 from any that were greater than 255. This effectively wrapped the values around from 0, allowing for more color combinations that didn't turn out all white.

```c#
/* Add each channel together from the two colors */
int finalColorRed = rgbColorOne.R + rgbColorTwo.R;
int finalColorGreen = rgbColorOne.G + rgbColorTwo.G;
int finalColorBlue = rgbColorOne.B + rgbColorTwo.B;

/* 
 * Subtract 255 from any values greater than 255, effectively 
 * wrapping around from 0. This allows for more color combinations
 * that don't result in plain white.
 */
if (finalColorRed > 255) finalColorRed -= 255;
if (finalColorGreen > 255) finalColorGreen -= 255;
if (finalColorBlue > 255) finalColorBlue -= 255;

/* Construct the mixture using the channels we just calculated */
string finalColor = ColorTranslator.ToHtml(Color.FromArgb(finalColorRed, finalColorGreen, finalColorBlue));
```

The last thing to do was send the colors back to the View for displaying the color boxes, and setting the bool flag.

```c#
/* Set the background information for each of the boxes */
ViewBag.FirstColorBox = "background: " + firstColor + ";";
ViewBag.SecondColorBox = "background: " + secondColor + ";";
ViewBag.FinalColorBox = "background: " + finalColor + ";";

/* Change the flag to show the boxes */
ViewBag.ShowBoxes = true;
```

#### **Additional Feature**

Just for fun, (and also because I got annoyed with having to switch windows to get color codes), I decided to add color pickers to this page. This allows the user to either manually enter color codes into the textboxes, or they can choose a color from the popup dialog and the color code will transfer to the appropriate textbox.

```html
<!-- Create a color picker under the first color box -->
<input type="color" onchange="setFirstColor(this)" onkeyup="setFirstColor(this)"
       class="btn btn-default" value="@Request["firstColor"]" style="width: 300px;">

<!-- Create a color picker under the second color box -->
<input type="color" onchange="setSecondColor(this)" onkeyup="setSecondColor(this)"
       class="btn btn-default" value="@Request["secondColor"]" style="width: 300px;">

<!-- Function calls to handle setting textbox values from the above color pickers. -->
<script>
    function setFirstColor(colorValue) {
        document.getElementById("firstColor").value = colorValue.value;
    }

    function setSecondColor(colorValue) {
        document.getElementById("secondColor").value = colorValue.value;
    }
</script>
```

### **IV: The Working Pages**

#### **Home Page**

![](images/homepage.PNG?raw=true)

#### **Miles to Metric Converter**

![](images/converter_one.PNG?raw=true)

![](images/converter_two.PNG?raw=true)


#### **Color Mixer**

![](images/colormixer_one.PNG?raw=true)

![](images/colormixer_two.PNG?raw=true)

![](images/colormixer_three.PNG?raw=true)

![](images/colormixer_four.PNG?raw=true)


### **V: Merging Into the Master Branch**

With everything working as intended, it was time to merge the feature branch into the master branch.

```bash
git checkout master
git merge HWK4-feature-branch
git push -u origin master
```
