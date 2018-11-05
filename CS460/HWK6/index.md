# **Homework VI**

For this assignment, we were expected to write an MVC web application that used portions of a large, complex pre-existing database. We needed to be able to derive C# models from that database, and use LINQ queries to obtain the required information for each section. This assignment also called for strictly using strongly-typed views using Razor.

## **Relevant Links**
- [Home](https://brockv.github.io/)
- [Assignment Page](http://www.wou.edu/~morses/classes/cs46x/assignments/HW6_1819.html)
- [Code Repository](https://github.com/brockv/brockv.github.io/tree/master/CS460/HWK6/Homework6)
- [Demo Video](https://www.youtube.com/watch?v=2Yn21oedRm0&feature=youtu.be)

### **I: Restoring the Database**

Initially, this was the most confusing part of this assignment. I had never used SQL Server Management Studio, so the entire process was more difficult than it needed to be. After several hours of frustration, and some additional tutorials, I was able to successfully restore the database and get it ready to add to my project.

![](images/restored_database.PNG?raw=true)

The database itself is very large, and as you can see in the above picture, contains numerous tables and views. A handful of these tables were "archive" tables however, and we're not necessary. For this assignment we didn't need any of the views, and we only needed the non-archive versions of the tables, so those are the ones I added to my project when I connected the database. 

### **II: Connecting the Database to the Application**

In order to connect the database to my project, I added a new ADO.NET Entity Data Model, giving it an appropriate name, then selected "Code First from Database". From there, I added a new connection, specifying the server name I used with SQL Server Management Studio in order to access the restored database file. The final step was selecting which tables to bring into the project. The end result was a single DbContext file and a total of 31 model classes, which is exactly what we wanted.

![](images/ado_entity_data.PNG?raw=true)

![](images/code_first.PNG?raw=true)

![](images/add_connection.PNG?raw=true)


### **III: Creating the First Feature: People Search**

The first feature we were expected to implement was the ability to search the database for a list of names that matched a user's input provided through a search bar. The list of names were expected to appear on the same page, somewhere below the search bar. Clicking on one of the search results should result in the user being taken to a separate page where they could view information belonging to that person. That information needed to include the searched person's full name, their preferred name, phone number, fax number, email address, the date they became a customer / employee, and the photo associated with their account.

To get this started, I first added a search bar and a button to go along with it.

```html
@using (Html.BeginForm("Home"))
{
    @Html.AntiForgeryToken()

    <div class="row searchArea">

        <div class="col-12">
            <!-- Create the search bar and search button -->
            @Html.TextBox("searchString", @Request["searchString"], new
       {
           @class = "form-control",
           id = "searchBar",
           placeholder = "Search by client name...",
           type = "search"
       })
            <input id="btnSearch" class="btn btn-default submitted" type="submit" value="Search" />
        </div>
    </div>
}
```

I then created an area to display any results found from the user's search. Each result would be created as an actionlink represented as a button, and displayed in a table for easy viewing.

```html
<div class="tableParent" style="max-width: 50vw;">

    <!-- Check if we should display anything and if the search bar was empty -->
    @if (@ViewBag.ShowSearchResults && @Request["searchString"] != "")
    {
        <div>
            <!-- Show the appropriate message -->
            <strong>@ViewBag.DisplayMessage</strong>
        </div>

        <!-- If the model isnt null, we have stuff to display to the user -->
        if (Model != null)
        {
            <table>
                <!-- Create a link to more details for each entry found in the search -->
                @foreach (var client in Model)
                {
                    <tr>
                        <td>
                            @Html.ActionLink(
                            @client.FullName + " (" + @client.PreferredName + ")",
                            "ViewDetails",
                            "People",
                            new { id = @client.PersonID },
                            new { @class = "btn btn-default tableChild" })
                        </td>
                    </tr>
                }
            </table>
        }
    }
</div>
```

After the results were displayed to the user, they needed a way to view details for each entry when clicked on. To accomplish this, I created a new view that would hold this information, which was called from a separate controller. In the method below, an instance of my ViewModel (explained in the second feature section)is created, and a Person object is initialized using the ID passed in from the view. If the ID is bad, resulting in VMPerson being null, the user is redirected to a view with an error message. If the ID is good, the ViewModel is passed to the view where the details are displayed, and the user is sent there instead.

```c#
/* Create an instance of the database for doing initial lookups */
private readonly WideWorldImportersContext db = new WideWorldImportersContext();

/// <summary>
/// Builds a view to display information about a given person, using their ID.
/// </summary>
/// <param name="id">The ID of the person</param>
/// <returns>A view displaying information about the given person.</returns>
public ActionResult ViewDetails(int id)
{
    WWIViewModel model = new WWIViewModel
    {
        /* See if there's an entry with the id that was passed in */
        VMPerson = db.People.Find(id)
    };

    /* If no entry was found, send the user to the 'NotFound' view */
    if (model.VMPerson == null)
    {
        return View("NotFound");
    }
    /* Redirect to the 'ViewDetails' view */
    return View("ViewDetails", model);
}
```

In order to neatly organize and display the information necessary, I used the table shown below. With that complete, it was time to move on to the second feature.

```html
<!-- Display information belonging to this person -->
<div class="row" style="border: 2px solid #ffffff; margin-bottom: 20px;">
    <h2 style="margin-left: 30px;"><strong>@Html.DisplayFor(model => model.VMPerson.FullName)</strong></h2>
    <hr />
    <div class="column">
        <dl class="dl-horizontal" style="margin-left: 20px;">
            <!-- Preferred name -->
            <dt>
                @Html.Label("Preferred Name:")
            </dt>
            <dd>
                @Html.DisplayFor(model => model.VMPerson.PreferredName)
            </dd>

            <!-- Phone number -->
            <dt>
                @Html.Label("Phone Number:")
            </dt>
            <dd>
                @Html.DisplayFor(model => model.VMPerson.PhoneNumber)
            </dd>

            <!-- Fax number -->
            <dt>
                @Html.Label("Fax Number:")
            </dt>
            <dd>
                @Html.DisplayFor(model => model.VMPerson.FaxNumber)
            </dd>

            <!-- Email address -->
            <dt>
                @Html.Label("Email Address:")
            </dt>
            <dd>
                <a href="mailto:@(Model.VMPerson.EmailAddress)">@(Model.VMPerson.EmailAddress)</a>
            </dd>

            <!-- Date joined -->
            <dt>
                @Html.Label("Date Joined:")
            </dt>
            <dd>
                @(Model.VMPerson.ValidFrom.ToShortDateString())
            </dd>
        </dl>
    </div>

    <!-- Display the photo for this entry -->
    <div class="column">
        <img src="https://via.placeholder.com/300" style="margin-bottom: 20px; margin-left: 40px; width: 300px; height: 300px;" />
    </div>
</div>
```

### **IV: Creating the Second Feature: Customer Sales Dashboard**

For this feature, the goal was to display additional information about a person if they were the primary contact person for a company. Otherwise, only show what was required in the first feature. This additional information included their company profile, a summary of their purchase history, and their top 10 most profitable items purchased (from Wide World Importers). Also, for extra credit we were given the option to pinpoint the companies location on a map.

Getting all of this information was going to require the use of multiple models, and since you are only allowed to pass in a single model to any given view, that's where the above mentioned ViewModel comes in. A ViewModel allows you to combine multiple models into a single source, giving you the ability to access all of the included models inside the view it's passed to.

After creating a subfolder in my Models directory, I created a new class, and added the information shown below. The top three entries are the models I would require to accomplish this feature, and the bottom four were the variables needed to store specific information.

```c#
/// <summary>
/// ViewModel constructed for use in the 'ViewDetails' view.
/// </summary>
public class WWIViewModel
{
    /* Initialize the models we'll need */
    public Person VMPerson { get; set; }
    public Customer VMCustomer { get; set; }
    public List<InvoiceLine> VMInvoices { get; set; }

    /* Initialize the variables we'll need */
    public int VMCustomerID { get; set; } = 0;
    public bool VMIsPrimaryContactPerson { get; set; } = false;
    public decimal VMGrossSales { get; set; } = 0;
    public decimal VMGrossProfits { get; set; } = 0;
}
```

Getting this feature up and running was going to require some major changes to my existing method, "ViewDetails". Instead of immediately redirecting to the details page after confirming the person's ID was valid, additional things needed to be checked and calculated.


The first thing to check for, was if this person was associated as the primary contact person for a company, through the Customers2 navigational property. If they were, that information was pulled from the database and assigned to VMCustomer.

```c#
WWIViewModel model = new WWIViewModel
{
    /* See if there's an entry with the id that was passed in */
    VMPerson = db.People.Find(id)
};

/* If no entry was found, send the user to the 'NotFound' view */
if (model.VMPerson == null)
{
    return View("NotFound");
}

/* Check if this person is a PrimaryContactPerson for a company */
if (model.VMPerson.Customers2.Count() > 0)
{
    /* Set the bool flag to true */
    model.VMIsPrimaryContactPerson = true;

    /* Grab the data for this customer */
    model.VMCustomerID = model.VMPerson.Customers2.FirstOrDefault().CustomerID;
    model.VMCustomer = db.Customers.Find(model.VMCustomerID);
```

The next step was to calculate the gross sales and profit of their company. The following queries did this perfectly.

```c#
/* Calculate the gross sales for this customer */
model.VMGrossSales = model.VMCustomer.Orders.SelectMany(x => x.Invoices)
    .SelectMany(x => x.InvoiceLines)
    .Sum(x => x.ExtendedPrice);

/* Calculate the gross profit for this customer*/
model.VMGrossProfits = model.VMCustomer.Orders.SelectMany(x => x.Invoices)
    .SelectMany(x => x.InvoiceLines)
    .Sum(x => x.LineProfit);
```

Finally, the following query is used to find the top 10 most profitable items purchased by this company from Wide World Importers.

```c#
/* Grab the top 10 purchases by this customer, in descending order */
model.VMInvoices = model.VMCustomer.Orders.SelectMany(x => x.Invoices)
    .SelectMany(x => x.InvoiceLines)
    .OrderByDescending(x => x.LineProfit)
    .Take(10)
    .ToList();
```

As with the basic information from the first feature, this additional information is displayed neatly in tables.

#### **Company Profile:**
```html
<!-- Company Profile -->
<div class="row" style="border: 2px solid #ffffff; margin-bottom: 20px;">
    <h2 style="margin-left: 30px;"><strong>Company Profile</strong></h2>
    <hr />
    <div class="column">
        <dl class="dl-horizontal" style="margin-left: 20px;">
            <!-- Company name -->
            <dt>
                @Html.Label("Company:")
            </dt>
            <dd>
                @Html.DisplayFor(model => model.VMCustomer.CustomerName)
            </dd>

            <!-- Phone number -->
            <dt>
                @Html.Label("Phone Number:")
            </dt>
            <dd>
                @Html.DisplayFor(model => model.VMCustomer.PhoneNumber)
            </dd>

            <!-- Fax number -->
            <dt>
                @Html.Label("Fax Number:")
            </dt>
            <dd>
                @Html.DisplayFor(model => model.VMCustomer.FaxNumber)
            </dd>

            <!-- Website URL -->
            <dt>
                @Html.Label("Website:")
            </dt>
            <dd>
                <a href="@(Model.VMCustomer.WebsiteURL)">@Model.VMCustomer.WebsiteURL</a>
            </dd>

            <!-- Member since -->
            <dt>
                @Html.Label("Member Since:")
            </dt>
            <dd>
                @(Model.VMCustomer.ValidFrom.ToShortDateString())
            </dd>

        </dl>

    </div>

    <!-- ~~~~START OF EXTRA CREDIT SECTION~~~~ -->
    <!-- Display a map of the companies location -->
    <div class="column">
        <div id="mapID"></div>
        <script>
            var locationLat = @Model.VMCustomer.DeliveryLocation.Latitude.Value;
            var locationLng = @Model.VMCustomer.DeliveryLocation.Longitude.Value;
            var map = L.map("mapID").setView([locationLat, locationLng], 10);

            L.tileLayer('https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token={accessToken}', {
                attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
                maxZoom: 18,
                id: "mapbox.streets",
                accessToken: "@ViewBag.DefinitelyNotASecretKey"
            }).addTo(map);

            var contentString = '<div id="content" style="text-align: center; color: #000000">'+
                '<p><strong>@(Model.VMCustomer.CustomerName)</strong><br>' +
                '@(Model.VMCustomer.City.CityName), @(Model.VMCustomer.City.StateProvince.StateProvinceCode)</p>';
            var marker = L.marker([locationLat, locationLng]).addTo(map)
                .bindPopup(contentString).openPopup();
        </script>
    </div>
    <!-- ~~~~END OF EXTRA CREDIT SECTION~~~~ -->

</div>
```

#### **Purchase History Summary:**
```html
<!-- Purchase History Summary -->
<div class="row" style="border: 2px solid #ffffff; margin-bottom: 20px;">
    <h2 style="margin-left: 30px;"><strong>Purchase History Summary</strong></h2>
    <hr />
    <div class="column">
        <dl class="dl-horizontal">
            <dt>
                @Html.Label("Orders:")
            </dt>

            <dd>
                @Html.DisplayFor(model => model.VMCustomer.Orders.Count)
            </dd>

            <dt>
                @Html.Label("Gross Sales:")
            </dt>

            <dd>
                @string.Format("{0:C}", Model.VMGrossSales)
            </dd>

            <dt>
                @Html.Label("Gross Profits:")
            </dt>

            <dd>
                @string.Format("{0:C}", Model.VMGrossProfits)
            </dd>

        </dl>

    </div>

</div>
```

#### **Items Purchased (10 Highest by Profit):**
```html
<!-- Items Purchased (10 highest by profit) -->
<div class="row" style="border: 2px solid #ffffff; margin-bottom: 20px;">
    <h2 style="margin-left: 30px;"><strong>Items Purchased</strong> (10 Highest by Profit)</h2>
    <!-- Table headers -->
    <table class="table itemsPurchased">
        <tr>
            <th>
                @Html.Label("Stock Item ID")
            </th>

            <th>
                @Html.Label("Description")
            </th>

            <th>
                @Html.Label("Line Profit")
            </th>

            <th>
                @Html.Label("Sales Person")
            </th>
        </tr>

        <!-- Table rows -->
        @foreach (var item in Model.VMInvoices)
        {
            <tr>
                <td>
                    @Html.DisplayFor(modelItem => item.StockItemID)
                </td>

                <td>
                    @Html.DisplayFor(modelItem => item.Description)
                </td>

                <td>
                    @(string.Format("{0:C}", item.LineProfit))
                </td>

                <td>
                    @Html.DisplayFor(modelItem => item.Invoice.Person4.FullName)
                </td>
            </tr>
        }

    </table>
</div>
```

The last thing I added to this page was a link at the bottom that would return the user to the main search page. The method I used preserved the state of the previous page, so that any search results were still present, similar to when pressing the browser's back button.

```html
<!-- Create a link back to the main page for easy navigation -->
<div>
    <p>
        <!-- Used this method to preserve search results upon returning to the search page -->
        <a href="javascript:history.back()">Back to Search</a>
    </p>
</div>
```

### **V: The Working Pages**

#### **Search Page:**

![](images/search_page.PNG?raw=true)

![](images/search_page_results.PNG?raw=true)

![](images/search_page_no_results.PNG?raw=true)

#### **View Details Page:**

![](images/details_page_basic_information.PNG?raw=true)

![](images/details_page_additional_information_one.PNG?raw=true)

![](images/details_page_additional_information_two.PNG?raw=true)


### **VI: Extra Feature: Pagination**

While testing my application I often encountered searches that had 20+ results. Scrolling through them all became tedious, so I wanted an easier way to navigate the results. The answer to this problem was pagination, and luckily for me, was fairly easy to implement.

I started by installing the PagedList.Mvc package through the NuGet Package Manager. This gave me access to its libraries in my application. Next, I had to make some changes to the method for my search page.

```c#
public ActionResult Home(string searchString, string currentSearch, int? page)
{
    /* Determine how to handle this request based on the search string */
    if (searchString != null)
    {
        /* Reset the page to 1 if the search string changed */
        page = 1;

        /* Switch the flag that controls the table visibility */
        ViewBag.ShowSearchResults = true;
    }
    else
    {
        /* Update the search string to what's in the filter */
        searchString = currentSearch;
    }
    
    /* Store the search so we can use it between page views */
    ViewBag.CurrentSearch = searchString;
```

In addition to the searchString being passed in, I now also had currentFilter, which is used to store a user's search string between pages, and page, which is the page the user is attempting to view. Then, based on the value of searchString, I determined whether to reset the page back to 1 as a result of the string changing, or to update it to the value stored in currentFilter. After these actions, the search string is stored so that search results are preserved between pages.

I then had to change how I was returning the information from the controller to the view. The two variables, pageSize and pageNumber, control how many results are shown per page, and which page to return to the user, respectfully. Before sending the results back to the view, they first needed to be ordered by some property (I chose FullName here, but it could have been anything related to the data), and then converted to a paged list.

```c#
int pageSize = 10;
int pageNumber = (page ?? 1);

/* Return the view with the search results */
return View(searchResults.OrderBy(x => x.FullName).ToPagedList(pageNumber, pageSize));
```

The last thing to do was actually add the pagination to the view. The following code snippet demonstrates how this was done.

```html
<!-- START PAGINATION SECTION -->
<div>
    <!-- Display the current page out of the total number of pages -->
    Page @(Model.PageCount < Model.PageNumber ? 0 : Model.PageNumber) of @Model.PageCount

    <!-- Create the page buttons and set the parameters for traversing the pages -->
    @Html.PagedListPager(Model, page => Url.Action("Home",
        new { page, sortOrder = Model.OrderBy(x => x.FullName), currentSearch = ViewBag.CurrentSearch }))
</div>
<!-- END PAGINATION SECTION -->
```

This immediately followed the section where my table was contained, but could easily be placed anywhere else. The first line is a simple display that shows the current page the user is viewing and what the total number of pages are, like so: "Page 3 of 12". The lines after that create the buttons that allow the user to transition between pages. It creates a Url.Action associated with each button that passes the page number, the sorting order (required for paginiation to maintain ordering between pages), and the current filter (which preserves the search results between pages). Below are screenshots demonstrating the pagination in action.


![](images/pagination_one.PNG?raw=true)

![](images/pagination_two.PNG?raw=true)

