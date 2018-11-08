# **Homework VII**

The goal for this assignment was to create another MVC web application, this time using AJAX to build responsive views. We also needed to learn how to use an existing API to acquire data serverside, the use of JSON, and custom routing URLs.

## **Relevant Links**
- [Home](https://brockv.github.io/)
- [Assignment Page](http://www.wou.edu/~morses/classes/cs46x/assignments/HW7_1819.html)
- [Code Repository](https://github.com/brockv/brockv.github.io/tree/master/CS460/HWK7/Homework7)
- [Demo Video](https://www.youtube.com/watch?v=q4Nt569kcC8&feature=youtu.be)

### **I: Setting Up the Controller**

The end goal of this assignment was to translate certain words from a client's input into GIFS using GIPHY's and their Sticker API Translate endpoint, then displaying them in the view. So to get this started, I first created the controller I would be using to handle the communication between GIPHY and the web app.

```c#
/// <summary>
/// Default method. Only called when the application first starts, or
/// if the page is refreshed.
/// </summary>
/// <returns></returns>
[HttpGet]
public ActionResult Index()
{
    return View();
}

/// <summary>
/// Assembles a GET request using the client's search term, an API key, and GIHPY's Sticker 
/// API Translate endpoint, and sends the request to GIPHY.
/// </summary>
/// <param name="lastWord">The client's search term.</param>
/// <returns>A JSON object containing the data about the request.</returns>
[HttpGet]
public JsonResult TranslateGIF(string lastWord)
{
    /* Get the API Key for making the request */
    string superSecretKey = System.Web.Configuration.WebConfigurationManager.AppSettings["APIKEY"];

    /* Build the URL with GIPHY's Translate Endpoint and the client's last word typed */
    string giphyURL = "http://api.giphy.com/v1/stickers/translate?api_key=" + superSecretKey + "&s=" + lastWord;

    /* Create a request using the constructed URL */
    WebRequest sendRequest = WebRequest.Create(giphyURL);

    /* Grab the response from the above request */
    WebResponse getReponse = sendRequest.GetResponse();

    /* Convert the response so we can read it */
    Stream data = sendRequest.GetResponse().GetResponseStream();

    /* Convert the response to a string so we can do stuff with it */
    string convertResponse = new StreamReader(data).ReadToEnd();

    /* Parse the JSON returned from GIPHY's Translate endpoint */
    var serialize = new System.Web.Script.Serialization.JavaScriptSerializer();
    var jsonResponse = serialize.DeserializeObject(convertResponse);

    /* Close the streams */
    data.Close();
    getReponse.Close();

    /* Returned the parsed object back to the client */
    return Json(jsonResponse, JsonRequestBehavior.AllowGet);
}
```

The focus of the controller is the TranslateGIF method, which takes a string and returns a JSON object. The string, "lastWord", is passed from the JavaScript, which I'll cover later. In order to send a request to GIPHY we need an API key from them. After registering for an account on their website, I was able to get a key and save it to a file on my computer. For security reasons, that key should never end up in my repository or in my source code, but I still needed to access it to make requests. This is accomplished through the first line in the method, as well as a minor adjustment to the Web.config file in the root directory of my project. After retrieving the API key, it, along with the string passed in, are used to construct a URL to send to GIPHY. The request is sent, and then we wait for a response. Once we have the response, it's converted first to a Stream, then again to string. We then deserialize it into the JSON object we want to return to the JavaScript, and close the Streams before returning out.

The last thing that needed to be done was save the request log to a database. After creating a database, model, and context file, I generated a table to store the information in. Each time a request is made, it's logged and saved to the database for viewing later.

```c#
/* Create a new entry in the database */
SearchRequestLog requestLogDB = requestLogDatabase.RequestLogs.Create();

/* Get the time of the request */
requestLogDB.RequestTimestamp = DateTime.Now;

/* Get what the client searched for */
requestLogDB.RequestType = Request.RequestType;//lastWord;

/* Get the IP address of the client */
requestLogDB.ClientIP = Request.UserHostAddress;

/* Get the client's browser */
requestLogDB.BrowserAgent = Request.UserAgent;

/* Add the search request to the database and save the changes */
requestLogDatabase.RequestLogs.Add(requestLogDB);
requestLogDatabase.SaveChanges();
```

### **II: Setting Up the View**

Once the controller was more or less complete, I created an empty view. All this needed was an input bar for the client to type a message into, a button to clear the output, and an area to display the client's input and GIF responses. What's seen below is the view in its entirety.

```html
<!-- DO NOT REMOVE THIS. Doing so will break the layout. -->
    </div>


<div class="row inputArea">

    <div class="col-12">
        <!-- Create the input bar -->
        @Html.TextBox("inputString", "", new
    {
        @class = "form-control",
        id = "userInput",
        placeholder = "Start typing your message here...",
        type = "text",
    })

    </div>
</div>

<div style="margin-top: 10px;">
    <!-- Add a button to refresh the page and let the client start over -->
    <input type="button" id="btnClear" value="Start Over" onClick="window.location.reload()">
</div>

<div class="inputArea" id="container">
    <p>
        <div id="messageContainer"></div>
    </p>
</div>


@section scriptSection
{
    <script type="text/javascript" src="~/Scripts/js_functions.js"></script>
}

```

### **III: Writing the JavaScript**

Nearly all of the work in this assignment is handled via JavaScript. Once the document is fully loaded, a callback function is registered on the input field, which listens for the spacebar being pressed. When that fires, the last word the client typed is parsed and sent to the controller in order to create a request.


The callback function is defined in main so that it's set up right when the document is fully loaded and ready.

```js
function main() {

    /* Give the input focus */
    $("#userInput").focus();

    /* Callback function that listens for the spacebar to be pressed */
    $("#userInput").keypress(function () {

        /* 32 == spacebar -- Was it pressed? */
        if ($("#userInput").val().length > 0 && event.which === 32) {

            /* Get the word the client typed before this space */
            var lastWord = getLastWordTyped();

            /* Determine what to do with the word */
            chooseAction(lastWord);

            /* Give the input focus again for ease of use */
            $("#userInput").focus();
        }
    });
}
```

This next function is used to get the last word the client typed in the input field. The first thing we do is get rid of the space from the end of the entire string, and then grab the last word by performing a pop. The word is then sent to another method where we determine what to do with it.

```js
function getLastWordTyped() {    

    /* Initialize a variable to hold the word we're after */
    var lastWord = "";

    /* Get the text from the input field */
    var input = document.getElementById("userInput").value;

    /* Trim the any punctuation, as well as the last space from the string */
    var parsedInput = input.substr(0, input.length).replace(/[.,\/#!$%\^&\*;:{}=\-_`~()]/g, "");

    /* Don't do anything if the string is just spaces */
    if (/\S/.test(parsedInput)) {

        /* Grab the last word from the string */
        lastWord = parsedInput.split(" ").pop();    
    }

    /* Return the word to be used */
    return lastWord;
}
```

If the word is in a list of predefined "fun" words, it's sent to the controller in order for a request to be constructed and sent to GIPHY. If it's a "boring" word, it is instead inserted directly into the view.

```js
function isFunWord(lastWord) {

    /* Convert the word to lower case for comparisons */
    var temp = lastWord;

    /* Check if this word is in our list of "fun" words */
    var isFunWord = true;
    for (var i = 0; i < boringWords.length; i++) {
        if (boringWords[i].toLowerCase() == temp.toLowerCase()) {
            isFunWord = false;
        }
    }

    /* Return true if it was in our list, false otherwise */
    return isFunWord;
}

function chooseAction(lastWord) {

    /* If this is a word we're interested in translating, do GIPHY magic */
    if (isFunWord(lastWord)) {

        $.ajax({

            dataType: "json",
            url: "/Main/TranslateGIF?=",
            data: { "lastWord": lastWord },
            success: insertGIF
        });
    }
    /* It's a "boring" word -- just send it to the view */
    else {

        insertWord(lastWord);
    }
}
```

The following two functions are responsible for making changes to the view by adding the GIF or the word, respectively.

```js
function insertGIF(result) {

    /* Grab the image type we want to display */
    var giphyURL = result.data.images.preview_gif.url;

    /* DEBUG -- Write the URL to console */
    console.log(giphyURL)

    /* Add the gif we grabbed to the "container" <div> tag using its id (#) */
    $("#messageContainer").append("<img width='75px;' height='75px;' src='" + giphyURL + "'/>");
}


function insertWord(lastWord) {

    /* Add the word entered to the "container" <div> tag using its id (#) */
    $("#messageContainer").append(" " + "<label> " + lastWord + " </label>");
}
```

### **IV: Examining the Database Logs**

After typing in a few messages, I went into the database to make sure that the logs were being saved. Sure enough, they were, and the database was very quickly filling up with information. Below is a screenshot of some of the logs.

![](images/database_logs.PNG?raw=true)
