function displayBids() {
    $(document).ready(function () {
        $('#dataTable').DataTable({
            "ajax": {
                "url": "/Items/GetData",
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                { "data": "Buyer" },
                { "data": "Price" }
            ]
        });
    });
}

function refreshBids() {

    var interval = 1000 * X;
    window.setInterval(main, interval);
}

///////////////////////////////////////////////////////
//                      MAIN                         //
///////////////////////////////////////////////////////
function main() {
    displayBids();
    refreshBids();
}

/* Call main() once the page is fully loaded */
$(document).ready(main);