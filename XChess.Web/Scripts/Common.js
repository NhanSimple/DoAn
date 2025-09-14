
function AfterSuccessActionAjaxform() {
    window.history.back();
}

function AjaxFormError(rs) {
    NotiError(rs);
}   

function AjaxLoginSuccess(rs) {
    if (rs.Status) {
        $("#MasterModal").modal("hide");
        $("#MasterModal").empty();
        $(".center-container").hide();
        document.getElementById("loading").style.display = "block";
    }
}
function AjaxLoginError(rs) {
    $("#thongbao").append(`<p style="color: red;">${rs.responseJSON.Message}</p>`)
}

function AjaxFormSuccess(rs) {
    if (rs.Status) {
        $("#MasterModal").modal("hide");
        $("#MasterModal").empty();
        NotiSuccess(rs.Message);
        //setTimeout(function () {
        //    AfterSuccessActionAjaxform(); // Gọi sau một khoảng delay nhỏ
        //}, 3000); // 1 giây (tuỳ thuộc toast của bạn)



    } else {
        NotiError("Lỗi sử lý", rs.Message)
    }
}
function NotiSuccess(message) {
    showToast(message, 3000, "#32CD32")
}

function NotiError(message) {
    showToast(message, 3000, "#CD0000")
}
function showToast(message, duration = 3000, color = "#4CAF50") {
    const container = document.getElementById("toast-container");

    const toast = document.createElement("div");
    toast.className = "toast";
    toast.style.backgroundColor = color;

    const text = document.createElement("span");
    text.textContent = message;
    toast.appendChild(text);

    const closeButton = document.createElement("button");
    closeButton.textContent = "×";
    closeButton.classList.add("close-btn");
    closeButton.onclick = () => {
        toast.classList.remove("show");
        setTimeout(() => toast.remove(), 300);
    };
    toast.appendChild(closeButton);

    container.appendChild(toast);

    setTimeout(() => {
        toast.classList.add("show");
    }, 50);

    setTimeout(() => {
        toast.classList.remove("show");
        setTimeout(() => toast.remove(), 300);
    }, duration);
}


function CreateAction(url) {
    AjaxCall(url, 'get', null, function (rs) {
        const $modal = $("#MasterModal");

        // Gán nội dung vào modal
        $modal.html(rs);

        // Hủy đăng ký các sự kiện cũ để tránh bind trùng
        $modal.off('shown.bs.modal hide.bs.modal');

        // Xử lý khi modal hiển thị xong
        $modal.on('shown.bs.modal', function () {
            const isFixedRight = $modal.find('.modal-dialog.fixed-right').length > 0;

            if (isFixedRight) {
                // Delay để đảm bảo focus mặc định đã được Bootstrap xử lý xong
                setTimeout(() => {
                    if ($modal.has(document.activeElement).length > 0) {
                        document.activeElement.blur(); // fix lỗi accessibility
                    }

                    // Nếu bạn muốn vô hiệu hóa pointer/mask
                    $(".modal-backdrop").css('display', 'none');
                    $modal.css('pointer-events', 'none');
                }, 50);
            }
        });

        // Xử lý trước khi modal đóng
        $modal.on('hide.bs.modal', function () {
            if (this.contains(document.activeElement)) {
                document.activeElement.blur(); // tránh lỗi aria-hidden
            }

            // Khôi phục pointer-events nếu bạn đã vô hiệu hóa trước đó
            $modal.css('pointer-events', 'auto');
        });

        // Gọi hiển thị modal
        $modal.modal("show");
    });
}

//function CreateAction(url) {
//    AjaxCall(url, 'get', null, function (rs) {
//        $("#MasterModal").html(rs);
//        $("#MasterModal").modal("show");
//        if ($('#MasterModal .modal-dialog.fixed-right').length > 0) {
//            document.activeElement.blur();
//            $(".modal-backdrop").css('display', 'none');
//            $(".modal").css('pointer-events', 'none');
//            $("button[data-dismiss=modal]").on('click', function () {
//                $(".modal-backdrop").css('display', 'none');
//                $(".modal").css('pointer-events', 'auto');
//            });
//        } else {
//        }
//    });
//}
function AjaxCall(url, type, data, callback, callbackError) {
    var isfunction = callback && typeof (callback) == "function";
    if (!isfunction) {
        callback = function () {
            console.log("Chưa cài đặt sự kiện thành công");
        }
    }

    var isfunction = callbackError && typeof (callbackError) == "function";
    if (!isfunction) {
        callbackError = function () {
            NotiError("Thao tác không thể thực hiện");
        }
    }
    $.ajax({
        url: url,
        type: type,
        success: callback,
        error: callbackError
    })
}


