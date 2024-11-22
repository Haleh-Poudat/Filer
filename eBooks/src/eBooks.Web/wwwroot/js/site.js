$(document).ready(function () {
    function enableFormValidation(formId) {
        $('#' + formId).data('validator', null);
        $.validator.unobtrusive.parse('#' + formId);
    }

    if ($('#jstree').length > 0) {
        if (allData != undefined || allData != null) {
            $(function () {
                $('#jstree').jstree({
                    'core': {
                        'data': allData,
                        'check_callback': true
                    }
                });
            });
        }
    }

    // Show password
    $('#password-toggle').on('click', function () {
        var passwordInput = $('#password-input');
        var passwordToggle = $('#password-toggle');

        if (passwordInput.attr('type') === 'password') {
            passwordInput.attr('type', 'text');
            passwordToggle.removeClass('fa-eye-slash').addClass('fa-eye');
        } else {
            passwordInput.attr('type', 'password');
            passwordToggle.removeClass('fa-eye').addClass('fa-eye-slash');
        }
    });

    window.enableFormValidation = enableFormValidation;

    // Function call during initial view loading.
    setRowNumbers();

    $(document).ajaxComplete(function () {
        setRowNumbers();
    });
    function getClickedNodeId() {
        var clickedElement = $('.jstree-clicked');
        var parentLi = clickedElement.closest('li');
        return parentLi.attr('id');
    }

    $('#jstree').on("changed.jstree", function (e, data) {
        $('#loadingOverlay').fadeIn();
        $('#btnEditCategory, #btnDeleteCategory, #btnCreateEbook').show();
        var selectedId = getClickedNodeId();
        if (selectedId) {
            $('#selectedCategoryId').val(selectedId);
            var createUrl = '/Category/Add' + '?id=' + selectedId;
            $('#btnCreateCategory').attr('data-ajax-url', createUrl);

            var editUrl = '/Category/Edit' + '?id=' + selectedId;
            $('#btnEditCategory').attr('data-ajax-url', editUrl);

            var createEbookUrl = '/Library/Create' + '?id=' + selectedId;
            $('#btnCreateEbook').attr('data-ajax-url', createEbookUrl);

            var deleteOnBegin = `DeleteCategory('${selectedId}', 'Are you sure you want to delete the selected group?', '/Category/Delete/', 'delete', 'refresh')`;
            $('#btnDeleteCategory').attr('data-ajax-begin', deleteOnBegin);
            $('#moreIndex').attr('pageIndex', '2');
            $('#Title').val('');

            $('#selectedTake').val('50');

            //$('#selectedTake').prop('selectedIndex', 0);
            //$('#selectedTake').trigger('change');
            //var text = $('#selectedTake option:first').text();

            $('span.current').text('Display 50 records.');
            $.ajax({
                url: '/Library/ListAjax',
                data: { selectedId },
                success: function (response) {
                    if (response === 'no-more-info') {
                        var newRow = '<tr class="text-center">' +
                            '<td></td>' +
                            '<td><span class="text-primary">No information found.</span></td>' +
                            '<td></td>' +
                            '<td></td>' +
                            '<td></td>' +
                            '</tr>';
                        $('#Datalist').html(newRow);
                    } else {
                        $('#Datalist').html(response);
                        setRowNumbers();
                    }
                    $('#loadingOverlay').fadeOut();
                },
                error: function (xhr, status, error) {
                    console.error('The operation encountered an error.', error);
                    $('#loadingOverlay').fadeOut();
                }
            });
        } else {
            var newRow = '<tr class="text-center">' +
                '<td></td>' +
                '<td><span class="text-primary">No data found.</span></td>' +
                '<td></td>' +
                '<td></td>' +
                '<td></td>' +
                '</tr>';
            $('#Datalist').html(newRow);
        }
    });
});

var curentUrl = window.Url;
var formId = window.formId;

$('#selectedTake').change(function () {
    $('input[name="Take"]').val($(this).val());
    $('#pageNumber').val(1);
    var operationTtype = 'SearchLibrearyOnSuccess(xhr,"html")';
    $(formId).attr('data-ajax-success', operationTtype);
    $(formId).submit();
});

function setRowNumbers() {
    $('.rowNo').each(function (index) {
        $(this).text(index + 1);
    });
}

function SearechBookTitle() {
    $('#loadingOverlay').fadeIn();
    $('#pageNumber').val(1);
    var operationTtype = 'SearchLibrearyOnSuccess(xhr,"html")';
    $(formId).attr('data-ajax-success', operationTtype);
    $(formId).submit();
}
function More() {
    $('#loadingOverlay').fadeIn();
    var index = $('#moreIndex').attr('pageIndex');
    $('#pageNumber').val(index);
    var operationTtype = 'SearchLibrearyOnSuccess(xhr,"append")';
    $(formId).attr('data-ajax-success', operationTtype);
    $(formId).submit();
}
function SearchLibrearyOnSuccess(xhr, operationTtype) {
    var result = xhr.responseText;

    if (operationTtype === 'append') {
        if (result === 'no-more-info') {
            MiniInfoAlertrTimer('No further information found.')
        } else {
            $('#Datalist').append(xhr.responseText);
            var num = parseInt($('#moreIndex').attr('pageIndex'));
            var index = (num) + 1;
            $('#moreIndex').attr('pageIndex', index)
        }
    }
    if (operationTtype === 'html') {
        if (result === 'no-more-info') {
            var newRow = '<tr class="text-center">' +
                '<td></td>' +
                '<td><span class="text-primary">No data found.</span></td>' +
                '<td></td>' +
                '<td></td>' +
                '<td></td>' +
                '</tr>';
            $('#Datalist').html(newRow);
        } else {
            $('#Datalist').html(result);
            setRowNumbers();
        }
    }
    $('#loadingOverlay').fadeOut();
}
function SearchOnSuccess(xhr) {
    $('#Datalist').append(xhr.responseText);
}
function AddOrEditLibraryOnFailure(xhr) {
    if (xhr.status == 400) {
        MiniErrorAlertTimer(xhr.responseText);
    }
    else if (xhr.status == 413) {
        MiniErrorAlertTimer('The selected file size exceeds the allowed limit.');
    }
    else if (xhr.status == 401) {
        MiniErrorAlertTimer('You do not have permission to perform this operation.');
    }
    else if (xhr.status == 403) {
        window.location = '/Account/LogIn';
    }
    else if (xhr.status == 404) {
        MiniErrorAlertTimer('The requested information was not found.');
    }

    else if (xhr.status == 500) {
        MiniErrorAlertTimer('A system error occurred. Please contact support.')
    }
}
function AddLibraryOnSuccess(xhr, status, data) {
    if (status == 'success') {
        $('#Datalist').prepend(data);
        $('#BestModal').modal('hide');
        MiniSuccessAlertTimer('The addition operation was completed successfully.');
    } else {
        $('#ErrorOnFailure').html(data);
    }
}
function AddOnCategorySuccess(xhr, status, data) {
    if (status === 'success') {
        var nodeData = JSON.parse(xhr.responseText);
        var parentId = nodeData.parentId;
        var new_node_text = nodeData.title;
        var new_node_id = nodeData.id;
        var position = 'last';

        if (parentId === null || parentId === undefined) {
            $('#jstree').jstree('create_node', '#', { "text": new_node_text, "id": new_node_id }, position, false, false);
            $('#BestModal').modal('hide');
        } else {
            var parent_node = $('#' + parentId);
            $('#jstree').jstree('create_node', $(parent_node), { "text": new_node_text, "id": new_node_id }, position, false, false);
            $('#jstree').jstree('deselect_node', parent_node);
            setTimeout(function () {
                $('#jstree').jstree().select_node(new_node_id);
            }, 500);
        }

        $('#BestModal').modal('hide');
        MiniSuccessAlertTimer('The category addition operation was completed successfully.');
    } else {
        $('#ErrorOnFailure').html('The entered information is incorrect.');
    }
}
function EditNodeOnSuccess(xhr, status, data, rowId, id) {
    if (status === 'success') {
        if (id !== null && id !== undefined) {
            if (xhr.readyState === 4 && xhr.status === 200) {
                var responseData = JSON.parse(xhr.responseText);

                var newText = responseData.title;
                var nodeId = responseData.id;
                if (nodeId) {
                    $('#jstree').jstree('rename_node', nodeId, newText);
                } else {
                    MiniSuccessAlertTimer('Please select a category to edit.');
                }
            }
        }
        $('#BestModal').modal('hide');
        MiniSuccessAlertTimer('The edit operation was completed successfully.');
    } else {
        $('#ErrorOnFailure').html('The edit operation failed.');
    }
}
function DeleteCategory(id, text, url, operation, rowId) {
    swal.fire({
        text: text,
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        cancelButtonColor: "#112E69",
        confirmButtonText: "accept",
        cancelButtonText: "cancel",
        background: "#777FEC",
        color: '#FFFFFF',
        closeOnConfirm: false, showClass: {
            popup: 'animate__animated animate__fadeInDown'
        },
        hideClass: {
            popup: 'animate__animated animate__fadeOutUp'
        }
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "POST",
                data: { id },
                success: function (param) {
                    if (param == "success") {
                        var selectedNode = $('#jstree').jstree('get_selected');
                        $('#jstree').jstree('delete_node', selectedNode);
                        MiniSuccessAlertTimer('Operation ' + ' ' + operation + ' ' + 'Successfully completed.');
                    }
                    else {
                        MiniErrorAlertTimer('Operation ' + ' ' + operation + ' ' + 'Failed.')
                    }
                },
                error: function (xhr) {
                    if (xhr.status == 400) {
                        MiniErrorAlertTimer(xhr.responseText)
                    }

                    else if (xhr.status == 403) {
                        window.location = '/Account/LogIn';
                    }
                    else if (xhr.status == 404) {
                        MiniErrorAlertTimer('The requested information was not found.');
                    }

                    else if (xhr.status == 500) {
                        MiniErrorAlertTimer('A system error occurred. Please contact support.')
                    }
                }
            });
        }
    });
};
function OnFailure(xhr) {
    if (xhr.status == 400) {
        MiniErrorAlertTimer(xhr.responseText)
    }
    else if (xhr.status == 401) {
        MiniErrorAlertTimer('You do not have permission to perform this operation.');
    }
    else if (xhr.status == 403) {
        window.location = '/Account/LogIn';
    }
    else if (xhr.status == 404) {
        MiniErrorAlertTimer('The requested information could not be found.');
    }

    else if (xhr.status == 500) {
        MiniErrorAlertTimer('A system error has occurred. Please contact support.')
    }
}
function AddOrEditOnFailure(xhr) {
    if (xhr.status == 400) {
        MiniErrorAlertTimer(xhr.responseText);
    }
    else if (xhr.status == 413) {
        MiniErrorAlertTimer(xhr.responseText);
    }
    else if (xhr.status == 401) {
        MiniErrorAlertTimer(xhr.responseText);
    }
    else if (xhr.status == 403) {
        window.location = '/Account/LogIn';
    }
    else if (xhr.status == 404) {
        MiniErrorAlertTimer('The requested information was not found.');
    }

    else if (xhr.status == 500) {
        MiniErrorAlertTimer('A system error has occurred. Please contact support.')
    }
}
function ShowModalOnSuccess(xhr, status, data, dialogClass, formId, firstScript) {
    if (status == 'success') {
        $('#BestModalDialog').addClass(dialogClass);
        $('#BestModal').modal('show');
        $('#BestModalBody').html(xhr.responseText);
        enableFormValidation(formId, firstScript);
    } else {
        if (xhr.status == 400) {
            MiniErrorAlertTimer(xhr.responseText);
        } else if (xhr.status == 403) {
            window.location = '/Account/LogIn';
        } else if (xhr.status == 404) {
            MiniErrorAlertTimer('The requested information was not found.');
        } else if (xhr.status == 500) {
            MiniErrorAlertTimer('A system error has occurred. Please contact support.');
        }
    }
}
function AddOnSuccess(xhr, status, data) {
    if (status == 'success') {
        $('#BestModal').modal('hide');
        MiniSuccessAlertTimer('The addition operation was completed successfully.');
    } else {
        $('#ErrorOnFailure').html(data);
    }
}

function EditOnSuccess(xhr, status, data, rowId) {
    if (status == 'success') {
        if (rowId != null || rowId != undefined) {
            $('#' + rowId).replaceWith(data);
        }
        $('#BestModal').modal('hide');
        MiniSuccessAlertTimer('The edit operation was completed successfully.');
    }
    else {
        $('#ErrorOnFailure').html('The entered information is incorrect.');
    }
}

function EditLibraryCategoryOnSuccess(xhr, status, data, rowId) {
    if (status == 'success') {
        if (rowId != null || rowId != undefined) {
            $('#' + rowId).replaceWith(data);
        }
        $('#BestModal').modal('hide');
        MiniSuccessAlertTimer('The edit operation was completed successfully.');
    }
    else {
        $('#ErrorOnFailure').html('The entered information is incorrect.');
    }
}

function RemoveUserRowInRoleSuccess(data, rowId) {
    if (data == 'success') {
        $('#' + rowId).remove();
        MiniSuccessAlertTimer('The user has been successfully removed from this role.');
        update(window.PageNumber);
    } else {
        MiniErrorAlertTimer('The operation failed. Please review the specified user.')
    }
}
function confirmDeleteOrReactivate(id, text, url, operation, rowId) {
    swal.fire({
        text: text,
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        cancelButtonColor: "#112E69",
        confirmButtonText: "accept",
        cancelButtonText: "cancel",
        background: "#777FEC",
        color: '#FFFFFF',
        closeOnConfirm: false, showClass: {
            popup: 'animate__animated animate__fadeInDown'
        },
        hideClass: {
            popup: 'animate__animated animate__fadeOutUp'
        }
    }).then((result) => {
        if (result.isConfirmed) {
            $('#loadingOverlay').fadeIn();
            $.ajax({
                url: url,
                type: "POST",
                data: { id },
                success: function (param) {
                    if (param == "success") {
                        MiniSuccessAlertTimer('Operation ' + ' ' + operation + ' ' + 'Successfully completed.');
                        if (rowId == "refresh") {
                            SuccessfulOperationAlert();
                        } else {
                            if (rowId == undefined) {
                                update(window.PageNumber);
                            } else {
                                $('#' + rowId).remove();
                            }
                        }
                    }
                    else {
                        MiniErrorAlertTimer('Operation ' + ' ' + operation + ' ' + 'Failed')
                    }
                    $('#loadingOverlay').fadeOut();
                },
                error: function (xhr) {
                    if (xhr.status == 400) {
                        MiniErrorAlertTimer(xhr.responseText)
                    }

                    else if (xhr.status == 403) {
                        window.location = '/Account/LogIn';
                    }
                    else if (xhr.status == 404) {
                        MiniErrorAlertTimer('The requested information was not found.');
                    }

                    else if (xhr.status == 500) {
                        MiniErrorAlertTimer('A system error has occurred. Please contact support.')
                    }
                    $('#loadingOverlay').fadeOut();
                }
            });
        }
    });
};
function DisMiss() {
    $('#BestModal').modal('hide');
}
function ChangePasswordOnSuccess(xhr, status, data) {
    if (status == 'success') {
        $('#BestModal').modal('hide');
        MiniSuccessAlertTimer('The password change operation was completed successfully.');
        $.ajax({
            url: '/LogOut',
            type: 'GET',
            success: function () {
                window.location.href = '/LogIn';
            },
            error: function () {
                alert('Server connection error.');
            }
        });
    } else {
        $('#ErrorOnFailure').html(data);
    }
}

$(document).on('change', '#fileInput', function (e) {
    var fileInput = this;
    $.ajax({
        url: '/library/GetSettingsValue',
        method: 'GET',
        success: function (data) {
            var allowedExtensionsPattern = new RegExp('(' + data.allowedExtensions.join('|').replace(/\./g, '\\.') + ')$', 'i');
            var maxFileSize = data.maxFileSize * 1024 * 1024;
            var errorMessage = '';

            if (fileInput.files.length > 0) {
                var file = fileInput.files[0];
                var fileExtension = file.name.split('.').pop();

                if (!allowedExtensionsPattern.test('.' + fileExtension)) {
                    errorMessage = 'The selected file format is invalid.';
                } else if (file.size > maxFileSize) {
                    errorMessage = 'The selected file size exceeds of ' + (maxFileSize / (1024 * 1024)) + ' MB';
                }
            } else {
                errorMessage = 'The selected file is empty.';
            }

            if (errorMessage) {
                MiniErrorAlertTimer(errorMessage);
            }
        },
        error: function () {
            MiniErrorAlertTimer('An error occurred while retrieving the settings.');
        }
    });
});

$(document).on('change', '.BanListChecker', function (e) {
    var password = $(this).val().toLowerCase();
    $.ajax({
        url: '/Admin/User/GetPasswordBanList',
        method: 'GET',
        success: function (data) {
            console.log(data);
            if (Array.isArray(data) && data.length > 0) {
                var result = data.includes(password);
                if (result) {
                    MiniErrorAlertTimer('The password is weak. Please choose a stronger password.');
                }
            }
        },
        error: function () {
            MiniErrorAlertTimer('An error occurred while retrieving the settings.');
        }
    });
});