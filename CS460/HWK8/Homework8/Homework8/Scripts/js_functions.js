﻿/* Get the ItemID from the hidden input field on the form*/
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
}

//if there is an error on the request
function refreshBids(ajax_call) {
    var interval = 1000 * 4;
    window.setInterval(ajax_call, interval);
}


///////////////////////////////////////////////////////
//                      MAIN                         //
///////////////////////////////////////////////////////
function main() {

    
    var ajax_call = function () {
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

    refreshBids(ajax_call);
}

/* Call main() once the page is fully loaded */
$(document).ready(main);