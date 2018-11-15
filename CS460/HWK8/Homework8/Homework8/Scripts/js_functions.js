/* Get the ItemID from the hidden input field on the form*/
var id = document.getElementById("hdnFlag").value;

function showBids(result) {
    var html = '';
    $.each(JSON.parse(result), function (key, item) {
        html += '<tr>';
        html += '<td>' + item.Buyer + '</td>';
        html += '<td>' + "$" + item.BidAmount.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,') + '</td>';
        html += '</tr>';
    });
    $('.tbody').html(html);

    refreshBids();
}

//if there is an error on the request
function refreshBids() {
    var interval = 1000 * 4;
    window.setInterval(main, interval);
}


///////////////////////////////////////////////////////
//                      MAIN                         //
///////////////////////////////////////////////////////
function main() { 
    $.ajax({
    type: "GET",
    dataType: "json",
    url: "/Bids/GetBids",
    data: { "id": id },
    success: showBids,
    error: function (errormessage) {
        alert(errormessage.responseText);
        }
    });
    
}

/* Call main() once the page is fully loaded */
$(document).ready(main);