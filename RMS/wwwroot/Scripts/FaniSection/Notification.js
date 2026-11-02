function loadNotifications() {
    debugger;
    BarAvordId = $('#HDFBarAvordUserID').val() == '' || $('#HDFBarAvordUserID').val() == undefined ? '' : $('#HDFBarAvordUserID').val();
    var vardata = new Object();
    vardata.BarAvordId = BarAvordId;

    $.ajax({
        url: '/UserNotification/GetUserNotification',
        data: JSON.stringify(vardata),
        type: 'post',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            let html = '';

            let unreadCount = data.filter(x => !x.isRead).length;

            if (unreadCount > 0) {
                $("#notificationCount")
                    .text(unreadCount)
                    .show();
            }
            else {
                $("#notificationCount").hide();
            }

            html += `<a href="#" class="dropdown-item text-center" style="padding:0px">
                           ${unreadCount}  پیغام جدید</a>
                        <div class="dropdown-divider"></div>

                        `;

            if (data.length === 0) {

                html = `
                        <div class="dropdown-item text-center">
                            اعلان جدیدی وجود ندارد
                        </div>`;
            }
            else {

                data.forEach(function (item) {

                    let shortText = item.description || '';

                    if (shortText.length > 100) {
                        shortText = shortText.substring(0, 100) + '...';
                    }

                    html += `
    <div class="notification-card">

        <a href="#"
           class="dropdown-item d-flex notification-item"
           data-id="${item.id}">

            <div style="width:100%">
                <strong>${item.title}</strong>

                <div class="notification-text">
                    ${shortText}
                </div>
            </div>

            ${!item.isRead
                            ? '<span class="badge badge-new">جدید</span>'
                            : ''}
        </a>

        <div class="notification-detail">
            ${item.description || ''}
        </div>

    </div>`;

                });
            }
            html += `<div class="dropdown-divider"></div>
                            <a href="#" style="padding:0px" class="dropdown-item text-center">مشاهده همه پیغام ها</a>`

            $("#notificationContainer").html(html);
        }
    });
}

function GetUnreadCount() {
    BarAvordId = $('#HDFBarAvordUserID').val() == '' || $('#HDFBarAvordUserID').val() == undefined ? '' : $('#HDFBarAvordUserID').val();
    var vardata = new Object();
    vardata.BarAvordId = BarAvordId;

    $.ajax({
        url: '/UserNotification/GetUnreadCount',
        data: JSON.stringify(vardata),
        type: 'post',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data > 0) {
                $("#notificationCount")
                    .text(data)
                    .show();
            }
            else {
                $("#notificationCount").hide();
            }
        }
    });
}


