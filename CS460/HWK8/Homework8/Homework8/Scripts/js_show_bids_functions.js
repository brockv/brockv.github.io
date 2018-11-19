/* Get the ItemID from the hidden input field on the form*/
var itemID = document.getElementById("itemID").value;

/**
 * Builds a table with the data returned from the AJAX function
 * @param {any} result The data returned from the AJAX function
 */
function showBids(result) {

    /* Only build the table if we got something back in the result */
    if (!jQuery.isEmptyObject(result)) {

        /* Build the columns */
        var columns = "";
        columns += '<tr>';
        columns += '<th>Buyer</th>';
        columns += '<th>Bid Amount</th>';
        columns += '</tr>';
        $(".thead").html(columns);

        /* Build the rows */
        var rows = '';
        $.each(JSON.parse(result), function (_key, item) {
            rows += '<tr>';
            rows += '<td>' + item.Buyer + '</td>';
            rows += '<td>' + "$" + item.BidAmount.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,') + '</td>'; /* Format the bid amount into a currency display */
            rows += '</tr>';
        });
        $('.tbody').html(rows);

        /* Show the table */
        $("#tableHeader").show();
        $("#bidsTable").show();
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
        success: showBids,
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

/**
 * Sets a refresh timer to the AJAX function, getBids()
 * */
function refreshBids() {
    var interval = 1000 * 4;
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