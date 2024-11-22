$(document).ready(function () {
    // تعریف تابع برای فعال کردن ولیدیشن فرم
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
    //End show password

    window.enableFormValidation = enableFormValidation;

    // فراخوانی تابع در زمان بارگذاری اولیه ویو
    setRowNumbers();

    // اجرای تابع در هنگام اجرای ایجکس
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

            var deleteOnBegin = `DeleteCategory('${selectedId}', 'از حذف گروه مورد نظر مطمعن هستید؟', '/Category/Delete/', 'حذف', 'refresh')`;
            $('#btnDeleteCategory').attr('data-ajax-begin', deleteOnBegin);
            $('#moreIndex').attr('pageIndex', '2');
            $('#Title').val('');

            $('#selectedTake').val('50');

            //$('#selectedTake').prop('selectedIndex', 0);
            //$('#selectedTake').trigger('change');
            //var text = $('#selectedTake option:first').text();

            $('span.current').text('نمایش 50 رکورد');
            $.ajax({
                url: '/Library/ListAjax',
                data: { selectedId },
                success: function (response) {
                    if (response === 'no-more-info') {
                        var newRow = '<tr class="text-center">' +
                            '<td></td>' +
                            '<td><span class="text-primary">اطلاعاتی یافت نشد</span></td>' +
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
                    console.error('عملیات با خطا مواجه شد', error);
                    $('#loadingOverlay').fadeOut();
                }
            });
        } else {
            var newRow = '<tr class="text-center">' +
                '<td></td>' +
                '<td><span class="text-primary">اطلاعاتی یافت نشد</span></td>' +
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
//function update(pageNumber) {
//    $.ajax({
//        type: 'GET',
//        url: curentUrl + '?pageNumber=' + pageNumber,
//        data: $('#FilterSearchForm').serialize(),
//    }).done(function (result) {
//        $('#Datalist').html(result);
//    });
//}
$('#selectedTake').change(function () {
    $('input[name="Take"]').val($(this).val());
    $('#pageNumber').val(1);
    var operationTtype = 'SearchLibrearyOnSuccess(xhr,"html")';
    $(formId).attr('data-ajax-success', operationTtype);
    $(formId).submit();
});

// تعریف تابع برای تنظیم شماره ردیف
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
            MiniInfoAlertrTimer('اطلاعات بیشتری یافت نشد')
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
                '<td><span class="text-primary">اطلاعاتی یافت نشد</span></td>' +
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
        //    $('#ErrorOnFailure').html(xhr.responseText);
    }
    else if (xhr.status == 413) {
        //window.location = '/Account/LogIn';
        MiniErrorAlertTimer('حجم فایل انتخابی بیشتر از حد مجاز میباشد');
    }
    else if (xhr.status == 401) {
        //window.location = '/Account/LogIn';
        MiniErrorAlertTimer(' شما مجوز دسترسی به این  عملیات را ندارید');
    }
    else if (xhr.status == 403) {
        window.location = '/Account/LogIn';
    }
    else if (xhr.status == 404) {
        MiniErrorAlertTimer('اطلاعات درخواستی یافت نشد');
    }

    else if (xhr.status == 500) {
        MiniErrorAlertTimer('بروز خطای سیستمی لطفا با پشتیبانی تماس بگیرید')
    }
}
function AddLibraryOnSuccess(xhr, status, data) {
    if (status == 'success') {
        $('#Datalist').prepend(data);
        $('#BestModal').modal('hide');
        MiniSuccessAlertTimer('عملیات افزودن با موفقیت انجام شد');
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
            // اضافه کردن نود اصلی (ریشه)
            $('#jstree').jstree('create_node', '#', { "text": new_node_text, "id": new_node_id }, position, false, false);
            $('#BestModal').modal('hide');
            //    $('#jstree').jstree().select_node(new_node_id);
        } else {
            var parent_node = $('#' + parentId);
            $('#jstree').jstree('create_node', $(parent_node), { "text": new_node_text, "id": new_node_id }, position, false, false);
            $('#jstree').jstree('deselect_node', parent_node);
            setTimeout(function () {
                $('#jstree').jstree().select_node(new_node_id);
            }, 500); // 500 میلی‌ثانیه به‌عنوان مثال
        }

        $('#BestModal').modal('hide');
        MiniSuccessAlertTimer('عملیات افزودن گروه با موفقیت انجام شد');
    } else {
        $('#ErrorOnFailure').html('اطلاعات وارد شده صحیح نمیباشند');
    }
}
function EditNodeOnSuccess(xhr, status, data, rowId, id) {
    if (status === 'success') {
        if (id !== null && id !== undefined) {
            if (xhr.readyState === 4 && xhr.status === 200) {
                var responseData = JSON.parse(xhr.responseText);

                var newText = responseData.title;
                var nodeId = responseData.id; // فرض می‌کنیم که یک نود انتخاب شده
                if (nodeId) {
                    $('#jstree').jstree('rename_node', nodeId, newText);
                } else {
                    MiniSuccessAlertTimer('گروهی را برای ویرایش انتخاب کنید');
                }
            }
        }
        $('#BestModal').modal('hide');
        MiniSuccessAlertTimer('عملیات ویرایش باموفقیت انجام شد');
    } else {
        $('#ErrorOnFailure').html('عملیات ویرایش  ناموفق بود');
    }
}
function DeleteCategory(id, text, url, operation, rowId) {
    swal.fire({
        text: text,
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        cancelButtonColor: "#112E69",
        confirmButtonText: "تایید",
        cancelButtonText: "انصراف",
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
                        MiniSuccessAlertTimer('عملیات ' + ' ' + operation + ' ' + 'با موفقیت انجام شد');
                    }
                    else {
                        MiniErrorAlertTimer('عملیات ' + ' ' + operation + ' ' + 'با شکست مواجه شد')
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
                        MiniErrorAlertTimer('اطلاعات درخواستی یافت نشد');
                    }

                    else if (xhr.status == 500) {
                        MiniErrorAlertTimer('بروز خطای سیستمی لطفا با پشتیبانی تماس بگیرید')
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
        //window.location = '/Account/LogIn';
        MiniErrorAlertTimer('شما مجوز دسترسی به این  عملیات را ندارید');
    }
    else if (xhr.status == 403) {
        window.location = '/Account/LogIn';
    }
    else if (xhr.status == 404) {
        MiniErrorAlertTimer('اطلاعات درخواستی یافت نشد');
    }

    else if (xhr.status == 500) {
        MiniErrorAlertTimer('بروز خطای سیستمی لطفا با پشتیبانی تماس بگیرید')
    }
}
function AddOrEditOnFailure(xhr) {
    if (xhr.status == 400) {
        //    $('#ErrorOnFailure').html(xhr.responseText);
        MiniErrorAlertTimer(xhr.responseText);
    }
    else if (xhr.status == 413) {
        //window.location = '/Account/LogIn';
        MiniErrorAlertTimer(xhr.responseText);
    }
    else if (xhr.status == 401) {
        //window.location = '/Account/LogIn';
        MiniErrorAlertTimer(xhr.responseText);
    }
    else if (xhr.status == 403) {
        window.location = '/Account/LogIn';
    }
    else if (xhr.status == 404) {
        MiniErrorAlertTimer('اطلاعات درخواستی یافت نشد');
    }

    else if (xhr.status == 500) {
        MiniErrorAlertTimer('بروز خطای سیستمی لطفا با پشتیبانی تماس بگیرید')
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
            MiniErrorAlertTimer('اطلاعات درخواستی یافت نشد');
        } else if (xhr.status == 500) {
            MiniErrorAlertTimer('بروز خطای سیستمی لطفا با پشتیبانی تماس بگیرید');
        }
    }
}
function AddOnSuccess(xhr, status, data) {
    if (status == 'success') {
        $('#BestModal').modal('hide');
        MiniSuccessAlertTimer('عملیات افزودن با موفقیت انجام شد');
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
        MiniSuccessAlertTimer('عملیات ویرایش باموفقیت انجام شد');
    }
    else {
        $('#ErrorOnFailure').html('اطلاعات وارد شده صحیح نمیباشند');
    }
}

function EditLibraryCategoryOnSuccess(xhr, status, data, rowId) {
    if (status == 'success') {
        if (rowId != null || rowId != undefined) {
            $('#' + rowId).replaceWith(data);
        }
        $('#BestModal').modal('hide');
        MiniSuccessAlertTimer('عملیات ویرایش باموفقیت انجام شد');
    }
    else {
        $('#ErrorOnFailure').html('اطلاعات وارد شده صحیح نمیباشند');
    }
}

function RemoveUserRowInRoleSuccess(data, rowId) {
    if (data == 'success') {
        $('#' + rowId).remove();
        MiniSuccessAlertTimer('حذف کاربر در این  نقش با موفقیت انجام شد');
        update(window.PageNumber);
    } else {
        MiniErrorAlertTimer('عملیات باشکست مواجه شد لطفا کاربر مورد نظر را مورد بررسی قرار دهید')
    }
}
function confirmDeleteOrReactivate(id, text, url, operation, rowId) {
    swal.fire({
        text: text,
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        cancelButtonColor: "#112E69",
        confirmButtonText: "تایید",
        cancelButtonText: "انصراف",
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
                        MiniSuccessAlertTimer('عملیات ' + ' ' + operation + ' ' + 'با موفقیت انجام شد');
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
                        MiniErrorAlertTimer('عملیات ' + ' ' + operation + ' ' + 'با شکست مواجه شد')
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
                        MiniErrorAlertTimer('اطلاعات درخواستی یافت نشد');
                    }

                    else if (xhr.status == 500) {
                        MiniErrorAlertTimer('بروز خطای سیستمی لطفا با پشتیبانی تماس بگیرید')
                    }
                    $('#loadingOverlay').fadeOut();
                }
            });
        }
    });
};
function DisMiss() {
    $('#BestModal').modal('hide');
    //    $('#BestModal').on('hidden.bs.modal', function (e) {
    //        DisMissAlert('عملیات لغو شد');
    //    });
}
function ChangePasswordOnSuccess(xhr, status, data) {
    if (status == 'success') {
        $('#BestModal').modal('hide');
        MiniSuccessAlertTimer('عملیات تغییر رمز عبور با موفقیت انجام شد');
        $.ajax({
            url: '/LogOut',
            type: 'GET',
            success: function () {
                    window.location.href = '/LogIn';
            },
            error: function () {
                alert('خطا در ارتباط با سرور');
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
            var maxFileSize = data.maxFileSize * 1024 * 1024; // تبدیل به بایت
            var errorMessage = '';

            if (fileInput.files.length > 0) {
                var file = fileInput.files[0];
                var fileExtension = file.name.split('.').pop();

                if (!allowedExtensionsPattern.test('.' + fileExtension)) {
                    errorMessage = 'فرمت فایل انتخابی شما نامعتبر است';
                } else if (file.size > maxFileSize) {
                    errorMessage = 'حجم فایل انتخابی بیشتر از ' + (maxFileSize / (1024 * 1024)) + ' مگابایت می‌باشد';
                }
            } else {
                errorMessage = 'فایل انتخابی خالی است';
            }

            if (errorMessage) {
                MiniErrorAlertTimer(errorMessage); // نمایش پیام خطا
                // می‌توانید اقدامات دیگری که نیاز دارید انجام دهید، اینجا اضافه کنید
            }
        },
        error: function () {
            MiniErrorAlertTimer('مشکلی در دریافت تنظیمات رخ داده است');
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
                    MiniErrorAlertTimer('پسورد ضعیف و قابل حدس است لطفا پسورد قوی تری انتخاب کنید');
                }
            }
        },
        error: function () {
            MiniErrorAlertTimer('مشکلی در دریافت تنظیمات رخ داده است');
        }
    });
});