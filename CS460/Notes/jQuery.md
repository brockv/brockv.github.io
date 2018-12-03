# **jQuery Examples**

## **AJAX GET/POST**

### **GET**

```js
$.get(URL,callback);
```

#### **GET Example One**

```js
$("button").click(function(){
    $.get("demo_test.asp", function(data, status){
        alert("Data: " + data + "\nStatus: " + status);
    });
});
```

#### **GET Example Two**

```js
$.ajax({
    type: "GET",
    dataType: "json",
    url: "/CONTROLLER/ACTION",
    data: { "id": VARIABLE },
    success: callback
});
```

#### **GET Example Three**

```js
.get("/CONTROLLER/ACTION", callback);
```

### **POST**

```js
$.post(URL,data,callback);
```

#### **POST Example One**

```js
$("button").click(function(){
    $.post("demo_test_post.asp",
    {
        name: "Donald Duck",
        city: "Duckburg"
    },
    function(data, status){
        alert("Data: " + data + "\nStatus: " + status);
    });
});
```

#### **POST Example Two**

```js
$.ajax({
    type: "POST",
    dataType: "json",
    url: "/CONTROLLER/ACTION",
    data: { "item": VARIABLE },
    success: callback,
    error: function (errormessage) {
        alert(errormessage.responseText);
    }
});
```

#### **POST Example Three**

```js
/* Send the information to the method */
$.post('/CONTROLLER/ACTION', DATA-TO-SEND, callback, 'json');
```

## **Building a Table With jQuery**

```html
<!-- Table headers (This is hidden by default. We only want to show it if there are bids.) -->
<table class="table" id="bidsTable">
    <thead>
        <tr>
            <th>
                Buyer
            </th>
            <th>
                Bid Amount
            </th>
        </tr>
    </thead>
    <!-- Table contents -->
    <tbody class="tbody"></tbody>
</table>
```

```js
function showBids(result) {

    /* Only build the table if we got something back in the result */
    if (!jQuery.isEmptyObject(result)) {
        /* Build the rows */
        var rows = '';
        $.each(JSON.parse(result), function (_key, item) {
            rows += '<tr>';
            rows += '<td>' + item.Buyer + '</td>';
            /* Format the bid amount into a currency display */
            rows += '<td>' + "$" + item.BidAmount.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,') + '</td>';
            rows += '</tr>';
        });
        $('.tbody').html(rows);

        /* Show the table */
        $("#tableHeader").html("Current Bids for this Item");
        $("#bidsTable").show();

    }
    /* Inform the user that there are no bids */
    else {
        $("#tableHeader").html("There are no Bids for this Item");
    }
}
```