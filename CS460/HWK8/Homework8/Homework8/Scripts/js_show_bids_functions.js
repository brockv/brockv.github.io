/* Get the ItemID from the hidden input field on the form*/
var itemID = document.getElementById("itemID").value;

/**
 * Builds a table with the data returned from the AJAX function
 * @param {any} result The data returned from the AJAX function
 */
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

/**
 * AJAX function that retrieves Bids for a given item to be 
 * displayed in a table on the view
 * */
function getBids() {
    /* Construct the AJAX call to retrieve the Bids for the item associated with the id */
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Bids/GetBids",
        data: { "id": itemID },
        success: showBids
    });
}

/**
 * Sets a refresh timer to the AJAX function, getBids()
 * */
function refreshBids() {
    /* Set the interval to 5 seconds */
    var interval = 1000 * 5;

    /* Apply the interval to the appropriate function */
    window.setInterval(getBids, interval);
}

///////////////////////////////////////////////////////
//                      MAIN                         //
///////////////////////////////////////////////////////
function main() {

    /* Initial call to the AJAX function */
    getBids();

    /* Set the refresh interval for the AJAX function */
    refreshBids();
}

/* Call main() once the page is fully loaded */
$(document).ready(main);