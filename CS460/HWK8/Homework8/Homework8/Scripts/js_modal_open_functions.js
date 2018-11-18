$(document).ready(function () {

    /* Open the modal for creating new listings */
    $("#btnOpenModal").click(function () {
        /* Get the modal from the partial view it's in */
        $.get("/Items/Create", function (data) {
            
            /* Send the data to the modal and show it */
            $("#createModal").html(data);
            $(window).on('load', function () {
                jQuery("#createModal").modal("show");
            });
        });
    });

    /* Open the modal for editing existing listings */
    $(".actionLinkEdit").click(function () {
        /* Get the modal from the partial view it's in */
        $.get("/Items/Edit?id=" + $(this).data('id'), function (data) {

            /* Send the data to the modal and show it */
            $("#createModal").html(data);
            $(window).on('load', function () {
                jQuery("#createModal").modal("show");
            });
        });        
    });

    /* Open the modal for deleting listings */
    $(".actionLinkDelete").click(function () {
        /* Get the modal from the partial view it's in */
        $.get("/Items/ConfirmDelete?id=" + $(this).data('id'), function (data) {

            /* Send the data to the modal and show it */
            $("#createModal").html(data);
            $(window).on('load', function () {
                jQuery("#createModal").modal("show");
            });
        });
    });
});