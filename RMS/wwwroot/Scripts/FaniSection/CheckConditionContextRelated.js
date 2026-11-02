function CheckCC(IHC_CCId,  ItemsFBShomareh, LevelNumber) {
    var vardata = new Object();
    vardata.Id = IHC_CCId;
    $.ajax({
        type: "POST",
        url: '/ConditionContext/GetConditionContextWithId',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            debugger;
            CC_Related = data;

            if (CC_Related.length != 0) {

                for (var i = 0; i < CC_Related.length; i++) {
                    debugger;
                    state = $('#CK' + CC_Related[i].related).prop('checked');
                    if (state) {
                        $('#CK' + CC_Related[i].related).prop('checked', false);

                        deleteItemsConditionLevel2(CC_Related[i], CC_Related[i].relatedConditionGroupId, ItemsFBShomareh, LevelNumber)


                        //$('#CK' + data[i]).click();
                    }
                }
            }
            else {
                GetRizMetreWithFBId(LevelNumber);
                GetItemsAddedAndRelForRizMetre(LevelNumber);
            }
        },
        error: function (msg) {
            toastr.error('خطا در درج اطلاعات', 'خطا');
        }
    });
}


function deleteItemsConditionLevel2(Id, ConditionGroupId, ItemsFBShomareh, LevelNumber) {
    debugger;

    BarAvordUserId = $('#HDFBarAvordUserID').val();
    OperationId = $('#HDFOperationId').val();
    Year = $('#HDFYear').val();
    NoeFB = parseInt($('#HDFNoeFB').val());

    var vardata = new Object();
    vardata.BarAvordId = BarAvordUserId;
    vardata.strFBShomareh = ItemsFBShomareh;
    vardata.ConditionGroupId = ConditionGroupId;
    vardata.LevelNumber = LevelNumber;
    vardata.Year = Year;
    vardata.NoeFB = NoeFB;

    $.ajax({
        type: "POST",
        url: "/ItemsHasConditionAddedToFB/DeleteItemsHasConditionAddedToFBAndRizMetre",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            info = response.split('_');
            if (info[0] == "OK") {
                GetRizMetreWithFBId(LevelNumber);
                GetItemsAddedAndRelForRizMetre(LevelNumber);
                $('#divShowRizMetre' + Id).slideUp(500);

                toastr.success('اضافه بهای آیتم انتخابی حذف گردید', 'موفقیت');

            }
            else
                toastr.error('مشکل در حذف اضافه بها', 'خطا');
        },
        error: function (response) {
            toastr.error('مشکل در حذف اضافه بها', 'خطا');
        }
    });
}