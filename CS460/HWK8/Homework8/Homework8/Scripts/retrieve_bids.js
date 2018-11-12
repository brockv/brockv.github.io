$(document).ready(function () {
    $('#dataTable').DataTable({
        "ajax": {
            "url": "/Home/GetData",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "Name" },
            { "data": "Position" },
            { "data": "Office" },
            { "data": "Age" },
            { "data": "Salary" }
        ]
    });
}); 