$(document).ready(function () {
});
function SuccessAlert(textMessage) {
    swal.fire({
        icon: 'success',
        background: "#348115",
        text: textMessage,
        position: 'top-end',
        color: '#FFFFFF',
        backdrop: 'show',
        showConfirmButton: false,
        showClass: {
            popup: 'animate__animated animate__fadeInTopLeft'
        },
        hideClass: {
            popup: 'animate__animated animate__fadeOutTopLeft'
        },
        timer: 1000
    });
}
function SuccessAlertreload(textMessage) {
    swal.fire({
        icon: 'success',
        background: "#348115",
        text: textMessage,
        position: 'top-end',
        color: '#FFFFFF',
        backdrop: 'show',
        showConfirmButton: false,
        showClass: {
            popup: 'animate__animated animate__fadeInTopLeft'
        },
        hideClass: {
            popup: 'animate__animated animate__fadeOutTopLeft'
        },
        timer: 1000
    }).then(() => {
        location.reload();
    });
}
function ErrorAlert(textMessage) {
    swal.fire({
        icon: 'error',
        background: "#AE0101",
        text: textMessage,
        position: 'top-end',
        color: '#FFFFFF',
        backdrop: 'show',
        showConfirmButton: false,
        showClass: {
            popup: 'animate__animated animate__fadeInTopLeft'
        },
        hideClass: {
            popup: 'animate__animated animate__fadeOutTopLeft'
        },
    });
}
function WarningAlert(textMessage) {
    swal.fire({
        icon: 'error',
        background: "#B47707",
        text: textMessage,
        position: 'top-end',
        color: '#FFFFFF',
        backdrop: 'show',
        showConfirmButton: false,
        showClass: {
            popup: 'animate__animated animate__fadeInTopLeft'
        },
        hideClass: {
            popup: 'animate__animated animate__fadeOutTopLeft'
        }
    });
}
function InfoAlert(textMessage) {
    swal.fire({
        icon: 'error',
        background: "#414ACE",
        text: textMessage,
        position: 'top-end',
        color: '#FFFFFF',
        backdrop: 'show',
        showConfirmButton: false,
        showClass: {
            popup: 'animate__animated animate__fadeInTopLeft'
        },
        hideClass: {
            popup: 'animate__animated animate__fadeOutTopLeft'
        }
    });
}
function InfoAlertTimer(textMessage) {
    swal.fire({
        icon: 'error',
        background: "#414ACE",
        text: textMessage,
        position: 'top-end',
        color: '#FFFFFF',
        backdrop: 'show',
        showConfirmButton: false,
        showClass: {
            popup: 'animate__animated animate__fadeInTopLeft'
        },
        hideClass: {
            popup: 'animate__animated animate__fadeOutTopLeft'
        },
        timer: 1000
    });
}
function questionAlert(textMessage) {
    swal.fire({
        icon: 'error',
        background: "#607A93",
        text: textMessage,
        position: 'top-end',
        color: '#FFFFFF',
        backdrop: 'show',
        showConfirmButton: false,
        showClass: {
            popup: 'animate__animated animate__fadeInTopLeft'
        },
        hideClass: {
            popup: 'animate__animated animate__fadeOutTopLeft'
        }
    });
}
function SuccessfulOperationAlert() {
    swal.fire({
        icon: 'success',
        background: "#348115",
        text: 'عملیات باموفقیت انجام شد',
        position: 'top-end',
        color: '#FFFFFF',
        backdrop: 'show',
        showConfirmButton: false,
        showClass: {
            popup: 'animate__animated animate__fadeInTopLeft'
        },
        hideClass: {
            popup: 'animate__animated animate__fadeOutTopLeft'
        },
    }).then(() => {
        location.reload();
    });
}
function FailureAlert() {
    Swal.fire({
        icon: 'error',
        title: 'عملیات با شکست مواجه شد',
        text: 'لطفا مجددا تلاش نمایید'
    })
}
function DisMissAlert(textMessage) {
    swal.fire({
        icon: 'error',
        background: "#414ACE",
        text: textMessage,
        position: 'top-end',
        color: '#FFFFFF',
        backdrop: 'show',
        showConfirmButton: false,
        timer: 1000
    });
}
function ErrorAlertTimer(textMessage) {
    swal.fire({
        icon: 'error',
        background: "#AE0101",
        text: textMessage,
        position: 'top-end',
        color: '#FFFFFF',
        backdrop: 'show',
        showConfirmButton: false,
        timer: 1000
    });
}


//Mini Alert   Start
function MiniSuccessAlertTimer(textMessage) {
    const Toast = Swal.mixin({
        toast: true,
        position: "top-start",
        showConfirmButton: false,
        background: "#87f687",
        color: '#000000',
        timer: 2000,
        timerProgressBar: true,
        customClass: {
            popup: 'custom-toast-success' // اضافه کردن کلاس سفارشی
        },
        width: '600px',
        didOpen: (toast) => {
            toast.onmouseenter = Swal.stopTimer;
            toast.onmouseleave = Swal.resumeTimer;
        }
    });

    Toast.fire({
        icon: 'success',
        title: textMessage
    });
}
function MiniErrorAlertTimer(textMessage) {
    const Toast = Swal.mixin({
        toast: true,
        position: "top-start",
        showConfirmButton: false,
        background: "#ff6e6e",
        color: '#000000',
        timer: 2000,
        timerProgressBar: true,
        customClass: {
            popup: 'custom-toast-error' // اضافه کردن کلاس سفارشی
        }, width: '500px',

        width: '600px',
        didOpen: (toast) => {
            toast.onmouseenter = Swal.stopTimer;
            toast.onmouseleave = Swal.resumeTimer;
        }
    });

    Toast.fire({
        icon: 'error',
        title: textMessage
    });
}
function MiniInfoAlertrTimer(textMessage) {
    const Toast = Swal.mixin({
        toast: true,
        position: "top-start",
        showConfirmButton: false,
        background: "#9dd9f9",
        color: '#000000',
        timer: 2000,
        timerProgressBar: true,
        customClass: {
            popup: 'custom-toast-info' // اضافه کردن کلاس سفارشی
        }, width: '500px',

        width: '600px',
        didOpen: (toast) => {
            toast.onmouseenter = Swal.stopTimer;
            toast.onmouseleave = Swal.resumeTimer;
        }
    });

    Toast.fire({
        icon: 'info',
        title: textMessage
    });
}
function MiniWarningAlertTimer(textMessage) {
    const Toast = Swal.mixin({
        toast: true,
        position: "top-start",
        showConfirmButton: false,
        background: "#f6d54a",
        color: '#000000',
        timer: 2000,
        timerProgressBar: true,
        customClass: {
            popup: 'custom-toast-warning' // اضافه کردن کلاس سفارشی
        }, width: '500px',

        width: '600px',
        didOpen: (toast) => {
            toast.onmouseenter = Swal.stopTimer;
            toast.onmouseleave = Swal.resumeTimer;
        }
    });

    Toast.fire({
        icon: 'warning',
        title: textMessage
    });
}

//Mini Alert   End




