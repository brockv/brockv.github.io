# **Homework V**

Similar to the last assignment, this weeks assignment was to write a simple, multi-page MVC web application. However, for this application we needed to utilize a simple database in order to keep track of mock service requests from campus apartment tennants.

## **Relevant Links**
- [Home](https://brockv.github.io/)
- [Assignment Page](http://www.wou.edu/~morses/classes/cs46x/assignments/HW5_1819.html)
- [Code Repository](https://github.com/brockv/brockv.github.io/tree/master/CS460/HWK5/Homework5)
- [Demo Video](https://www.youtube.com/watch?v=vN2k-Gze3Vo&feature=youtu.be)

### **I: Creating the Database and Table**

Based on the assignment requirements, there were eight things I needed to keep track of on each form. The tennants first name, last name, phone number, which apartment complex they lived in, their unit number, whether or not the landlord could enter their home unsupervised, and the time the request was submitted.

I started by first writing out what my database would need to store and in what format, and then I translated that into the required code. To create the table for the database and seed it with data, we needed to write a T-SQL script. The script below first creates the table with the required fields, and then populates the table with sample data.

```sql
-- Service Request Form table
CREATE TABLE [dbo].[ServiceRequestForms]
(
    [ID]                 INT IDENTITY (1, 1) NOT NULL,
    [FirstName]          NVARCHAR (30)		 NOT NULL,
    [LastName]           NVARCHAR (30)		 NOT NULL,
    [PhoneNumber]        NVARCHAR (20)		 NOT NULL,
    [ApartmentName]      NVARCHAR (64)		 NOT NULL,
    [UnitNumber]         INT				 NOT NULL,
    [RequestDescription] NVARCHAR (100)		 NOT NULL,
	[AllowEntry]         BIT	             NOT NULL,
    [RequestTimestamp]   DATETIME			 NOT NULL,
    CONSTRAINT [PK_dbo.ServiceRequestForms] PRIMARY KEY CLUSTERED ([ID] ASC)
);

INSERT INTO [dbo].[ServiceRequestForms] (FirstName, LastName, PhoneNumber, ApartmentName, UnitNumber, RequestDescription, AllowEntry, RequestTimestamp) VALUES
	('Doug', 'Douglas', '503-963-0325', 'Meadow Creek', '124', 'The backdoor is stuck shut.', '1', '2018/04/26 01:54:47 PM'),
	('Ivy', 'Iverson', '503-980-5452', 'Shady Oaks', '262', 'Upstairs neighbors are too loud after quiet hours.', '0', '2018/07/06 08:17:36 AM'),
	('Jim', 'Johnson', '503-870-0225', 'Robins Lane', '320', 'The bathroom faucet wont stop leaking', '1', '2018/08/16 11:44:47 PM'),
	('Sue','Suzanne', '503-987-2465', 'Meadow Creek', '202', 'The shower head in the main bathroom is broken.', '0', '2018/10/03 08:22:56 AM'),
	('Mira', 'Kuzak', '503-871-0285', 'Robins Lane', '110', 'The heaters in the bedrooms wont turn on.', '1', '2018/10/18 07:44:27 PM')
GO
```
To help manage and reset the database, another script was added that drops the table.

```sql
-- Take the ServiceRequestForms table down
DROP TABLE [dbo].[ServiceRequestForms];
```

### **II: Creating the Request Form Page**

Before building the view, I first created the model it would be generated from. Using the columns from my table as a reference, I added the appropriate properties to the model, setting annotations where necessary.

```c#
/// <summary>
/// Primary key for the table.
/// </summary>
[Key]
public int ID { get; set; }

/// <summary>
/// Property to hold the tennant's first name.
/// </summary>
[Required(ErrorMessage = "Please enter your first name"), StringLength(20)]
[Display(Name ="First Name")]
public string FirstName { get; set; }

/// <summary>
/// Property to hold the tennant's last name.
/// </summary>
[Required(ErrorMessage = "Please enter your last name"), StringLength(20)]
[Display(Name = "Last Name")]
public string LastName { get; set; }

/// <summary>
/// Property to hold the tennant's phone number.
/// </summary>
[Required(ErrorMessage = "Please enter your phone number"), StringLength(20)]
[RegularExpression("^\\d{3}-\\d{3}-\\d{4}$", ErrorMessage = "Please use the format: ###-###-####")]
[Display(Name = "Phone Number")]
public string PhoneNumber { get; set; }

/// <summary>
/// Property to hold the tennant's apartment name.
/// </summary>
[Required(ErrorMessage = "Please enter your apartment name"), StringLength(20)]
[Display(Name = "Apartments")]
public string ApartmentName { get; set; }

/// <summary>
/// Property to hold the tennant's unit number.
/// </summary>
[Required(ErrorMessage = "Please enter your unit number")]
[RegularExpression("([1-9][0-9]*)", ErrorMessage = "Unit Number must be positive")]
[Display(Name = "Unit Number")]
public int UnitNumber { get; set; }

/// <summary>
/// Property to hold the tennant's reason for submitting the request.
/// </summary>
[Required(ErrorMessage = "Please describe your request"), StringLength(250)]
[Display(Name = "Request Description")]
public string RequestDescription { get; set; }

/// <summary>
/// Property to hold the tennant's consent for unsupervised entry.
/// </summary>
[Display(Name = "Entry Allowed?")]
public bool AllowEntry { get; set; }

/// <summary>
/// Property to hold the time the tennant's form was submitted.
/// </summary>
[Display(Name = "Date Submitted")]
public DateTime RequestTimestamp { get; set; }
```

Once that was done, I was able to build the actual page as a strongly-typed view using the model I just created. This bound each input element on the form to the corresponding property in the model.

```html
<div class="col-md-4" style="float: left; margin-bottom: 10px;">
    <div class="form-group">

        @Html.LabelFor(model => model.FirstName, "First Name:", htmlAttributes: new { @class = "control-label" })
        @Html.EditorFor(model => model.FirstName, new { htmlAttributes = new { @class = "form-control" } })
        @Html.ValidationMessageFor(model => model.FirstName, "", new { @class = "text-danger" })
    </div>
</div>


<div class="col-md-4" style="float: left; margin-bottom: 10px;">
    <div class="form-group">
        @Html.LabelFor(model => model.LastName, "Last Name:", htmlAttributes: new { @class = "control-label" })
        @Html.EditorFor(model => model.LastName, new { htmlAttributes = new { @class = "form-control" } })
        @Html.ValidationMessageFor(model => model.LastName, "", new { @class = "text-danger" })
    </div>
</div>
```

Something I added later was a short message informing the user that their form was submitted successfully, and that the were going to be redirected to another page. I did this by setting ViewBag.Message in the controller only after their form was accepted and saved to the database, 

```c#
/* Check if the model was handed to us in a valid state */
if (ModelState.IsValid)
{
    /* Add the service request to the database and save the changes */
    requestFormDatabase.RequestForms.Add(newRequestForm);
    requestFormDatabase.SaveChanges();

    /* Set a success message (displayed on the view), and redirect after a short delay */
    ViewData["Success"] = "Submitted successfully! You will now be redirected...";
    Response.AddHeader("REFRESH", "3;URL=http://localhost:56053/Home/RequestLog");
```

and then added a section in the view to display the message.

```html
<h1 id="successMessage"><strong>@ViewData["Success"]</strong></h1>
```

### **III: Creating the Request Log Page**

This page was almost entirely generated by Visual Studio. I used the List template when adding the view, and then made changes to the template to style the table to my tastes. One important  piece of this step though, was that we were expected to list all the entries in the database from oldest to newest. The snippet below accomplished that.

```c#
/* Generates a list of all the requests, ordered by oldest to newest request */
return View(requestFormDatabase.RequestForms.ToList().OrderBy(x => x.RequestTimestamp));
```

### **IV: Connecting the Database to the Application**

Quite possible the most important step in this entire project was connecting the database we created to our application. This required the addition of a new folder to the project named DAL (Data Access Layer), which contained a file that served as the bridge between the two.

```c#
public class ServiceRequestFormContext : DbContext
{
        /// <summary>
        /// Constructor which links the database to the web application
        /// </summary>
        public ServiceRequestFormContext() : base("name=RequestsDatabase") { }

        /// <summary>
        /// Used to let us both query the database and save instances of it.
        /// </summary>
        public virtual DbSet<ServiceRequestForm> RequestForms { get; set; }
}
```

We also needed to add the following to our Web.config file, which specifies the name of our database, the string used to connect to the database from the application, and the provider.

```xml
<connectionStrings>
<add name="RequestsDatabase"
     connectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\vance\Documents\School\CS460\brockv.github.io\CS460\HWK5\Homework5\Homework5\App_Data\RequestsDatabase.mdf;Integrated Security=True"
     providerName="System.Data.SqlClient"/>
</connectionStrings>
```

### **V: The Working Pages**

#### **Home Page**

![](images/homepage.PNG?raw=true)

#### **Request Form**

![](images/requestform_one.PNG?raw=true)

![](images/requestform_two.PNG?raw=true)

![](images/requestform_three.PNG?raw=true)


#### **Request Log**

![](images/requestlog.PNG?raw=true)

