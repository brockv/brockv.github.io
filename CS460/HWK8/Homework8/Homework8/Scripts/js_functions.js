var id = document.getElementById("hdnFlag").value;

function showBids(result) {
    var rows;
    $.each(JSON.parse(result), function (i, item) {
        rows += "<tr>"
            + "<td>" + item.Buyer + "</td>"
            + "<td>" + item.BidAmount + "</td>"
            + "</tr>";
    });
    $('#bidsTable tbody').append(rows);
}

//if there is an error on the request
function errorOnAjax() {
    console.log("ERROR");
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
        success: function (result) {
            var html = '';
            $.each(JSON.parse(result), function (key, item) {
                html += '<tr>';
                html += '<td>' + item.Buyer + '</td>';
                html += '<td>' + "$" + item.BidAmount.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,') + '</td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

    //var interval = 1000 * 4;
    //window.setInterval(showBids, interval);

}

/* Call main() once the page is fully loaded */
$(document).ready(main);