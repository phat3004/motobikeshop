// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

/* Write your JavaScript code.*/

$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})

// Initialize popover component
$(function () {
    $('[data-toggle="popover"]').popover()
})


showInPopUp = (url, title) => {
    $.ajax({
        type: "Get",
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
        }
    })
}

$(function () {
    $('#alert-box').removeClass('hide');
    $('#alert-box').delay(2000).slideUp(700);
});

