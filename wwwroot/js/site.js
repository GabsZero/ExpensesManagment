// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function showConfirmationMessage(id, name) {
    console.log(id, name)
    bootbox.dialog({
        title: 'Removing Expense Type record',
        message: `<p>This action will permanently remove this record (${name}) from your data. Do you want to proceed?.</p>`,
        size: 'large',
        buttons: {
            cancel: {
                label: "No, it was a mistake!",
                className: 'btn-danger',
                callback: function () {
                    console.log('Custom cancel clicked');
                }
            },
            ok: {
                label: "Yes, please, delete this!",
                className: 'btn-default',
                callback: function () {
                    console.log($('#delete_route').val() + `/${id}`)
                    $.ajax({
                        url: $('#delete_route').val() + `/${id}`,
                        method: 'POST',
                        data: {
                            id: id
                        },
                        success: function (data) {
                            bootbox.hideAll()
                            $(`#${id}`).remove()
                            $('#table').bootstrapTable('refresh')
                            
                        }
                    })
                }
            }
        }
    })
}